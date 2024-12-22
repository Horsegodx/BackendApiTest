using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class PollAnswer
    {
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public int OptionId { get; set; }

        public virtual PollOption Option { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
