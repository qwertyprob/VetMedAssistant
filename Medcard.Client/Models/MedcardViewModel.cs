using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Client.Models
{ 
    [Serializable]
    public class MedcardViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OwnerName { get; set; }
        public string PhoneNumber {  get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public string PetName { get; set; }
        public string ChipNumber { get; set; }
        public string Age { get; set; }
        public string Breed { get; set; }
        public string Drugs { get; set; } = "-\n-\n-\n-\n-\n-\n-\n-\n-";
        public string Treatments { get; set; } = "-\n-\n-\n-\n-\n-\n-\n-\n-";
        public string Recomendations {  get; set; } = "-\n-\n-\n-\n-\n-\n-\n-\n-";

        public MedcardViewModel() { }
        public MedcardViewModel(string OwnerName, string PhoneNumber, string PetName, string ChipNumber,
                                string Age, string Breed, string Drugs= "Здесь пока ничего не указано!",
                                string Treatments= "Здесь пока ничего не указано!",
                                string recomendations = "Здесь пока ничего не указано!")  
        {
            Id=Guid.NewGuid();
            this.OwnerName=OwnerName;
            this.PhoneNumber=PhoneNumber;
            this.PetName=PetName;
            this.ChipNumber=ChipNumber;
            this.Age=Age;
            this.Breed=Breed;
            this.Drugs=Drugs;
            this.Treatments=Treatments;
            this.Recomendations=Recomendations;
        }
    }
}
