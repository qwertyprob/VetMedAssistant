using Medcard.DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class MedcardService : IMedcardService
{
    private readonly IRepository _repository;

    public MedcardService(IRepository repository)
    {
        _repository = repository;

    }

    public async Task<OwnerEntity> CreateMedcardAsync(string name, string phone,
                                                       string petName, int chipNumber,
                                                       int petAge, string petBreed,
                                                       string petDrugs = "Здесь пока пусто!",
                                                       string petTreatment = "Здесь пока пусто!")
    {
        return await _repository.CreateAsync(name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);
    }

    public async Task<List<OwnerEntity>> GetMedcardsAsync()
    {
        return await _repository.GetAsync();
    }

    public async Task<OwnerEntity> UpdateMedcardAsync(Guid id, string name, string phone,
                                                       string petName, int chipNumber,
                                                       int petAge, string petBreed,
                                                       string petDrugs,
                                                       string petTreatment)
    {
        return await _repository.UpdateAsync(id, name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);
    }

    public async Task<OwnerEntity> DeleteMedcard(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<OwnerEntity> GetMedcardById(Guid id)
    {
        return await _repository.GetByIdAsync(id);

    }





}
