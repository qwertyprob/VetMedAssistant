using System;

namespace Medcard.Mvc.Models
{
    public class MedcardUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PetName { get; set; }
        public int ChipNumber { get; set; }
        public int PetAge { get; set; }
        public string PetBreed { get; set; }
        public string PetDrugs { get; set; }
        public string PetTreatment { get; set; }
    }

}
