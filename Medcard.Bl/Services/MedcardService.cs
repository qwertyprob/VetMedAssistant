using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.Bl.Abstraction;
using Medcard.Bl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Medcard.Bl.Services
{
    public class MedcardService : IMedcardService
    {

        private readonly IMedcardRepository _repository;
        private readonly ISearchRepository _searchRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public MedcardService(IMedcardRepository repository, IMapper mapper, AppDbContext dbcontext, ISearchRepository searchRepository)
        {

            _repository = repository;
            _mapper = mapper;
            _dbContext = dbcontext;
            _searchRepository = searchRepository;
        }

        public async Task<IReadOnlyCollection<OwnerModel>> GetAllAsync()
        {
            var medcard = await _repository.GetAllAsync();

            if (medcard is null)
            {
                return Array.Empty<OwnerModel>();
            }
            var mappedMedcard = _mapper.Map<IReadOnlyCollection<OwnerModel>>(medcard);

            return mappedMedcard;
        }
        public async Task<OwnerModel> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return null;

            var medcard = await _repository.GetByIdAsync(id);

            var mappedMedcard = _mapper.Map<OwnerModel>(medcard);

            return mappedMedcard;

        }
        public async Task<OwnerModel> CreateAsync(MedcardViewModel medcardViewModel)
        {
            medcardViewModel.DateCreate = DateTime.Now; 
            var medcard = await _repository.CreateAsync(medcardViewModel);
            var mappedMedcard = _mapper.Map<OwnerModel>(medcard);

            return mappedMedcard;
        }
        public async Task<OwnerModel> UpdateAsync(Guid id, MedcardViewModel medcardViewModel)
        {
            var medcard = await _repository.UpdateNoDrugsNoTreatmentsAsync(id, medcardViewModel);

            var mappedMedcard = _mapper.Map<OwnerModel>(medcard);

            return mappedMedcard;
        }

        //Переписать логику в Irepository -> IMedcardMvcService  -> Controlller
        public async Task UpdateDrugsAsync(Guid petId, string drugs)
        {
            var pet = await _dbContext.Pets
                .Include(p => p.Drugs)
                .FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
            {
                throw new Exception("Pet not found");
            }
            if (drugs == " ")
            {
                drugs = "\nПрепараты:\n-\n-\n-\n-\n-";
            }

            pet.Drugs.Clear();

            var drug = new DrugEntity { Description = drugs.Trim() };
            pet.Drugs.Add(drug);

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateTreatmentsAsync(Guid petId, string treatments)
        {
            var pet = await _dbContext.Pets
                .Include(p => p.Treatments)
                .FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
            {
                throw new Exception("Pet not found");
            }

            if (treatments == " ")
            {
                treatments = "\nЛечение:\n-\n-\n-\n-\n-";
            }

            pet.Treatments.Clear();

            var treatment = new TreatmentEntity { Description = treatments.Trim() };

            pet.Treatments.Add(treatment);

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateRecomendAsync(Guid petId, string recomendations)
        {
            var pet = await _dbContext.Pets
                .Include(p => p.Recomendations)
                .FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
            {
                throw new Exception("Pet not found");
            }
            if (recomendations == " ")
            {
                recomendations = "\nРекомендации:\n-\n-\n-\n-\n-\n-";
            }
            pet.Recomendations.Clear();

            var recomendation = new RecomendationEntity { Description = recomendations.Trim() };

            pet.Recomendations.Add(recomendation);

            await _dbContext.SaveChangesAsync();
        }


        


       

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<Guid> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Guid.Empty;

            var lowerCaseName = name.ToLower();

            var ownerId = await _dbContext.Owners
                .AsNoTracking()
                .Where(o => o.Name.ToLower() == lowerCaseName)
                .Select(o => o.Id)
                .FirstOrDefaultAsync();

            if (ownerId != Guid.Empty)
                return ownerId;

            var petOwnerId = await _dbContext.Owners
                .Include(o => o.Pets)
                    .ThenInclude(p => p.Drugs)
                .Include(o => o.Pets)
                    .ThenInclude(p => p.Treatments)
                .AsNoTracking()
                .Where(o => o.Pets.Any(p => p.Name.ToLower() == lowerCaseName))
                .Select(o => o.Id)
                .FirstOrDefaultAsync();

            return petOwnerId;
        }









    }
}
