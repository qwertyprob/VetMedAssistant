using Medcard.DbAccessLayer.Dto;
using Medcard.Bl.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.Bl.Abstraction
{
    public interface IMedcardService
    {
        Task<IReadOnlyCollection<OwnerModel>> GetAllAsync();
        Task<OwnerModel> GetByIdAsync(Guid id);
        Task<OwnerModel> CreateAsync(MedcardViewModel medcardViewModel);
        Task<bool> DeleteAsync(Guid id);
        Task<OwnerModel> UpdateAsync(Guid id, MedcardViewModel medcardViewModel);
        Task<string> UpdateTestsAsync(Guid id, string tests);
        Task<string> UpdateDrugsAsync(Guid id, string Drugs);
        Task<string> UpdateTreatAsync(Guid id, string Treatments);
        Task<string> UpdateRecAsync(Guid id, string Recomendations);
    }
}