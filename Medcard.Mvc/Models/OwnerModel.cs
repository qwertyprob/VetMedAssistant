using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Имя клиента обязательно для заполнения.")]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime DateCreate { get; set; }
        //Связь 1:многие
        public List<PetModel> Pets { get; set; } = new List<PetModel>();


        public OwnerModel() { }
        public OwnerModel(Guid id,
                         string name, string phone, DateTime date,
                         string petName, string chipNumber,
                         string petAge, string petBreed,
                         string petDrugs,
                         string petTreatment)
        {
            Id = id;
            Name = name;
            PhoneNumber = phone;
            DateCreate = date;
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
                         string name, string phone,DateTime date,
                         string petName, string chipNumber,
                         string petAge, string petBreed,
                         string petDrugs,
                         string petTreatment)
        {
            return new OwnerModel(id, name, phone,date, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);
        }



    }
}
