using Medcard.Client.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Text;

namespace Medcard.Client.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly NavigationManager _navigationManager;
        public UserService(IHttpClientFactory httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;

        }

        //LOGIN
        public async Task<string> Login(LoginViewModel model)
        {
            if (model == null)
            {
                return "Пустой юзер";
            }

            if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Email))
            {
                return "Пустой логин или пароль";
            }

            var client = _httpClient.CreateClient("Medcard");

            var requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("user/login", requestContent);

            

            if (!response.IsSuccessStatusCode)
            {
                return $"Неправильные данные";
            }



            var content = await response.Content.ReadAsStringAsync();

            return "Успешный логин";

        }
    }
}
