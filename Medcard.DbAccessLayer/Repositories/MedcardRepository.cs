using Medcard.DbAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medcard.Core.Models;

namespace Medcard.DbAccessLayer
{
    public class MedcardRepository : IMedcardRepository
    {
        private readonly AppDbContext _dbcontext;
        public MedcardRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<OwnerModel>> GetAll()
        {
            var medcardEntity = await _dbcontext.Owners
                .Include(p => p.Pets)
                    .ThenInclude(d => d.Drugs)
                .Include(p => p.Pets)
                    .ThenInclude(t => t.Treatments)
                .AsNoTracking()
                .ToListAsync();

            //Преобразование
            var medcard = medcardEntity
                .Select(x => OwnerModel.Create(
                    x.Id = Guid.NewGuid(),
                    x.Name,
                    x.PhoneNumber,
                    x.Pets.Select(p => new PetModel
                    {
                        Id = Guid.NewGuid(),
                        Name = p.Name,
                        ChipNumber = p.ChipNumber,
                        Age = p.Age,
                        Breed = p.Breed,
                        Drugs = p.Drugs.Select(d => new DrugsModel
                        {
                            Description = d.Description,
                            PetId = p.Id
                        }).ToList(),
                        Treatments = p.Treatments.Select(t => new TreatmentsModel
                        {
                            Description = t.Description,
                            PetId = p.Id
                        }).ToList()
                    }).ToList()
                )).ToList();

            return medcard;
        }



    }
}
