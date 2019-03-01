using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class ClothesHeap:ICloth
    {
        public int Id { get; set; }

        public int? WhishId { get; set; }
        public int? SelectedId { get; set; }

        public WishList Wish { get; set; }
        public SelectedClothes Selected { get; set; }

        public string Name { get;set; }
        public int Number { get;set;}
        public string Color {get;set; }
        public string Size {get;set;}
    }                      
}
