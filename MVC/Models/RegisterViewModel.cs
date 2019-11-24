using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string Tel { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20,ErrorMessage = "Password length show be between 6 and 20",MinimumLength = 6)]
        [Compare("ConfirmPassword", ErrorMessage = "Password should match")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string Sex { get; set; }
        public string ErrorMessage { get; set; }
    }
}