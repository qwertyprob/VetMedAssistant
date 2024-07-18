using Microsoft.AspNetCore.Mvc;

namespace Medcard.Mvc.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
