using Microsoft.AspNetCore.Mvc;

namespace Medcard.Mvc.Controllers
{
    public class AuthorizationController : Controller
    {        
        public IActionResult Authorize()
        {
            return View();
        }
    }
}
