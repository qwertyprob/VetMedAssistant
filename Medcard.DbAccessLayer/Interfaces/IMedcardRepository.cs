using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Interfaces
{

    public interface IMedcardRepository<TOwner, TPet, TDrug, TTreatment>
    {
        
        Task<List<TOwner>> GetAllAsync();
        Task<TOwner> GetByIdAsync(Guid id);
    }

}