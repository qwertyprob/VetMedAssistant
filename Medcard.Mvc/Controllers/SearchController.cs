using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Medcard.Mvc.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMedcardServiceMvc _medcardService;
        private readonly ILogger<MedcardController> _logger;

        public SearchController(IMedcardServiceMvc medcardServiceMvc, ILogger<MedcardController> logger)
        {
            _medcardService = medcardServiceMvc;
            _logger = logger;

        }
        [HttpPost]
        public IActionResult SearchMedcardPost(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                return RedirectToAction("Index","Medcard"); 
            }

            return RedirectToAction("SearchMedcard", new { clientName });
        }

        [HttpGet("Search/{clientName}")]
        public async Task<IActionResult> SearchMedcard(string clientName)
        {


            ViewBag.ClientName = clientName;

            ViewBag.ClientName = clientName.Trim();


            var medcards = await _medcardService.GetAllFromSearchAsync(clientName.Trim());




            return View("SearchMedcard", medcards);
        }
    }
}
