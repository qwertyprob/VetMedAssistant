using Medcard.DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedcardService
{
    Task<OwnerEntity> GetMedcardById(Guid id);
    Task<OwnerEntity> CreateMedcardAsync(string name, string phone, string petName, int chipNumber, int petAge, string petBreed, string petDrugs = "Здесь пока пусто!", string petTreatment = "Здесь пока пусто!");
    Task<OwnerEntity> DeleteMedcard(Guid id);
    Task<List<OwnerEntity>> GetMedcardsAsync();
    Task<OwnerEntity> UpdateMedcardAsync(Guid id, string name, string phone, string petName, int chipNumber, int petAge, string petBreed, string petDrugs, string petTreatment);
}