using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Client.Models
{
    [Serializable]
    public class TreatmentsModel
    {
        private Guid PetId { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = "\"Лечение:\\n-\\n-\\n-\\n-\\n-\"";
    }
}
