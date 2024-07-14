

using System;
using System.Collections.Generic;

namespace Medcard.DbAccessLayer.Entities
{
    public class OwnerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        // Связь один ко многим (один хозяин может иметь несколько питомцев)
        public List<PetEntity> Pets { get; set; } = new List<PetEntity>();



    }
}
