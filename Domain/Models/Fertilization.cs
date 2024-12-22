using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Fertilization
    {
        public int FertilizationId { get; set; }
        public DateTime FertilizationDate { get; set; }
        public string FertilizerName { get; set; } = null!;
        public int PlantId { get; set; }

        public virtual Plant Plant { get; set; } = null!;
    }
}
