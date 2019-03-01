using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class ShopItem: Clothing
    {
        static public string DefaultSelect { get; set; }

        public string Description { get; set; }
        public string TitleImg { get; set; }
        public string Gender { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }


        public ICollection<Size> Sizes { get; set; }
        public ICollection<Color> Colors { get; set; }
        public ICollection<ItemImage> Images { get; set; }

        public ShopItem()
        {
            Sizes =  new List<Size>();
            Colors = new List<Color>();
            Images = new List<ItemImage>();
        }

        public static ShopItem operator +(ShopItem item1, Clothing item2)
        {
            ShopItem shopCart = (ShopItem)item1.MemberwiseClone();
            shopCart.Number = item2.Number;
            shopCart.Color = item2.Color;
            shopCart.Size = item2.Size;
            return shopCart;
        }

        public static ShopItem operator +(ShopItem item1, SelectedClothes item2)
        {
            ShopItem shopCart = (ShopItem)item1.MemberwiseClone();
            shopCart.Number = item2.Number;
            shopCart.Color = item2.Color;
            shopCart.Size = item2.Size;
            return shopCart;
        }

        public static ShopItem operator -(ShopItem item1, SelectedClothes item2)
        {
            //ShopItem shopCart = (ShopItem)item1.MemberwiseClone();
            item1.Quantity -= item2.Number;
            return item1;
        }
    }        
}
