using Medcard.DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository
{
    Task<OwnerEntity> CreateAsync(string name, string phone, string petName, int chipNumber, int petAge, string petBreed, string petDrugs, string petTreatment);
    Task<OwnerEntity> DeleteAsync(Guid id);
    Task<IReadOnlyCollection<OwnerEntity>> GetAsync();
    Task<OwnerEntity> GetByIdAsync(Guid id);
    Task<OwnerEntity> UpdateAsync(Guid id,string name, string phone, string petName, int chipNumber, int petAge, string petBreed, string petDrugs, string petTreatment);
}