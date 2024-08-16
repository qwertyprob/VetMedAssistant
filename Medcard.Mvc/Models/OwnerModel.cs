using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Mvc.Models
{
    public class OwnerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        //Связь 1:многие
        public List<PetModel> Pets { get; set; } = new List<PetModel>();


        public OwnerModel(Guid id, string name, string phone, List<PetModel> pets)
        {
            Id = id;
            Name = name;
            PhoneNumber = phone;
            Pets = pets;
        }





        public static OwnerModel Create(Guid id, string name, string phone, List<PetModel> pets)
        {
            return new OwnerModel(id, name, phone, pets);
        }



    }
}
