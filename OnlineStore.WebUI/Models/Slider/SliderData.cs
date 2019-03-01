using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models.Slider
{
    public class SliderData
    {
        private string rootStyleFolder = "Visualization/";
        private string imageUrl;

        public string CollectionName { get; set; }
        public string Description { get; set; }
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                if (value.StartsWith("images"))
                {
                    imageUrl = rootStyleFolder + value;
                }
                else
                {
                    imageUrl = value;
                }

            }
        }

    }
}