using Medcard.DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Entities
{
    public class PetEntity
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ChipNumber { get; set; }
        public string Age { get; set; }
        public string Breed { get; set; }

        // Внешний ключ для связи с владельцем
        public Guid OwnerId { get; set; }
        public OwnerEntity Owner { get; set; }

        // Связь один ко многим (один питомец может иметь несколько записей о лечении и препаратах)
        public List<TreatmentEntity> Treatments { get; set; } = new List<TreatmentEntity>();
        public List<DrugEntity> Drugs { get; set; } = new List<DrugEntity>();

        public List<RecomendationEntity> Recomendations { get; set; } = new List<RecomendationEntity>();
    }
}
