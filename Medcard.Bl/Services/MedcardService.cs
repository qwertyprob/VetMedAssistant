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


        public MedcardService(IMedcardRepository repository, IMapper mapper, ISearchRepository searchRepository)
        {

            _repository = repository;
            _mapper = mapper;
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
        

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<OwnerModel> UpdateAsync(Guid id, MedcardViewModel medcardViewModel)
        {
            var medcardDto = await _repository.UpdateAsync(id, medcardViewModel);

            var mappedMedcard = _mapper.Map<OwnerModel>(medcardDto);

            return mappedMedcard;

        }

        public async Task<string> UpdateDrugsAsync(Guid id, string drugs)
        {

            await _repository.UpdateDrugsAsync(id, drugs);

            return drugs;
        }

        public async Task<string> UpdateTreatAsync(Guid id, string treatments)
        {

            await _repository.UpdateTreatAsync(id, treatments);

            return treatments;
        }

        public async Task<string> UpdateRecAsync(Guid id, string recomendations)
        {

            await _repository.UpdateRecAsync(id, recomendations);

            return recomendations;
        }




    }
}
