using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Medcard.DbAccessLayer.Dto
{
    public class OwnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        //Связь 1:многие
        public List<PetDto> PetsDtos { get; set; } = new List<PetDto>();

        public OwnerDto() { }
        public OwnerDto(Guid id,
                         string name, string phone,
                         string petName, string chipNumber,
                         string petAge, string petBreed,
                         string petDrugs,
                         string petTreatment)
        {
            Id = id;
            Name = name;
            PhoneNumber = phone;
            PetsDtos = new List<PetDto>()
            {
                new PetDto()
                {
                    Id= id,
                    Name= name,
                    ChipNumber= chipNumber,
                    Age=petAge,
                    Breed=petBreed,
                    DrugDtos=new List<DrugsDto>()
                    {
                        new DrugsDto()
                        {
                            PetId= id,
                            Description= name,
                        }


                    },
                    TreatmentDtos = new List<TreatmentsDto>()
                    {
                        new TreatmentsDto()
                        {
                            PetId = id,
                            Description= name
                        }
                    }
                }
            };

            

        }
        public static OwnerDto Create(Guid id,
                         string name, string phone,
                         string petName, string chipNumber,
                         string petAge, string petBreed,
                         string petDrugs,
                         string petTreatment)
        {
            return new OwnerDto(id, name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);
        }
       
    }
}
