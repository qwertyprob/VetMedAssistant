using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Bl.Models
{
    [Serializable]
    public class TestsModel
    {
        public Guid PetId { get; set; }
        public string Description { get; set; } = "-Общий анализ крови:\n-Биохимия крови:\n-Анализ мочи:\n-УЗИ:\n-\n-\n-\n-";
    }
}
