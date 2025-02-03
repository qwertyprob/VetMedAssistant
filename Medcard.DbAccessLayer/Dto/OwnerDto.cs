using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Medcard.DbAccessLayer.Dto
{
    [Serializable]
    public class OwnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreate { get; set; }

        //Связь 1:многие
        public List<PetDto> PetsDtos { get; set; } = new List<PetDto>();

        
    }
}
