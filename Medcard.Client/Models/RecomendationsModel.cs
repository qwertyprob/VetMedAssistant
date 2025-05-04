using System;

namespace Medcard.Client.Models
{
    public class RecomendationsModel
    {
        public Guid PetId { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = "Рекомендации:\n-\n-\n-\n-\n-";
    }
}
