using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public class Clothing:ICloth
    {
        private string name;

        public int Id { get; set; }        

        public string Name { get => name.Trim(); set => name = value; }
        public int Number { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public string Name_
        {
            get
            {              
                return Name.Replace(" ", "-").ToLower();                
            }
        }
    }
}
