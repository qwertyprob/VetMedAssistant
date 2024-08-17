using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Dto
{
    [Serializable]
    public class PetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ChipNumber { get; set; }
        public string Age { get; set; }
        public string Breed { get; set; }

        //Связи 1:многие
        public List<TreatmentsDto> TreatmentDtos { get; set; } = new List<TreatmentsDto>();
        public List<DrugsDto> DrugDtos { get; set; } = new List<DrugsDto>();
    }
}
