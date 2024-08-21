using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Services
{
    public class MedcardService : IMedcardService
    {

        private readonly IMedcardRepository _repository;
        public MedcardService(IMedcardRepository repository)
        {

            _repository = repository;
        }
        public async Task<IReadOnlyCollection<OwnerDto>> GetAllAsync()
        {
            var medcard = await _repository.GetAllAsync();

            if(medcard is null)
            {
                return Array.Empty<OwnerDto>();
            }

            return medcard;
        }
        public async Task <OwnerDto> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return null;

            return await _repository.GetByIdAsync(id);
        }
        public async Task<OwnerDto> CreateAsync(MedcardViewModel medcardViewModel)
        {
            var medcard = await _repository.CreateAsync(medcardViewModel);

            return medcard;
        }
        public async Task<OwnerDto> UpdateAsync(Guid id,MedcardViewModel medcardViewModel)
        {
            var medcard = await _repository.UpdateAsync(id,medcardViewModel);

            return medcard;
        }
        public async Task<OwnerDto> UpdateDrugsAndTreatments(Guid id, string Drugs, string Treatments)
        {
            var medcard = await _repository.UpdateDrugsAndTreatments(id, Drugs, Treatments);

            return medcard;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {


           return await _repository.DeleteAsync(id);

        }
    }
}
