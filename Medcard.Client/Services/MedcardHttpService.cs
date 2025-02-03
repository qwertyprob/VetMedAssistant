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
                        Id= ownerId,
                        Name = medcardViewModel.PetName,
                        ChipNumber = medcardViewModel.ChipNumber,
                        Age = medcardViewModel.Age,
                        Breed = medcardViewModel.Breed,
                        Drugs = new List<DrugsModel>()
                        {
                            new DrugsModel()
                            {
                                PetId = ownerId,
                                Description = medcardViewModel.Drugs
                            }
                        },
                        Treatments = new List<TreatmentsModel>()
                        {
                            new TreatmentsModel()
                            {
                                PetId = ownerId,
                                Description = medcardViewModel.Treatments
                            }
                        },
                        Recomendations = new List<RecomendationsModel>()
                        {
                            new RecomendationsModel()
                            {
                                PetId = ownerId,
                                Description = medcardViewModel.Recomendations
                            }
                        }

                    }
                }
            };


            try
            {
                var response = await client.PostAsJsonAsync<OwnerModel>($"{client.BaseAddress}/create", mappedOwner);

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
