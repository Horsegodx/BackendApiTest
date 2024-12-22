using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Payment
    {
        public int PaymentsId { get; set; }
        public string PaymentType { get; set; } = null!;
        public int PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? PenaltyAmount { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
