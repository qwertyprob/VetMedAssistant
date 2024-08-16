using Medcard.DbAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medcard.Core.Interfaces;

namespace Medcard.DbAccessLayer
{
    

    public class MedcardRepository : IMedcardRepository <OwnerEntity, PetEntity, DrugEntity, TreatmentEntity>
    {
        private readonly AppDbContext _dbcontext;
        public MedcardRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<OwnerEntity>> GetAllAsync()
        {
            var medcard = await _dbcontext.Owners
                .Include(p => p.Pets)
                    .ThenInclude(d => d.Drugs)
                .Include(p => p.Pets)
                    .ThenInclude(t => t.Treatments)
                .ToListAsync();

            return medcard;


        }

       
    }
}
