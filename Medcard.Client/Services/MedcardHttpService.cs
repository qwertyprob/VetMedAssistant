using Medcard.Client.Abstraction;
using Medcard.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

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
        //GET
        public async Task<List<OwnerModel>> GetAllFromApiAsync()
        {
            
                var client = _httpClient.CreateClient("Medcard");

                var response = await client.GetFromJsonAsync<List<OwnerModel>>(client.BaseAddress+"get");


                if (client.BaseAddress == null)
                {
                    return null;
                }
               
                return response ?? new List<OwnerModel>();
            
        }
        public async Task<OwnerModel> GetMedcardById(Guid id)
        {
            var client = _httpClient.CreateClient("Medcard");

            var response = await client.GetFromJsonAsync<OwnerModel>(client.BaseAddress + $"get/{id}");
            if(response == null)
            {
                return new OwnerModel();
            }

            return response;

        }

        //CREATE
        public async Task<OwnerModel> CreateMedcardAsync(MedcardViewModel request)
        {
            var client = _httpClient.CreateClient("Medcard");

            var json = JsonConvert.SerializeObject(request);

            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("create", requestContent);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error response: {response.StatusCode}");
                return null;
            }


            var contentBody = await response.Content.ReadAsStringAsync();


            if (string.IsNullOrWhiteSpace(contentBody))
            {
                Console.WriteLine("Empty response body");
                return null;
            }

            var responseBody = JsonConvert.DeserializeObject<OwnerModel>(contentBody);

            Console.WriteLine(responseBody.Name);


            return responseBody;



        }
        //UPDATE
        public async Task<OwnerModel> UpdateMedcardAsync(Guid id, MedcardViewModel model)
        {
            var client = _httpClient.CreateClient("Medcard");

            

            var requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Debugging the request content
            Console.WriteLine($"Sending request body: {await requestContent.ReadAsStringAsync()}");

            var response = await client.PutAsync($"update/{id}", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error response: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                return null;
            }

            var contentBody = await response.Content.ReadAsStringAsync();


            if (string.IsNullOrWhiteSpace(contentBody))
            {
                Console.WriteLine("Empty response body");
                return null;
            }

            var responseBody = JsonConvert.DeserializeObject<OwnerModel>(contentBody);

            Console.WriteLine($"Response body: {responseBody.Name}");

            return responseBody;
        }
        public async Task<bool> UpdateDrugsAsync(Guid id, string text)
        {
            var client = _httpClient.CreateClient("Medcard");

            var request = new HttpRequestMessage(HttpMethod.Put, $"drugs/{id}?text={Uri.EscapeDataString(text)}");

            request.Headers.Add("Accept", "*/*");

            // Empty body
            request.Content = new StringContent(string.Empty);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return false;
            }

            var json = await response.Content.ReadAsStringAsync();
            return true;
        }
        public async Task<bool> UpdateTreatAsync(Guid id, string text)
        {
            var client = _httpClient.CreateClient("Medcard");

            var request = new HttpRequestMessage(HttpMethod.Put, $"treatments/{id}?text={Uri.EscapeDataString(text)}");

            request.Headers.Add("Accept", "*/*");

            // Empty body
            request.Content = new StringContent(string.Empty);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return false;
            }

            var json = await response.Content.ReadAsStringAsync();
            return true;
        }
        public async Task<bool> UpdateRecAsync(Guid id, string text)
        {
            var client = _httpClient.CreateClient("Medcard");

            var request = new HttpRequestMessage(HttpMethod.Put, $"recomendations/{id}?text={Uri.EscapeDataString(text)}");

            request.Headers.Add("Accept", "*/*");

            // Empty body
            request.Content = new StringContent(string.Empty);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return false;
            }

            var json = await response.Content.ReadAsStringAsync();
            return true;
        }



        //DELETE
        public async Task<bool> DeleteMedcardAsync(Guid id)
        {
            var client = _httpClient.CreateClient("Medcard");

           
                var response = await client.DeleteAsync(client.BaseAddress + $"delete/{id}");
                return true;
            
        }
    }
}
