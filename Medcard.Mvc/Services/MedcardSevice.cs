using Medcard.DbAccessLayer.Entities;
using Medcard.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class MedcardService : IMedcardService
{
    private readonly IRepository _repository;


    public MedcardService(IRepository repository)
    {
        _repository = repository;

    }

    public async Task<OwnerModel> CreateMedcardAsync(string name, string phone,
                                                string petName, int chipNumber,
                                                int petAge, string petBreed,
                                                string petDrugs = "Здесь пока пусто!",
                                                string petTreatment = "Здесь пока пусто!")
    {
        var medcard = await _repository.CreateAsync(name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);

        var ownerModel = new OwnerModel
        {
            Id = medcard.Id,
            Name = medcard.Name,
            PhoneNumber = medcard.PhoneNumber,
            Pets = new List<PetModel>
    {
        new PetModel
        {
            Name = petName,
            ChipNumber = chipNumber,
            Age = petAge,
            Breed = petBreed,
            Drugs = new List<DrugModel>
            {
                new DrugModel
                {
                Description = petDrugs
                }
            },

            Treatments = new List<TreatmentModel>
            {
                new TreatmentModel
                {
                Description = petTreatment
                }
            }
        }
    }
        };



        return ownerModel;
    }


    public async Task<IReadOnlyCollection<OwnerModel>> GetMedcardsAsync()
    {
        var medcards = await _repository.GetAsync();
        var ownerModel = MapToModels(medcards);

        return ownerModel;
    }

    public async Task<OwnerModel> UpdateMedcardAsync(Guid id,string name, string phone,
                                                 string petName, int chipNumber,
                                                 int petAge, string petBreed,
                                                 string petDrugs,
                                                 string petTreatment)
    {

        return null;        
    }


    public async Task<OwnerEntity> DeleteMedcardAsync(Guid id)
    {
        var ownerModel = await _repository.DeleteAsync(id);

        return ownerModel;

    }

    public async Task<OwnerModel> GetMedcardById(Guid id)
    {
        var ownerEntity = await _repository.GetByIdAsync(id);

        if (ownerEntity == null)
        {
            return null;
        }

        var ownerModel = new OwnerModel
        {
            Id = ownerEntity.Id,
            Name = ownerEntity.Name,
            PhoneNumber = ownerEntity.PhoneNumber,
            Pets = ownerEntity.Pets.Select(pet => new PetModel
            {
                Id = pet.Id,
                Name = pet.Name,
                ChipNumber = pet.ChipNumber,
                Age = pet.Age,
                Breed = pet.Breed,
                Drugs = pet.Drugs.Select(drug => new DrugModel
                {
                    Id = drug.Id,
                    Description = drug.Description
                }).ToList(),
                Treatments = pet.Treatments.Select(treatment => new TreatmentModel
                {
                    Id = treatment.Id,
                    Description = treatment.Description
                }).ToList()
            }).ToList()
        };
        return ownerModel;
    }
    private List<OwnerModel> MapToModels(List<OwnerEntity> entities)
    {
        var models = new List<OwnerModel>();
        foreach (var entity in entities)
        {
            var ownerModel = new OwnerModel
            {
                Id = entity.Id,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Pets = entity.Pets.Select(p => new PetModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ChipNumber = p.ChipNumber,
                    Age = p.Age,
                    Breed = p.Breed,
                    Drugs = p.Drugs.Select(d => new DrugModel
                    {
                        Id = d.Id,
                        Description = d.Description
                    }).ToList(),
                    Treatments = p.Treatments.Select(t => new TreatmentModel
                    {
                        Id = t.Id,
                        Description = t.Description
                    }).ToList()
                }).ToList()
            };
            models.Add(ownerModel);
        }
        return models;
    }

    
}
