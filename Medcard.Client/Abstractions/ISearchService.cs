using Medcard.Client.Models;

namespace Medcard.Client.Abstractions
{
    public interface ISearchService
    {
        Task<IReadOnlyCollection<OwnerModel>> SearchAsync(string searchItem);
    }
}