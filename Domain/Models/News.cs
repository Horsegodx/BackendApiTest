using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string NewsText { get; set; } = null!;
        public DateTime NewsDate { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
