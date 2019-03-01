using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OnlineStore.WebUI.Models.Slider;
using OnlineStore.WebUI.Models.Banner;
using OnlineStore.WebUI.Models;
using OnlineStore.Domain.Models;
using OnlineStore.WebUI.Database;

namespace OnlineStore.WebUI.Controllers
{
    public class ShopController : Controller
    {
        ApplicationContext context;

        public ShopController()
        {
            context = new ApplicationContext();   
        }
        
        // GET: Shop
        public ActionResult Index(string category = "All Products")
        {
            ShopItem.DefaultSelect = category;
            var clothList = context.ShopItems.Include(s => s.Sizes).Include(c => c.Colors).Include(i => i.Images).ToList();

            return View("Shop",clothList);
        }       
    }
}