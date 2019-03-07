using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using OnlineStore.WebUI.Models.Slider;
using OnlineStore.WebUI.Models.Banner;
using OnlineStore.WebUI.Models;
using OnlineStore.Domain.Models;
using OnlineStore.WebUI.Database;
using System.Web;

namespace OnlineStore.WebUI.Controllers.Api
{
    
    public class HomeController : ApiController
    {
        ApplicationContext context;       
        

        public HomeController()
        {
            context = new ApplicationContext();
        }
        
        [HttpGet]
        public IEnumerable<ShopItem> GetSelectedClothes()
        {

            if (HttpContext.Current.Session["UserName"] != null)
            {
                var selectedClothes = context.Selected.Where(c => c.UserId == Int32.Parse(HttpContext.Current.Session["UserID"].ToString())).ToList();

                if (selectedClothes != null)
                {
                    var existedClothes = selectedClothes
                                        .Where(i => context.ShopItems.Any(cloth=>cloth.Name==i.Name))
                                        .Select(i =>
                                        {
                                            return  context.ShopItems.First(c => c.Name == i.Name);
                                        });
                    return existedClothes;
                }
               
            }
            return null;                     
        }       

    }
}
