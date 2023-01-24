using FormulaApi.Core;
using FormulaApi.Data;
using FormulaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public DriversController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    // GET
    [HttpGet]
    [Route("getById")]
    public async Task<IActionResult> Get(int Id)
    {
        Driver driver = await _unitOfWork.Drivers.GetById(Id);
        return Ok(driver);
    }

    // GET ALL
    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _unitOfWork.Drivers.All());
    }
    
    // POST 
    [HttpPost]
    [Route("AddDriver")]
    public async Task<IActionResult> AddDriver(Driver driver)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.Drivers.Add(driver);
            await _unitOfWork.CompleteAsync();
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
    public async Task<IActionResult> Update(Driver entity)
    {
        Driver driver = await _unitOfWork.Drivers.GetById(entity.Id);
        if (driver != null)
        {
            await _unitOfWork.Drivers.Update(entity);
            await _unitOfWork.CompleteAsync();
            return Ok(driver);
        }
        else return NotFound();
        
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int Id)
    {
        Driver driver = await _unitOfWork.Drivers.GetById(Id);
        if (driver != null)
        { 
            await _unitOfWork.Drivers.Delete(driver);
            await _unitOfWork.CompleteAsync();
            return Ok(driver);
        }
        else return NotFound();
    }
}