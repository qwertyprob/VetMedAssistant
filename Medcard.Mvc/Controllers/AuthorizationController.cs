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
        public async Task<IActionResult> Login(string email, string password)
        {
            if(!ModelState.IsValid) { return NotFound(); }
            var user = _authService.Login(email, password);


            return RedirectToAction("Medcard");

        }
    }
}
