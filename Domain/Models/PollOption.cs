using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class PollOption
    {
        public PollOption()
        {
            PollAnswers = new HashSet<PollAnswer>();
        }

        public int OptionId { get; set; }
        public int PollId { get; set; }
        public string OptionText { get; set; } = null!;

        public virtual Poll Poll { get; set; } = null!;
        public virtual ICollection<PollAnswer> PollAnswers { get; set; }
    }
}
