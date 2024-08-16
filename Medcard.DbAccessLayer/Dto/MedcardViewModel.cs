using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Dto
{
    public class MedcardViewModel
    {
        public string OwnerName { get; set; }
        public string PhoneNumber {  get; set; }
        public string PetName { get; set; }
        public string ChipNumber { get; set; }
        public string Age { get; set; }
        public string Breed { get; set; }
        public string Drugs { get; set; }
        public string Treatments { get; set; }

        public MedcardViewModel(Guid Id,string OwnerName, string PhoneNumber, string PetName, string ChipNumber,
                                string Age, string Breed, string Drugs, string Treatments)  
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
