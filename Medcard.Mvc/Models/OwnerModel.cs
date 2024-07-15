using System.Collections.Generic;
using System;

namespace MedcardMvc.Models
{
    public class OwnerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<PetModel> Pets { get; set; } = new List<PetModel>();
    }

}
