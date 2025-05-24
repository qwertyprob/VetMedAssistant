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
                //Dto
                CreateMap<OwnerEntity, OwnerDto>()
                    .ForMember(dest => dest.PetsDtos, opt => opt.MapFrom(src => src.Pets));

                CreateMap<PetEntity, PetDto>()
                    .ForMember(dest => dest.DrugDtos, opt => opt.MapFrom(src => src.Drugs))
                    .ForMember(dest => dest.TreatmentDtos, opt => opt.MapFrom(src => src.Treatments))
                    .ForMember(dest => dest.RecomendationsDtos, opt => opt.MapFrom(src => src.Recomendations))
                    .ForMember(dest => dest.TestsDtos, opt => opt.MapFrom(src => src.Tests)); 


                    CreateMap<DrugEntity, DrugsDto>();
                    CreateMap<TreatmentEntity, TreatmentsDto>();
                    CreateMap<RecomendationEntity, RecomendationsDto>();
                    CreateMap<TestsEntitity, TestsDto>();


        }
        

    }

}
