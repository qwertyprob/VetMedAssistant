using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Medcard.Mvc.Services
{
    public class MedcardServiceMvc : IMedcardServiceMvc
    {

        private readonly IMedcardRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public MedcardServiceMvc(IMedcardRepository repository, IMapper mapper, AppDbContext dbcontext)
        {

            _repository = repository;
            _mapper = mapper;
            _dbContext = dbcontext;
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
            var medcard = await _repository.CreateAsync(medcardViewModel);
            var mappedMedcard = _mapper.Map<OwnerModel>(medcard);

            return mappedMedcard;
        }
        public async Task<OwnerModel> UpdateAsync(Guid id, MedcardViewModel medcardViewModel)
        {
            var medcard = await _repository.UpdateAsync(id, medcardViewModel);

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

            pet.Drugs.Clear();

            var drugDescriptions = drugs.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var description in drugDescriptions)
            {
                var drug = new DrugEntity { Description = description.Trim() };
                pet.Drugs.Add(drug);
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateTreatmentsAsync(Guid petId, string treatments)
        {
            // Найти питомца по ID
            var pet = await _dbContext.Pets
                .Include(p => p.Treatments)
                .FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
            {
                throw new Exception("Pet not found");
            }

            // Очистить существующее лечение
            pet.Treatments.Clear();

            // Добавить новое лечение
            var treatmentDescriptions = treatments.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var description in treatmentDescriptions)
            {
                var treatment = new TreatmentEntity { Description = description.Trim() };
                pet.Treatments.Add(treatment);
            }

            // Сохранить изменения в базе данных
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {


            return await _repository.DeleteAsync(id);

        }

        public async Task<Guid> SearchByPetName(string petName)
        {
            if (string.IsNullOrWhiteSpace(petName))
                return Guid.Empty;

            var lowerCasePetName = petName.ToLower();

            var medcard = await _dbContext.Owners
               .Include(p => p.Pets)
                   .ThenInclude(d => d.Drugs)
               .Include(p => p.Pets)
                   .ThenInclude(t => t.Treatments)
               .AsNoTracking()
               .Where(o => o.Pets.Any(p => p.Name.ToLower() == lowerCasePetName))
               .Select(o => o.Id)
               .FirstOrDefaultAsync();

            return medcard;
        }








    }
}
