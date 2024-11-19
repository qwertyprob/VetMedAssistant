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
            
            var lowerSearchItem = searchItem.ToLower();


            var medcardResult = await _dbcontext.Owners
                .Include(o => o.Pets)
                    .ThenInclude(p => p.Drugs)
                .Include(o => o.Pets)
                    .ThenInclude(p => p.Treatments)
                .Include(o => o.Pets)
                    .ThenInclude(p => p.Recomendations)
                .AsNoTracking()
                .Where(o => o.Name.ToLower().StartsWith(lowerSearchItem) ||
                            o.Pets.Any(pet => pet.Name.ToLower().StartsWith(lowerSearchItem)))
                .OrderByDescending(o => o.DateCreate)
                .ToListAsync();

            var mappedMedcard = _mapper.Map<IReadOnlyCollection<OwnerDto>>(medcardResult);

            return mappedMedcard;
        }


    }
}
