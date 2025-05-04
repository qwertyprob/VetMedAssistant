using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Client.Models
{
    [Serializable]
    public class DrugsModel
    {
        public Guid PetId { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = "Препараты:\n-\n-\n-\n-\n-";
    }
}
