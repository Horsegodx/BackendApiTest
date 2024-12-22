using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserPlant
    {
        public int UserPlantId { get; set; }
        public int UserId { get; set; }
        public int PlantId { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
