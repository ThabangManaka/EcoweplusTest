using System.Net.Http;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;


namespace backend.Controllers
{
     [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
  private readonly ApplicationDbContext _db;

  public  EmployeeController (ApplicationDbContext db)
  {
      _db = db;
  }
           [HttpGet("[action]")]
          // [Authorize(Policy = "RequireLoggedIn")]
        public IActionResult GetEmployees(){

          return Ok(_db.Employee.ToList());
        }

             [HttpPost("[action] ")]
     //   [Authorize(Policy = "RequireAdminstratorRole")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeViewModel formData){
            var newemployee =  new EmployeeViewModel {
                Firstname = formData.Firstname,
                Lastname = formData.Lastname,
                Email = formData.Email ,
                Phone= formData.Phone,
                Company = formData.Company

             
            };

         await _db.Employee.AddAsync(newemployee);
        await _db.SaveChangesAsync();
            return Ok();
        }

                [HttpPut("[action]/{id}")]
        //[Authorize(Policy = "RequireAdminstratorRole")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id,
        [FromBody] EmployeeViewModel  formData){
            
          if (!ModelState.IsValid){
              return BadRequest(ModelState);
          }
          var findEmployee = _db.Employee.FirstOrDefault(p => p.Id == id);

          if (findEmployee  == null) {
              return NotFound();
          }
          //If company was found
             findEmployee.Firstname  = formData. Firstname ;
             findEmployee . Lastname  = formData. Lastname ;
             findEmployee.Email = formData.Email;
             findEmployee.Phone = formData.Phone;
            findEmployee.Company = formData.Company;
       

                _db.Entry(findEmployee).State =EntityState.Modified;
                  await _db.SaveChangesAsync();
                  return Ok(new JsonResult("The employee with id" + id + " is updated"));
        }
               [HttpDelete("[action]/{id}")]
          // [Authorize(Policy = "RequireAdminstratorRole")]
          public async Task<IActionResult> DeleteEmployee([FromRoute] int id){
          if (!ModelState.IsValid){
              return BadRequest(ModelState);
            }
              //find employe
              var findEmployee = await _db.Employee.FindAsync(id);

             if (findEmployee == null) {
              return NotFound();
          }
           _db.Employee.Remove(findEmployee);
            await _db.SaveChangesAsync();
          return Ok(new JsonResult("The employee with id" + id + " was Deleted."));
          }
    }
    
    }