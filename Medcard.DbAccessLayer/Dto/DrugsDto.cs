using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Dto
{
   
    public class DrugsDto
    {
        public Guid PetId { get; set; }
        public string Description { get; set; }
    }
}
