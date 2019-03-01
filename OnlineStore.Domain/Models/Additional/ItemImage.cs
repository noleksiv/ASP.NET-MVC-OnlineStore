using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class ItemImage
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public int? ShopItemId { get; set; }
        public ShopItem ShopItem { get; set; }
    }
}
