using Medcard.Client.Models;

namespace Medcard.Client.Abstraction
{
    public interface IMedcardHttpService
    {
        Task<List<OwnerModel>> GetAllFromApiAsync();
        Task<bool> DeleteMedcardAsync(Guid id);
        Task<OwnerModel> CreateMedcardAsync(MedcardViewModel medcardViewModel);

    }
}
