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
        Task<OwnerModel> UpdateAsync(Guid id, MedcardViewModel medcardViewModel);
        Task UpdateDrugsAsync(Guid petId, string drugs);
        Task UpdateTreatmentsAsync(Guid petId, string treatments);
        Task UpdateRecomendAsync(Guid petId, string recomendations);
        Task<bool> DeleteAsync(Guid id);
        Task<Guid> SearchByNameAsync(string name);
    }
}