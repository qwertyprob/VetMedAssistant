using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Bl.Models
{
    [Serializable]
    public class TreatmentsModel
    {
        public Guid PetId { get; set; }
        public string Description { get; set; } = "\"Лечение:\\n-\\n-\\n-\\n-\\n-\"";
    }
}
