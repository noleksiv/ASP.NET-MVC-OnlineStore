using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models.Banner
{
    public class BannerItem
    {
        private string rootStyleFolder = "Visualization/";
        private string imageUrl;

        public string SectionName { get; set; }
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

        public string Category { get; set; }

    }
}