using System.Collections.Generic;
using System;

namespace MedcardMvc.Models
{
    public class PetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        private int? _chipNumber;

        public int? ChipNumber
        {
            get => _chipNumber != 0 ? _chipNumber : null;
            set => _chipNumber = value;
        }
        private int? _age;
        public int? Age
        {
            get => _age != 0 ? _age : null;
            set => _age = value > 0 ? value : null ;
        }
        public string Breed { get; set; }
        public List<DrugModel> Drugs { get; set; } = new List<DrugModel>();
        public List<TreatmentModel> Treatments { get; set; } = new List<TreatmentModel>();
    }

}
