using Medcard.Client.Abstraction;
using Medcard.Client.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Medcard.Client.Services
{
    

    public class MedcardHttpService : IMedcardHttpService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly NavigationManager _navigationManager;

        public MedcardHttpService(IHttpClientFactory httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;

    }

        public async Task<List<OwnerModel>> GetAllFromApi()
        {
            

            try
            {
                var client = _httpClient.CreateClient("Medcard");


                if (client.BaseAddress == null)
                {
                    return null;
                }
               


                var response = await client.GetFromJsonAsync<List<OwnerModel>>($"{client.BaseAddress}/get");


                return response ?? new List<OwnerModel>();
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

    }
}
