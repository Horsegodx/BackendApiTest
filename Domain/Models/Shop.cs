using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Shop
    {
        public Shop()
        {
            ProductInShops = new HashSet<ProductInShop>();
            UserShops = new HashSet<UserShop>();
        }

        public int ShopId { get; set; }
        public string ShopName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;

        public virtual ICollection<ProductInShop> ProductInShops { get; set; }
        public virtual ICollection<UserShop> UserShops { get; set; }
    }
}
