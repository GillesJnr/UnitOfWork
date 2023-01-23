using FormulaApi.Data;
using FormulaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly ApiDbContext _context;
    public DriversController(ApiDbContext context)
    {
        _context = context;
    }
    
    
    // GET
    [HttpGet]
    [Route("getById")]
    public async Task<IActionResult> Get(int Id)
    {
        Driver driver = await _context.Drivers.FirstOrDefaultAsync(s => s.Id == Id);
        return Ok(driver);
    }

    // GET ALL
    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Drivers.ToListAsync());
    }
    
    // POST 
    [HttpPost]
    [Route("AddDriver")]
    public async Task<IActionResult> AddDriver(Driver driver)
    {
        if (ModelState.IsValid)
        {
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddDriver), new { Id = driver.Id }, driver);
        }
        else
        {
            return BadRequest();
        }
    }
    
    
    // UPDATE 
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update(int Id, Driver entity)
    {
        Driver driver = await _context.Drivers.FirstOrDefaultAsync(s => s.Id == Id);
        if (driver != null)
        {   
            driver.DriverNumber = entity.DriverNumber;
            driver.Team = entity.Team;
            driver.Name = entity.Name;
            await _context.SaveChangesAsync();
            return Ok(driver);
        }
        else return NotFound();
        
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int Id)
    {
        Driver driver = await _context.Drivers.FirstOrDefaultAsync(s => s.Id == Id);
        if (driver != null)
        { 
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return Ok(driver);
        }
        else return NotFound();
    }
}