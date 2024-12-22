using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Harvest
    {
        public int HarvestId { get; set; }
        public DateTime HarvestDate { get; set; }
        public int PlantId { get; set; }

        public virtual Plant Plant { get; set; } = null!;
    }
}
