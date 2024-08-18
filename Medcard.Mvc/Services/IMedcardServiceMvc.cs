using Medcard.DbAccessLayer.Dto;
using Medcard.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.Mvc.Services
{
    public interface IMedcardServiceMvc
    {
        Task<IReadOnlyCollection<OwnerModel>> GetAllAsync();
        Task<OwnerModel> GetByIdAsync(Guid id);
        Task<OwnerModel> CreateAsync(MedcardViewModel medcardViewModel);
        Task<OwnerModel> UpdateAsync(Guid id, MedcardViewModel medcardViewModel);
        Task<OwnerModel> UpdateNew(Guid id, string drugs, string treatments);
        Task<bool> DeleteAsync(Guid id);
    }
}