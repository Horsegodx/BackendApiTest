using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class SntEvent
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventName { get; set; } = null!;
        public string EventLocation { get; set; } = null!;
        public int SntId { get; set; }
        public int UserId { get; set; }

        public virtual Snt Snt { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
