using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class WishList : ICloth
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ClothId { get; set; }

        public string Name { get;set;}        
        public string Color {get;set; }
        public string Size { get; set; }

        public static bool operator== (WishList wishItem, string nullValue)
        {
            if (wishItem.Name == nullValue || wishItem.Color == nullValue || wishItem.ClothId == null)
                return true;
            else
                return !true;
        }

        public static bool operator!= (WishList wishItem, string nullValue)
        {        
                return !(wishItem==nullValue);
        }
    }

}