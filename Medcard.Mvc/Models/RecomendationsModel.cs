using System;

namespace Medcard.Mvc.Models
{
    public class RecomendationsModel
    {
        public Guid PetId { get; set; }
        public string Description { get; set; } = "Здесь пока ничего не указано!";
    }
}
