using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class ProductInShop
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductPrice { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; } = null!;
    }
}
