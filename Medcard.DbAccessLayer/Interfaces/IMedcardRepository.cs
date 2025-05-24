using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Interfaces
{

    public interface IMedcardRepository
    {
        
        Task<IReadOnlyCollection<OwnerDto>> GetAllAsync();
        Task<OwnerDto> GetByIdAsync(Guid id);
        Task<OwnerDto> CreateAsync(MedcardViewModel medcardViewModel);
        Task<OwnerDto> UpdateAsync(Guid id,MedcardViewModel medcardViewModel);
        Task<string> UpdateTestsAsync(Guid id, string text);
        Task<string> UpdateDrugsAsync(Guid id, string Drugs);
        Task<string> UpdateRecAsync(Guid id, string Recomendations);
        Task<string> UpdateTreatAsync(Guid id, string Treatment);
        Task<bool> DeleteAsync(Guid id);
    }

}