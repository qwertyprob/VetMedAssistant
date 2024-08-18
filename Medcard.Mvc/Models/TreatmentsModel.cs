using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Mvc.Models
{
    [Serializable]
    public class TreatmentsModel
    {
        public Guid PetId { get; set; }
        public string Description { get; set; } = "Здесь пока ничего нет!";
    }
}
