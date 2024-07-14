using Medcard.DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Entities
{
    public class TreatmentEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        // Внешний ключ для связи с питомцем
        public Guid PetId { get; set; }
        public PetEntity Pet { get; set; }
    }
}
