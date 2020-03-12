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
    public class CompanyController : Controller
    {
  private readonly ApplicationDbContext _db;

  public  CompanyController (ApplicationDbContext db)
  {
      _db = db;
  }
           [HttpGet("[action]")]
          // [Authorize(Policy = "RequireLoggedIn")]
        public IActionResult GetCompanies(){

          return Ok(_db.Company.ToList());
        }

             [HttpPost("[action] ")]
     //   [Authorize(Policy = "RequireAdminstratorRole")]
        public async Task<IActionResult> AddCompany([FromBody] CompanyViewModel formData){
            var newproduct =  new CompanyViewModel {
                Name = formData.Name,
                Email = formData.Email ,
                ImageName = formData.ImageName
             
            };

         await _db.Company.AddAsync(newproduct);
        await _db.SaveChangesAsync();
            return Ok();
        }

                [HttpPut("[action]/{id}")]
        //[Authorize(Policy = "RequireAdminstratorRole")]
        public async Task<IActionResult> UpdateCompany([FromRoute] int id,
        [FromBody] CompanyViewModel  formData){
            
          if (!ModelState.IsValid){
              return BadRequest(ModelState);
          }
          var findCompany = _db.Company.FirstOrDefault(p => p.Id == id);

          if (findCompany == null) {
              return NotFound();
          }
          //If company was found
             findCompany.Name = formData.Name;
             findCompany.Email = formData.Email;
            findCompany.ImageName = formData.ImageName;
       

                _db.Entry(findCompany).State =EntityState.Modified;
                  await _db.SaveChangesAsync();
                  return Ok(new JsonResult("The company with id" + id + " is updated"));
        }
               [HttpDelete("[action]/{id}")]
          // [Authorize(Policy = "RequireAdminstratorRole")]
          public async Task<IActionResult> DeleteCompany([FromRoute] int id){
          if (!ModelState.IsValid){
              return BadRequest(ModelState);
            }
              //find company
              var findCompany = await _db.Company.FindAsync(id);

             if (findCompany == null) {
              return NotFound();
          }
           _db.Company.Remove(findCompany);
            await _db.SaveChangesAsync();
          return Ok(new JsonResult("The company with id" + id + " was Deleted."));
          }
    }
    
    }