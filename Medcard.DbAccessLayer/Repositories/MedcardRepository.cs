using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace Medcard.DbAccessLayer
{
    

    public class MedcardRepository : IMedcardRepository 
    {
        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;

        public MedcardRepository(AppDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<OwnerDto>> GetAllAsync()
        {
            var medcard = await _dbcontext.Owners
                .Include(p => p.Pets)
                    .ThenInclude(d => d.Drugs)
                .Include(p => p.Pets)
                    .ThenInclude(t => t.Treatments)
                .AsNoTracking()
            .ToListAsync();


            var mappedMedcard = _mapper.Map<List<OwnerDto>>(medcard);

            return mappedMedcard;
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


            var mappedMedcard = _mapper.Map<OwnerDto>(medcard);

            return mappedMedcard;


        }


    }
}
