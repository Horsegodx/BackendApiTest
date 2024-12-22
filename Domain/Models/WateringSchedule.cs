using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class WateringSchedule
    {
        public int WateringScheduleId { get; set; }
        public int PlantId { get; set; }
        public DateTime WateringDate { get; set; }
        public TimeSpan WateringTime { get; set; }

        public virtual Plant Plant { get; set; } = null!;
    }
}
