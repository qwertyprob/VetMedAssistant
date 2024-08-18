using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.Mvc.Models;
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

        public MedcardServiceMvc(IMedcardRepository repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<OwnerModel>> GetAllAsync()
        {
            var medcard = await _repository.GetAllAsync();

            if(medcard is null)
            {
                return Array.Empty<OwnerModel>();
            }
            var mappedMedcard = _mapper.Map<IReadOnlyCollection<OwnerModel>> (medcard);

            return mappedMedcard;
        }

        public async Task <OwnerModel> GetByIdAsync(Guid id)
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
            var medcard = await _repository.UpdateAsync(id,medcardViewModel);

            var mappedMedcard = _mapper.Map<OwnerModel>(medcard);

            return mappedMedcard;
        }

        public async Task<OwnerModel> UpdateNew(Guid id, string drugs, string treatments)
        {
            // Step 1: Fetch the existing data
            var medcardSearch = await _repository.GetByIdAsync(id);
            if (medcardSearch == null)
            {
                throw new ArgumentException("Medcard not found.");
            }

            // Step 2: Update Drugs and Treatments
            foreach (var pet in medcardSearch.PetsDtos)
            {
                // Update Drugs
                pet.DrugDtos = string.IsNullOrWhiteSpace(drugs)
                    ? new List<DrugsDto>()
                    : drugs.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(d => new DrugsDto { Description = d.Trim() })
                           .ToList();

                // Update Treatments
                pet.TreatmentDtos = string.IsNullOrWhiteSpace(treatments)
                    ? new List<TreatmentsDto>()
                    : treatments.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(t => new TreatmentsDto { Description = t.Trim() })
                                .ToList();
            }

            var mappedMedcard = _mapper.Map<MedcardViewModel>(medcardSearch);

            await _repository.UpdateAsync(id, mappedMedcard);
            var returnModelMapper = _mapper.Map<OwnerModel>(medcardSearch);

            return returnModelMapper;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {


           return await _repository.DeleteAsync(id);

        }
    }
}
