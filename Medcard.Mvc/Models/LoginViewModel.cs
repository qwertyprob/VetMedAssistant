using System.ComponentModel.DataAnnotations;

namespace Medcard.Mvc.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Заполните это поле")]
        [EmailAddress(ErrorMessage = "Введите корректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string Password { get; set; }
    }
}
