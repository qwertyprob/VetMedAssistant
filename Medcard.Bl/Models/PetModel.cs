﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Bl.Models
{
    [Serializable]
    public class PetModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Кличка питомца обязательна для заполнения.")]
        public string Name { get; set; }
        public string ChipNumber { get; set; }
        public string Age { get; set; }
        public string Breed { get; set; }

        //Связи 1:многие
        public List<TreatmentsModel> Treatments { get; set; } = new List<TreatmentsModel>();
        public List<RecomendationsModel> Recomendations { get; set; } = new List<RecomendationsModel>();
        public List<TestsModel> Tests { get; set; } = new List<TestsModel>();
        public List<DrugsModel> Drugs { get; set; } = new List<DrugsModel>();
    }
}
