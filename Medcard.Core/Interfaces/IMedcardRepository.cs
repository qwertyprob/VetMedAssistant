using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.Core.Interfaces
{

    public interface IMedcardRepository<TOwner, TPet, TDrug, TTreatment>
    {
        
        Task<List<TOwner>> GetAllAsync();
    }

}