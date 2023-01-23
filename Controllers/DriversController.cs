using FormulaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormulaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private static List<Driver> _drivers = new List<Driver>()
    {
        new Driver()
        {
            Id = 1,
            Name = "Gabriel Gilles Adjowotor",
            Team = "F150 Seria A",
            DriverNumber = 45
        }, new Driver()
        {
            Id = 2,
            Name = "Anthony Elorm Zottor",
            Team = "Maybach Formuala 360",
            DriverNumber = 89
        }
    };
    
    
    // GET
    [HttpGet]
    [Route("getById")]
    public IActionResult Get(int Id)
    {
        return Ok(_drivers.Where(s => s.Id == Id).FirstOrDefault());
    }

    // GET ALL
    [HttpGet]
    [Route("getAll")]
    public IActionResult GetAll()
    {
        return Ok(_drivers);
    }
    
    // POST 
    [HttpPost]
    [Route("AddDriver")]
    public IActionResult AddDriver(Driver driver)
    {
        if (ModelState.IsValid)
        {
            _drivers.Add(driver);
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
    public IActionResult Update(int Id, Driver entity)
    {
        Driver driver = _drivers.FirstOrDefault(s => s.Id == Id);
        if (driver != null)
        {   
            driver.DriverNumber = entity.DriverNumber;
            driver.Team = entity.Team;
            driver.Name = entity.Name;
            return Ok(driver);
        }
        else return NotFound();
        
    }


    [HttpDelete]
    [Route("Delete")]
    public IActionResult Delete(int Id)
    {
        Driver driver = _drivers.FirstOrDefault(s => s.Id == Id);
        if (driver != null)
        {
            _drivers.Remove(driver);
            return Ok(_drivers);
        }
        else return NotFound();
    }
}