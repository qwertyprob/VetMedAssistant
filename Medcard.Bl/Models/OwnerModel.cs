using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Bl.Models
{
    [Serializable]
    public class OwnerModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Имя клиента обязательно для заполнения.")]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime DateCreate { get; set; }
        //Связь 1:многие
        public List<PetModel> Pets { get; set; } = new List<PetModel>();


        


    }
}
