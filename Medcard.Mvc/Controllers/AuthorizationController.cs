using Medcard.Mvc.Models;
using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Medcard.Mvc.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthServiceMvc _authService;

        public AuthorizationController(IAuthServiceMvc authService)
        {
            _authService = authService;
        }
        public IActionResult Auth()
        {
            return View("Auth");
        }
        public IActionResult Medcard()
        {
            return RedirectToAction("Index", "Medcard");
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password)
        {
            
            var user = await _authService.CreateUser(email, password);
              
            
            return RedirectToAction("Auth",user); 

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Auth", model); // Возвращаем обратно с моделью, если есть ошибки валидации
            }

            var user = _authService.Login(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Auth", model); // Возвращаем обратно с ошибкой
            }

            // Если пользователь успешно вошел в систему
            return RedirectToAction("Medcard");
        }

    }
}
