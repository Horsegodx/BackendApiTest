using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Poll
    {
        public Poll()
        {
            PollOptions = new HashSet<PollOption>();
        }

        public int PollId { get; set; }
        public string PollQuestion { get; set; } = null!;
        public DateTime PollDate { get; set; }

        public virtual ICollection<PollOption> PollOptions { get; set; }
    }
}
