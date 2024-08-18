using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Medcard.Mvc.Controllers
{
    public class TemplateController : Controller
    {
        private IMedcardServiceMvc _medcardService;
        private ILogger<TemplateController> _logger;

        public TemplateController(IMedcardServiceMvc medcardservice, ILogger<TemplateController> logger)
        {
            _medcardService = medcardservice;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
