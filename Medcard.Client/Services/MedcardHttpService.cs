using Medcard.Client.Abstraction;
using Medcard.Client.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Medcard.Client.Services
{
    public class MedcardHttpService : IMedcardHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MedcardHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<OwnerModel>> GetAllFromApiAsync()
        {
            var client = _httpClientFactory.CreateClient("Medcard"); 

            try
            {
                var response = await client.GetFromJsonAsync<List<OwnerModel>>(client.BaseAddress+"/get");
                return response ?? new List<OwnerModel>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка запроса: {ex.Message}");
                return new List<OwnerModel>();
            }
        }
        public async Task<OwnerModel> CreateMedcardAsync(MedcardViewModel medcardViewModel)
        {
            var client = _httpClientFactory.CreateClient("Medcard");

            var MappedOwner = new OwnerModel
            {
                Id = Guid.NewGuid(),
                Name = medcardViewModel.OwnerName,
                PhoneNumber = medcardViewModel.PhoneNumber,
                DateCreate = medcardViewModel.DateCreate,
                Pets = new List<PetModel>
                {
                    new PetModel()
                    {
                        Id= medcardViewModel.Id,
                        Name = medcardViewModel.PetName,
                        ChipNumber = medcardViewModel.ChipNumber,
                        Age = medcardViewModel.Age,
                        Breed = medcardViewModel.Breed,
                        Drugs = new List<DrugsModel>()
                        {
                            new DrugsModel()
                            {
                                PetId = medcardViewModel.Id,
                                Description = medcardViewModel.Drugs
                            }
                        },
                        Treatments = new List<TreatmentsModel>()
                        {
                            new TreatmentsModel()
                            {
                                PetId = medcardViewModel.Id,
                                Description = medcardViewModel.Treatments
                            }
                        },
                        Recomendations = new List<RecomendationsModel>()
                        {
                            new RecomendationsModel()
                            {
                                PetId = medcardViewModel.Id,
                                Description = medcardViewModel.Recomendations
                            }
                        }

                    }
                }
            };
            try
            {
                var response = await client.PostAsJsonAsync<OwnerModel>($"{client.BaseAddress}/create", MappedOwner);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OwnerModel>();
                    return result; 
                }
                else { return new OwnerModel(); }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запроса: {ex.Message}");

                return new OwnerModel();
                
            }
        }
        public async Task<bool> DeleteMedcardAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient("Medcard");

            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + $"/delete/{id}");
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка запроса: {ex.Message}");
                return false;
            }
        }
    }
}
