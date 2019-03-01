using OnlineStore.Domain.Models;
using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class AboutController : Controller
    {
        List<Clothing> selectedCloth = new List<Clothing>()
        {
            new Clothing
            {
                Name ="Vintage Inspired Classic",
                Color="Black",
                Size="M",
                Number=1

            },
            new Clothing
            {
                Name ="Esprit Ruffle Shirt",
                Color="White",
                Size="XL",
                Number=2
            }
        };

        MainPageParameters param;
        // GET: About
        public ActionResult Index()
        {
            ViewBag.TitleImgUrl = "/Visualization/images/bg-01.jpg";
            return View("About");
        }
    }
}