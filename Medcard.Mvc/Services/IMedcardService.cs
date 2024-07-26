using Medcard.DbAccessLayer.Entities;
using Medcard.Mvc.Models; 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedcardService
{
    Task<OwnerModel> CreateMedcardAsync(string name, string phone, string petName, int chipNumber, int petAge, string petBreed, string petDrugs = "Здесь пока пусто!", string petTreatment = "Здесь пока пусто!");
    Task<OwnerEntity> DeleteMedcardAsync(Guid id);
    Task<OwnerModel> GetMedcardById(Guid id);
    Task<IReadOnlyCollection<OwnerModel>> GetMedcardsAsync();
    Task<OwnerModel> UpdateMedcardAsync(Guid id,string name, string phone, string petName, int chipNumber, int petAge, string petBreed, string petDrugs, string petTreatment);
}