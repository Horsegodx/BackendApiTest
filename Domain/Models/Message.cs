using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; } = null!;
        public TimeSpan MessageTime { get; set; }
        public DateTime MessageDate { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
