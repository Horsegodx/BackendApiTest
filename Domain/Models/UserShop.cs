using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserShop
    {
        public int UserShopId { get; set; }
        public int UserId { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
