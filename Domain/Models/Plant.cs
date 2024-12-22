using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Plant
    {
        public Plant()
        {
            Fertilizations = new HashSet<Fertilization>();
            Harvests = new HashSet<Harvest>();
            UserPlants = new HashSet<UserPlant>();
            WateringSchedules = new HashSet<WateringSchedule>();
        }

        public int PlantId { get; set; }
        public string PlantName { get; set; } = null!;
        public string PlantType { get; set; } = null!;

        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<Harvest> Harvests { get; set; }
        public virtual ICollection<UserPlant> UserPlants { get; set; }
        public virtual ICollection<WateringSchedule> WateringSchedules { get; set; }
    }
}
