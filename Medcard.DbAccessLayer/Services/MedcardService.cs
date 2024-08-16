using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Interfaces;
using System;
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
            return await _repository.GetAllAsync();
        }
        public async Task <OwnerDto> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

    }
}
