using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Interfaces
{
    public interface IMedcardService
    {
        Task<IReadOnlyCollection<OwnerDto>> GetAllAsync();
        Task<OwnerDto> GetByIdAsync(Guid id);
        Task<OwnerDto> CreateAsync(MedcardViewModel medcardViewModel);
        Task<OwnerDto> UpdateAsync(Guid id,MedcardViewModel medcardViewModel);
        Task<bool> DeleteAsync(Guid id);
    }
}