using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class SelectedClothes:ICloth
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ClothId { get; set; }

        public string Name  { get; set; }
        public int Number   { get; set; }
        public string Color { get; set; }
        public string Size  { get; set; }

        public SelectedClothes()    {}

        public static bool operator ==(SelectedClothes selectedItem, string nullValue)
        {
            if (selectedItem.Name == nullValue || selectedItem.Color == nullValue || selectedItem.ClothId == null || selectedItem.Number == 0)
                return true;
            else
                return !true;
        }

        public static bool operator !=(SelectedClothes selectedItem, string nullValue)
        {
            return !(selectedItem == nullValue);
        }
    }
}
