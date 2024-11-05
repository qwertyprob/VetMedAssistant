using Medcard.Mvc.Abstractions;
using Medcard.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Medcard.Mvc.Controllers
{
    //[AuthorizeRole("Admin")]
    public class SearchController : Controller
    {
        private readonly IMedcardServiceMvc _medcardService;
        private readonly ISearchServiceMvc _searchService;
        private readonly ILogger<MedcardController> _logger;

        public SearchController(IMedcardServiceMvc medcardServiceMvc, ILogger<MedcardController> logger, ISearchServiceMvc searchService)
        {
            _medcardService = medcardServiceMvc;
            _logger = logger;
            _searchService = searchService;
        }
        [HttpPost]
        public IActionResult SearchMedcardPost(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem))
            {
                return RedirectToAction("Index","Medcard"); 
            }

            return RedirectToAction("SearchMedcard", new { searchItem });
        }

        [HttpGet("Search/{searchItem}")]
        public async Task<IActionResult> SearchMedcard(string searchItem)
        {

            ViewBag.SearchItem = searchItem.Trim();

            var medcards = await _searchService.GetAllFromSearchAsync(searchItem.Trim());

            return View("SearchMedcard", medcards);
        }


    }
}
