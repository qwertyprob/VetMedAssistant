using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Entities
{
    public class RecomendationEntity
    {
        public Guid Id { get; set; }

        public string Descriptions { get; set; }

        // Внешний ключ для связи с питомцем
        public Guid PetId { get; set; }
        public PetEntity Pet { get; set; }
    }
}
