using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Medcard.DbAccessLayer
{
    

    public class MedcardRepository : IMedcardRepository <OwnerDto, PetDto, DrugsDto, TreatmentsDto>
    {
        private readonly AppDbContext _dbcontext;
        public MedcardRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<OwnerDto>> GetAllAsync()
        {
            var medcard = await _dbcontext.Owners
                .Include(p => p.Pets)
                    .ThenInclude(d => d.Drugs)
                .Include(p => p.Pets)
                    .ThenInclude(t => t.Treatments)
                .AsNoTracking()
                .ToListAsync();


            return new List<OwnerDto>(); 


        }
        public async Task<OwnerDto> GetByIdAsync(Guid id)
        {
            var medcard = await _dbcontext.Owners
                .Include(p => p.Pets)
                    .ThenInclude(d => d.Drugs)
                .Include(p => p.Pets)
                    .ThenInclude(t => t.Treatments)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);


            return new OwnerDto();


        }


    }
}
