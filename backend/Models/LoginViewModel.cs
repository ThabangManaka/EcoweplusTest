using System;
using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name="User Name")] 
        public string Username { get; set; }
         [Required]
         [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}