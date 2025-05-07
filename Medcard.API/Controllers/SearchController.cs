using Medcard.Bl.Abstraction;
using Medcard.Bl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medcard.Api.Controllers
{
    [ApiController]
    [Route("/api/search/{searchItem}")]
    [Authorize]

    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService  = searchService;
        }
        [HttpGet]
        public async Task<IActionResult> SearchAsync(string searchItem)
        {
            
            var results = await _searchService.GetAllFromSearchAsync(searchItem);
            return Ok(results);
        }
    }
}
