using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Interfaces
{

    public interface IMedcardRepository<T>
    {
        
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(MedcardViewModel medcardViewModel);
        Task<T> UpdateAsync(Guid id,MedcardViewModel medcardViewModel);
        Task<T> UpdateNoDrugsNoTreatmentsAsync(Guid id, MedcardViewModel medcardViewModel);
        Task<T> UpdateDrugsAndTreatments(Guid id, string Drugs, string Treatments,string Recomendations);
        Task<bool> DeleteAsync(Guid id);
    }

}