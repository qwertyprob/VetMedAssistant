using Medcard.Client.Abstractions;
using Medcard.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Medcard.Client.Services
{
    public class SearchService : ISearchService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly TokenService _tokenService;

        public SearchService(IHttpClientFactory httpClient, NavigationManager navigationManager, TokenService tokenService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _tokenService = tokenService;
        }
        private HttpClient GetClientWithAuth()
        {
            var client = _httpClient.CreateClient("Medcard");

            // Получаем токен
            var token = _tokenService.GetToken();

            if (_tokenService.HasToken())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _navigationManager.NavigateTo("/Login");
            }

            return client;
        }
        //GET 
        public async Task<IReadOnlyCollection<OwnerModel>> SearchAsync(string searchItem)
        {
            var client = GetClientWithAuth();
            var response = await client.GetFromJsonAsync<IReadOnlyCollection<OwnerModel>>($"search/{searchItem}");
            if (client.BaseAddress == null)
            {
                return null;
            }

            return response ?? new List<OwnerModel>();

        }
    }
}
