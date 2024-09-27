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
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password)
        {
            var user = await _authService.CreateUser(email, password);

            return View("Index",user); 

        }
    }
}
