using Medcard.Client.Abstraction;
using Medcard.Client.Models;
using System.Net.Http;

namespace Medcard.Client.Services
{
    

    public class MedcardHttpService : IMedcardHttpService
    {
        private readonly IHttpClientFactory _httpClient;

        public MedcardHttpService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
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
