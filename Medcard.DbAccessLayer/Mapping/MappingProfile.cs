using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OwnerEntity, OwnerDto>();
            CreateMap<PetEntity, PetDto>();
            CreateMap<DrugEntity, DrugsDto>();
            CreateMap<TreatmentEntity, TreatmentsDto>();
            
        }
    }

}
