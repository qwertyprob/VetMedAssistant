using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Client.Models
{
    public class OwnerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "Aye";
        public string PhoneNumber { get; set; } = "Aye";

        public DateTime DateCreate { get; set; }

        public List<PetModel> Pets { get; set; } = new List<PetModel>();


       



    }
}
