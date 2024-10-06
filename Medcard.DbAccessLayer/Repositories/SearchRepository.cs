using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Repositories
{
    public class SearchRepository:ISearchRepository
    {
        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;

        public SearchRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public async Task<IReadOnlyCollection<OwnerDto>> GetAllFromSearchAsync(string searchItem)
        {
            var medcardResult = await _dbcontext.Owners
            .Include(p => p.Pets)
                .ThenInclude(d => d.Drugs)
            .Include(p => p.Pets)
                .ThenInclude(t => t.Treatments)
            .Include(p => p.Pets)
                    .ThenInclude(r => r.Recomendations)
            .AsNoTracking()
            .Where(p => p.Name.ToLower() == searchItem.ToLower() || p.Pets.Any(pet => pet.Name.ToLower() == searchItem.ToLower()))
            .OrderByDescending(p => p.DateCreate)
            .ToListAsync();

            var mappedMedcard = _mapper.Map<IReadOnlyCollection<OwnerDto>>(medcardResult);

            return mappedMedcard;
        }

    }
}
