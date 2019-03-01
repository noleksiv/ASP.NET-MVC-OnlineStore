using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OnlineStore.WebUI.Models.Slider;
using OnlineStore.WebUI.Models.Banner;
using OnlineStore.WebUI.Models;
using OnlineStore.Domain.Models;


namespace OnlineStore.WebUI.Controllers.Blog
{
    public class BlogController : Controller
    {
       
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
    }
}