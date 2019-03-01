using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hex { get; set; }
        public ICollection<ShopItem> Clothes { get; set; }

        public Color()
        {
            Clothes = new List<ShopItem>();
        }
    }
}
