using AutoMapper;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.Bl.Models;
using System.Linq;

namespace Medcard.Bl.Mapping
{
    public class MappingProfileBlazor : Profile
    {
        public MappingProfileBlazor()
        {
            // Entity to DTO
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

            // DTO to Model
            CreateMap<OwnerDto, OwnerModel>()
                .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.PetsDtos));

            CreateMap<PetDto, PetModel>()
                .ForMember(dest => dest.Treatments, opt => opt.MapFrom(src => src.TreatmentDtos))
                .ForMember(dest => dest.Drugs, opt => opt.MapFrom(src => src.DrugDtos))
                .ForMember(dest => dest.Recomendations, opt => opt.MapFrom(src => src.RecomendationsDtos))
                .ForMember(dest => dest.Tests, opt => opt.MapFrom(src => src.TestsDtos));

            CreateMap<DrugsDto, DrugsModel>();
            CreateMap<TreatmentsDto, TreatmentsModel>();
            CreateMap<RecomendationsDto, RecomendationsModel>();
            CreateMap<TestsDto, TestsModel>();

            //Dto to ViewModel

            CreateMap<OwnerDto, MedcardViewModel>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.PetsDtos.FirstOrDefault().Name))
            .ForMember(dest => dest.ChipNumber, opt => opt.MapFrom(src => src.PetsDtos.FirstOrDefault().ChipNumber))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.PetsDtos.FirstOrDefault().Age))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.PetsDtos.FirstOrDefault().Breed))
            .ForMember(dest => dest.Drugs, opt => opt.MapFrom(src =>
                string.Join("\n", src.PetsDtos.SelectMany(p => p.DrugDtos.Select(d => d.Description)))))
            .ForMember(dest => dest.Treatments, opt => opt.MapFrom(src =>
                string.Join("\n", src.PetsDtos.SelectMany(p => p.TreatmentDtos.Select(t => t.Description)))))
            .ForMember(dest => dest.Treatments, opt => opt.MapFrom(src =>
                string.Join("\n", src.PetsDtos.SelectMany(p => p.RecomendationsDtos.Select(t => t.Description)))))
            .ForMember(dest => dest.Tests, opt => opt.MapFrom(src =>
                string.Join("\n", src.PetsDtos.SelectMany(p => p.TestsDtos.Select(t => t.Description)))));
        }


    }
}

