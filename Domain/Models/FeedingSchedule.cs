using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class FeedingSchedule
    {
        public int FeedingId { get; set; }
        public int AnimalsId { get; set; }
        public TimeSpan FeedingTime { get; set; }
        public string FeedingType { get; set; } = null!;
        public DateTime FeedingDate { get; set; }

        public virtual Animal Animals { get; set; } = null!;
    }
}
