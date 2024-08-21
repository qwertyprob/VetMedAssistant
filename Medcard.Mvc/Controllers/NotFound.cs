using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Medcard.Mvc.Controllers
{
    public class NotFound : Controller
    {
        private IMedcardServiceMvc _medcardService;
        private ILogger<NotFound> _logger;

        public NotFound(IMedcardServiceMvc medcardservice, ILogger<NotFound> logger)
        {
            _medcardService = medcardservice;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View("Index");
        }

    }
}
