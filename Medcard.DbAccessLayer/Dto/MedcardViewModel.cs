using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Dto
{
    [Serializable]
    public class MedcardViewModel
    {
        Guid Id { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber {  get; set; }
        public string PetName { get; set; }
        public string ChipNumber { get; set; }
        public string Age { get; set; }
        public string Breed { get; set; }
        public string Drugs { get; set; }
        public string Treatments { get; set; }

        public MedcardViewModel() { }
        public MedcardViewModel(string OwnerName, string PhoneNumber, string PetName, string ChipNumber,
                                string Age, string Breed, string Drugs= "Здесь пока ничего не указано!",
                                string Treatments= "Здесь пока ничего не указано!")  
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
            
        }
    }
}
