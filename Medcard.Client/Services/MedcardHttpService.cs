using Medcard.Client.Abstraction;
using Medcard.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<List<OwnerModel>> GetAllFromApiAsync()
        {
            
                var client = _httpClient.CreateClient("Medcard");

                var response = await client.GetFromJsonAsync<List<OwnerModel>>(client.BaseAddress+"/get");


                if (client.BaseAddress == null)
                {
                    return null;
                }
               
                return response ?? new List<OwnerModel>();
            
        }
        public async Task<OwnerModel> GetMedcardById(Guid id)
        {
            var client = _httpClient.CreateClient("Medcard");

            var response = await client.GetFromJsonAsync<OwnerModel>(client.BaseAddress + $"/get/{id}");
            if(response == null)
            {
                return new OwnerModel();
            }

            return response;


        }
        public async Task<OwnerModel> UpdateMedcardAsync(Guid id,OwnerModel model)
        {
            var client = _httpClient.CreateClient("Medcard");

            var updateModel = new OwnerModel()
            {
                Id = id,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Pets = new List<PetModel>()
                {
                    new PetModel()
                    {
                        Name = model.Pets.First().Name,
                        Age = model.Pets.First().Age,
                        Breed = model.Pets.First().Breed,
                        ChipNumber = model.Pets.First().ChipNumber
                    }
                }
            };

            var owner = await client.PostAsJsonAsync<OwnerModel>(client.BaseAddress + $"/update/{id}", updateModel);


            return updateModel;


        }
        public async Task<OwnerModel> CreateMedcardAsync(MedcardViewModel medcardViewModel)
        {
            var client = _httpClient.CreateClient("Medcard");
            Guid ownerId = Guid.NewGuid();

            var mappedOwner = new OwnerModel
            {
                Id = ownerId,
                Name = medcardViewModel.OwnerName,
                PhoneNumber = medcardViewModel.PhoneNumber,
                DateCreate = medcardViewModel.DateCreate,
                Pets = new List<PetModel>
                {
                    new PetModel()
                    {
                        Name = medcardViewModel.PetName,
                        ChipNumber = medcardViewModel.ChipNumber,
                        Age = medcardViewModel.Age,
                        Breed = medcardViewModel.Breed,
                        Drugs = new List<DrugsModel>
                        {
                            new DrugsModel {  Description = medcardViewModel.Drugs }
                        },
                        Treatments = new List<TreatmentsModel>
                        {
                            new TreatmentsModel {  Description = medcardViewModel.Treatments }
                        },
                        Recomendations = new List<RecomendationsModel>
                        {
                            new RecomendationsModel {  Description = medcardViewModel.Recomendations }
                        }
                    }
                }
            };

            
                Console.WriteLine($"Отправка запроса на {client.BaseAddress}/create");
                var response = await client.PostAsJsonAsync($"{client.BaseAddress}/create", mappedOwner);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OwnerModel>();

                    return result;
                }

                Console.WriteLine($"Ошибка API: {response.StatusCode}");
                return new OwnerModel();
           
        }

        public async Task<bool> DeleteMedcardAsync(Guid id)
        {
            var client = _httpClient.CreateClient("Medcard");

           
                var response = await client.DeleteAsync(client.BaseAddress + $"/delete/{id}");
                return true;
            
        }
    }
}
