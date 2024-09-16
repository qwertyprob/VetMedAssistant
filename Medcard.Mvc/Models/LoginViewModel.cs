using System.ComponentModel.DataAnnotations;

namespace Medcard.Mvc.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        // This property can be used to redirect to a specific URL after login
        public string ReturnUrl { get; set; }
    }
}
