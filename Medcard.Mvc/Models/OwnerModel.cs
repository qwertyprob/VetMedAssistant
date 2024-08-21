using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Mvc.Models
{
    [Serializable]
    public class OwnerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        //Связь 1:многие
        public List<PetModel> Pets { get; set; } = new List<PetModel>();


        public OwnerModel() { }
        public OwnerModel(Guid id,
                         string name, string phone,
                         string petName, string chipNumber,
                         string petAge, string petBreed,
                         string petDrugs,
                         string petTreatment)
        {
            Id = id;
            Name = name;
            PhoneNumber = phone;
            Pets = new List<PetModel>()
            {
                new PetModel()
                {
                    Id= id,
                    Name= petName,
                    ChipNumber= chipNumber,
                    Age=petAge,
                    Breed=petBreed,
                    Drugs=new List<DrugsModel>()
                    {
                        new DrugsModel()
                        {
                            PetId= id,
                            Description= petDrugs,
                        }


                    },
                    Treatments = new List<TreatmentsModel>()
                    {
                        new TreatmentsModel()
                        {
                            PetId = id,
                            Description= petTreatment
                        }
                    }
                }
            };



        }
        public static OwnerModel Create(Guid id,
                         string name, string phone,
                         string petName, string chipNumber,
                         string petAge, string petBreed,
                         string petDrugs,
                         string petTreatment)
        {
            return new OwnerModel(id, name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);
        }



    }
}
