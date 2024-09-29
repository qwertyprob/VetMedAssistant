using Microsoft.AspNetCore.Mvc;

namespace Medcard.Mvc.Controllers
{
    public class HostingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
