using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using OnlineStore.WebUI.Models.Slider;
using OnlineStore.WebUI.Models.Banner;
using OnlineStore.Domain.Models;

namespace OnlineStore.WebUI.Models
{
    public class MainPageParameters
    {
        public List<SliderData> SliderParam { get; set; }
        public List<BannerItem> BannerParam { get; set; }
        public List<ShopItem> Clothes { get; set; }
        public List<Clothing> SelectedCloth { get; set; }
    }
}