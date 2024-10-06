

using System;
using System.Collections.Generic;

namespace Medcard.DbAccessLayer.Entities
{
    public class OwnerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        private DateTime _dateCreate;
        public DateTime DateCreate
        {
            get => _dateCreate;
            set => _dateCreate = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }

        // Связь один ко многим (один хозяин может иметь несколько питомцев)
        public List<PetEntity> Pets { get; set; } = new List<PetEntity>();



    }
}
