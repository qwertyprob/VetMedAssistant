using System.Collections.Generic;
using System;

namespace Medcard.Mvc.Models
{
    public class PetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public int? ChipNumber
        //{
        //    get => age == 0 ? null : age;
        //    set => age = value;
        //}
        //private int? age;
        //public int? Age
        //{
        //    get => age == 0 ? null : age;
        //    set => age = value;
        //}
        public int? ChipNumber { get; set; }
        public int? Age { get; set; }
        public string Breed { get; set; }
        public List<DrugModel> Drugs { get; set; } = new List<DrugModel>();
        public List<TreatmentModel> Treatments { get; set; } = new List<TreatmentModel>();
    }

}
