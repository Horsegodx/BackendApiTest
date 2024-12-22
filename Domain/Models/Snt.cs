using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Snt
    {
        public Snt()
        {
            SntEvents = new HashSet<SntEvent>();
        }

        public int SntId { get; set; }
        public string SntName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string ManagerFirstName { get; set; } = null!;
        public string ManagerLastName { get; set; } = null!;
        public string ManagerPhone { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<SntEvent> SntEvents { get; set; }
    }
}
