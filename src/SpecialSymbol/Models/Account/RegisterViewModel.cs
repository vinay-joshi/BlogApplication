using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpecialSymbol.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress ,MaxLength(500)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
    
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Password must match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
