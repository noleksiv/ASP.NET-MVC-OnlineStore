using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }        

        public ICollection<ShopItem> Clothes { get; set; }

        public Size()
        {
            Clothes = new List<ShopItem>();
        }
    }
}
