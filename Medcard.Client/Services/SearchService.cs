using Medcard.Client.Abstractions;
using Medcard.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Medcard.Client.Services
{
    public class SearchService : ISearchService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly NavigationManager _navigationManager;

        public SearchService(IHttpClientFactory httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;

        }

        //GET 
        public async Task<IReadOnlyCollection<OwnerModel>> SearchAsync(string searchItem)
        {
            var client = _httpClient.CreateClient("Medcard");
            var response = await client.GetFromJsonAsync<IReadOnlyCollection<OwnerModel>>($"search/{searchItem}");
            if (client.BaseAddress == null)
            {
                return null;
            }

            return response ?? new List<OwnerModel>();

        }
    }
}
