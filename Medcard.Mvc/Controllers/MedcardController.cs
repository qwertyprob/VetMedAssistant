using Medcard.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MedcardMvc.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

public class MedcardController : Controller
{
    private readonly IMedcardService _medcardService;

    public MedcardController(IMedcardService medcardService)
    {
        _medcardService = medcardService;
    }

    [HttpGet]
    public async Task<IActionResult> More(Guid ownerId)
    {
        var ownerEntity = await _medcardService.GetMedcardById(ownerId);

        if (ownerEntity == null)
        {
            return NotFound();
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

        return View(ownerModel);
    }


    public async Task<IActionResult> Index()
    {
        var medcards = await _medcardService.GetMedcardsAsync();
        var medcardModels = MapToModels(medcards);

        return View(medcardModels);
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
