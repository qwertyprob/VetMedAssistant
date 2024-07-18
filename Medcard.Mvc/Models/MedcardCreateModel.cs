namespace Medcard.Mvc.Models
{
    public class MedcardCreateModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PetName { get; set; }
        public int ChipNumber { get; set; }
        public int PetAge { get; set; }
        public string PetBreed { get; set; }
        public string PetDrugs { get; set; } = "Здесь пока пусто!";
        public string PetTreatment { get; set; } = "Здесь пока пусто!";
    }

}
