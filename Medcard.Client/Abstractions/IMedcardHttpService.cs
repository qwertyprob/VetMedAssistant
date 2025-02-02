using Medcard.Client.Models;

namespace Medcard.Client.Abstraction
{
    public interface IMedcardHttpService
    {
        Task<List<OwnerModel>> GetAllFromApi();
    }
}
