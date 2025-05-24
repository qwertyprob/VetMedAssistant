using Medcard.Client.Models;

namespace Medcard.Client.Abstraction
{
    public interface IMedcardHttpService
    {
        Task<List<OwnerModel>> GetAllFromApiAsync();
        Task<bool> DeleteMedcardAsync(Guid id);
       
        Task<OwnerModel> GetMedcardById(Guid id);
        Task<OwnerModel> CreateMedcardAsync(MedcardViewModel request);
        Task<OwnerModel> UpdateMedcardAsync(Guid id, MedcardViewModel model);
        Task<bool> UpdateDrugsAsync(Guid id, string text);
        Task<bool> UpdateTreatAsync(Guid id, string text);
        Task<bool> UpdateRecAsync(Guid id, string text);
        Task<bool> UpdateTestsAsync(Guid id, string text);
    }
}
