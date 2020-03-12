
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
    [Required]
    public string Firstname { get; set; }
      [Required]
      public string  Lastname { get; set; }
      public string Email{ get; set; }

     public string Phone { get; set; }

     public CompanyViewModel Company { get; set; }
     
     [ForeignKey("Company")]
     public int ForeignFK { get; set; }
    }
}