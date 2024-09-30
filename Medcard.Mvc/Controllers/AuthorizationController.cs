using Medcard.Mvc.Models;
using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Http;
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
        [Route("/Authorization")]
        public IActionResult Auth()
        {
            return View("Auth");
        }
       


        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password)
        {
            
            var user = await _authService.CreateUser(email, password);
              
            
            return RedirectToAction("Auth",user); 

        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Auth", model); 
            }

            var user = _authService.Login(model.Email, model.Password);
            HttpContext.Session.SetString("userRole", "Admin");
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Auth", model); 
            }

            
            return Redirect("/");
        }

    }
}
