using Medcard.DbAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MedcardRepository : IRepository
{
    private readonly AppDbContext _dbcontext;
    public MedcardRepository(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task<IReadOnlyCollection<OwnerEntity>> GetAsync()
    {
        var medcard = await _dbcontext.Owners
            .Include(owner => owner.Pets)
                .ThenInclude(pet => pet.Drugs)
            .Include(pet => pet.Pets)
                .ThenInclude(pet => pet.Treatments)
            .AsNoTracking()
            .ToListAsync();



        return medcard;

    }
    public async Task<OwnerEntity> GetByIdAsync(Guid id)
    {
        var owner = await _dbcontext.Owners
            .Include(o => o.Pets)
                .ThenInclude(p => p.Drugs)
            .Include(o => o.Pets)
                .ThenInclude(p => p.Treatments)
            .SingleOrDefaultAsync(x => x.Id == id);

        return owner;
    }


    public async Task<OwnerEntity> CreateAsync(
        string name, string phone,
        string petName, int chipNumber,
        int petAge, string petBreed,
        string petDrugs,
        string petTreatment)
    {
        Guid id = Guid.NewGuid();

        var medcard = new OwnerEntity()
        {
            Id = id,
            Name = name,
            PhoneNumber = phone,
            Pets = new List<PetEntity>
        {
            new PetEntity
            {
                Id = Guid.NewGuid(),
                OwnerId = id,
                Name = petName,
                ChipNumber = chipNumber,
                Age = petAge,
                Breed = petBreed,
                Treatments = new List<TreatmentEntity>
                {
                    new TreatmentEntity
                    {
                        Description = petTreatment,
                        PetId = id // Использование id владельца
                    }
                },
                Drugs = new List<DrugEntity>
                {
                    new DrugEntity
                    {
                        Description = petDrugs,
                        PetId = id // Использование id владельца
                    }
                }
            }
        }
        };

        _dbcontext.Add(medcard);
        await _dbcontext.SaveChangesAsync();

        return medcard;
    }


    public async Task<OwnerEntity> UpdateAsync(Guid id,
     string name, string phone,
     string petName, int chipNumber,
     int petAge, string petBreed,
     string petDrugs,
     string petTreatment)
    {

        var updateMedcard = await _dbcontext.Owners
            .Include(o => o.Pets)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        if (updateMedcard != null)
        {

            updateMedcard.Name = name;
            updateMedcard.PhoneNumber = phone;


            var petToUpdate = updateMedcard.Pets.FirstOrDefault();
            if (petToUpdate != null)
            {
                petToUpdate.Name = petName;
                petToUpdate.ChipNumber = chipNumber;
                petToUpdate.Age = petAge;
                petToUpdate.Breed = petBreed;


                petToUpdate.Drugs.Clear();
                petToUpdate.Treatments.Clear();

                petToUpdate.Drugs.Add(new DrugEntity { Description = petDrugs });
                petToUpdate.Treatments.Add(new TreatmentEntity { Description = petTreatment });
            }


            await _dbcontext.SaveChangesAsync();
        }

        return updateMedcard;
    }




    public async Task<OwnerEntity> DeleteAsync(Guid id)
    {
        var deletedMedcard = await _dbcontext.Owners
            .Include(o => o.Pets)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        if (deletedMedcard != null)
        {
            _dbcontext.Owners.Remove(deletedMedcard);
            await _dbcontext.SaveChangesAsync();
        }
        return deletedMedcard;
    }

    public Task<OwnerEntity> UpdateAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    
}
