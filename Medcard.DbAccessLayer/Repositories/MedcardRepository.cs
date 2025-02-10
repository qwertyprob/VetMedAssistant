using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


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
                .Include(p=> p.Pets)
                    .ThenInclude(r => r.Recomendations)
                .OrderByDescending(p => p.DateCreate)
                .AsNoTracking()
                .ToListAsync();


            var mappedMedcard = _mapper.Map<IReadOnlyCollection<OwnerDto>>(medcard);

            return mappedMedcard;
        }
        public async Task<OwnerDto> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return null;

            var medcard = await _dbcontext.Owners
                .Include(p => p.Pets)
                    .ThenInclude(d => d.Drugs)
                .Include(p => p.Pets)
                    .ThenInclude(t => t.Treatments)
                .Include(p => p.Pets)
                    .ThenInclude(r=>r.Recomendations)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);


            var mappedMedcard = _mapper.Map<OwnerDto>(medcard);

            return mappedMedcard;

        }
        public async Task<OwnerDto> CreateAsync(MedcardViewModel medcardViewModel)
        {

            var ownerEntity = new OwnerEntity
            {
                Id = Guid.NewGuid(),
                Name = medcardViewModel.OwnerName,
                PhoneNumber = medcardViewModel.PhoneNumber,
                DateCreate = medcardViewModel.DateCreate,
                Pets = new List<PetEntity>
                {
                    new PetEntity()
                    {
                    Id = Guid.NewGuid(),
                    Name = medcardViewModel.PetName,
                    ChipNumber = medcardViewModel.ChipNumber,
                    Age = medcardViewModel.Age,
                    Breed = medcardViewModel.Breed,
                    Drugs = new List<DrugEntity>()
                    {
                        new DrugEntity()
                        {
                            
                            Description= medcardViewModel.Drugs
                        }
                    },
                    Treatments = new List<TreatmentEntity>()
                    {
                        new TreatmentEntity()
                        {
                            Description = medcardViewModel.Treatments
                        }
                    },
                    Recomendations = new List<RecomendationEntity>()
                    {
                        new RecomendationEntity()
                        {
                            Description = medcardViewModel.Recomendations
                        }
                    }
                    
                    }
                }

            };    
            _dbcontext.Owners.Add(ownerEntity);
            await _dbcontext.SaveChangesAsync();
            var medcard = _mapper.Map<OwnerDto>(ownerEntity);

            return medcard;
        }

        public async Task <OwnerDto> UpdateAsync(Guid id, MedcardViewModel medcardViewModel)
        {
            var ownerEntity = await _dbcontext.Owners
            .Include(o => o.Pets)
            .FirstOrDefaultAsync(o => o.Id == id);

            ownerEntity.Name = medcardViewModel.OwnerName;
            ownerEntity.PhoneNumber = medcardViewModel.PhoneNumber;
            ownerEntity.DateCreate = medcardViewModel.DateCreate;
            foreach (var pet in ownerEntity.Pets)
            {
                pet.Name = medcardViewModel.PetName;
                pet.ChipNumber = medcardViewModel.ChipNumber;
                pet.Age = medcardViewModel.Age;
                pet.Breed = medcardViewModel.Breed;
            }

             _dbcontext.Owners.Update(ownerEntity);
            await _dbcontext.SaveChangesAsync();
            
            var mappedMedcard = _mapper.Map<OwnerDto>(ownerEntity);

            return mappedMedcard;
        }
        public async Task<string> UpdateDrugsAsync(Guid id, string Drugs)
        {
            var ownerEntity = await _dbcontext.Pets
            .Include(o => o.Drugs)
            .FirstOrDefaultAsync(o => o.Id == id);

            foreach(var drug in ownerEntity.Drugs)
            {

                drug.Description = Drugs;
                
            }
            await _dbcontext.SaveChangesAsync();
           

            return Drugs; 

        }
        public async Task<string> UpdateTreatAsync(Guid id, string Treatments)
        {
            var ownerEntity = await _dbcontext.Pets
            .Include(o => o.Treatments)
            .FirstOrDefaultAsync(o => o.Id == id);

            foreach (var treatment in ownerEntity.Treatments)
            {

                treatment.Description = Treatments;

            }
            await _dbcontext.SaveChangesAsync();
            return Treatments;
        }
        public async Task<string> UpdateRecAsync(Guid id, string Recomendations)
        {
            var ownerEntity = await _dbcontext.Pets
            .Include(o => o.Recomendations)
            .FirstOrDefaultAsync(o => o.Id == id);

            foreach (var recomendation in ownerEntity.Recomendations)
            {

                recomendation.Description = Recomendations;

            }
            await _dbcontext.SaveChangesAsync();
            return Recomendations;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {

            var ownerEntity = await _dbcontext.Owners
            .Include(o => o.Pets)
                .ThenInclude(p => p.Drugs)
            .Include(o => o.Pets)
                .ThenInclude(p => p.Treatments)
            .Include(p => p.Pets)
                    .ThenInclude(r => r.Recomendations)
            .FirstOrDefaultAsync(o => o.Id == id);

            if (ownerEntity == null)
            {
                return false; 
            }

            _dbcontext.Remove(ownerEntity);

            await _dbcontext.SaveChangesAsync();

            
            return true;

        }
    }
}
