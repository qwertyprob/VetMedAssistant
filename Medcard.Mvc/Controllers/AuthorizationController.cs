using Medcard.Mvc.Abstractions;
using Medcard.Mvc.Models;
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


            return Redirect("/");

        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Auth", model); 
            }

            var user = _authService.Login(model.Email, model.Password);
            SetRoleInSession();
             
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Auth", model);
            }


            return Redirect("/");
        }
        //Перенес в AuthService
        [HttpGet]
        public void SetRoleInSession()
        {
            _authService.SetRoleSession();
        }

    }
}
