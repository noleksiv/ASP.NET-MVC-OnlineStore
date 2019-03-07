
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using OnlineStore.WebUI.Models.Slider;
using OnlineStore.WebUI.Models.Banner;
using OnlineStore.WebUI.Models;
using OnlineStore.Domain.Models;
using Newtonsoft.Json;
using OnlineStore.WebUI.Database;
using System.Web.Security;

namespace OnlineStore.WebUI.Controllers
{
   
    public class HomeController : Controller
    {      

        ApplicationContext context = new ApplicationContext();
        MainPageParameters param = new MainPageParameters();

        public HomeController() {}

        // GET: Home
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<SliderData> slider = new List<SliderData>
        {
            new SliderData{CollectionName="Women Collection 2018", Description="NEW SEASON", ImageUrl="images/slide-01.jpg"},
            new SliderData{CollectionName="Men New-Season", Description="Jackets & Coats", ImageUrl="images/slide-02.jpg"},
            new SliderData{CollectionName="Men Collection 2018", Description="New arrivals", ImageUrl="images/slide-03.jpg"}

        };

            List<BannerItem> banner = new List<BannerItem>
        {
            new BannerItem{SectionName="Women", Description="Spring 2018", Category="women", ImageUrl="images/banner-01.jpg"},
            new BannerItem{SectionName="Men", Description="Spring 2018", Category="men", ImageUrl="images/banner-02.jpg"},
            new BannerItem{SectionName="Accessories", Description="New Trend", Category="watches", ImageUrl="images/banner-03.jpg"}

        };

            param.Clothes = context.ShopItems.Include(s=>s.Sizes).Include(c=>c.Colors).Include(i=>i.Images).Take(8).ToList();
            param.BannerParam = banner;
            param.SliderParam = slider;

            // reset shop menu default select
            ShopItem.DefaultSelect = "All Products";

            return View(param);
        }

        #region Autentification

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(UserAccount user)
        {
            var userPassword = Converter.Hash(user.Password);
            var obj = context.Users.Where(u => u.Email == user.Email && u.Password == userPassword).FirstOrDefault();
            if (obj is UserAccount)
            {
                System.Diagnostics.Debug.WriteLine("session was created!!!!!!");
                Session["UserID"] = obj.Id.ToString();
                Session["UserName"] = obj.FullName.ToString();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        #endregion


        #region Operations with a cart        

        [HttpPost]
        public JsonResult ReceiveCartItems(string json)
        {
            var items = JsonConvert.DeserializeObject<List<Clothing>>(json.ToString());

            // pass selected items to finally cart page
            return Json(new { result = "redirect", url = Url.Action("ShoppingCart", "Home", new { param = Converter.ListToUrl(items) }) });
        }

        [HttpGet]
        public JsonResult GetSelectedClothes()
        {
            if (Session["UserName"] != null)
            {
                var sessionId = Session["UserID"].ToString();

                var selectedClothes = context.Selected.Where(c => c.UserId.ToString() == sessionId).ToList();

                if (selectedClothes != null)
                {
                    var existedClothes = selectedClothes
                                        .Where(i => context.ShopItems.Any(cloth => cloth.Name == i.Name))
                                        .Select(i =>
                                        {
                                            // adjusting the newest info
                                            var obj = context.ShopItems.First(c => c.Name == i.Name);
                                            obj.Color = i.Color;
                                            obj.Size = i.Size;
                                            obj.Number = i.Number;
                                            return obj;
                                        })
                                        .ToList();

                    return Json(existedClothes, JsonRequestBehavior.AllowGet);
                }

            }
            return null;
        }

        [HttpPost]
        public JsonResult AddToCart(string json)
        {
            var selectedCloth = JsonConvert.DeserializeObject<SelectedClothes>(json);

            // check if needed atributes were passed
            if(selectedCloth != null)
            {
                selectedCloth.UserId = Int32.Parse(Session["UserID"].ToString());
                selectedCloth.Name = selectedCloth.Name.Trim();

                var sessionId = Session["UserID"].ToString();

                var selectedClothes = context.Selected.Where(c => c.UserId.ToString() == sessionId).ToList();

                if(!selectedClothes.Any(c=>c.ClothId==selectedCloth.ClothId && c.Name==selectedCloth.Name && c.Size==selectedCloth.Size && c.Color == selectedCloth.Color))
                {
                    context.Selected.Add(selectedCloth);
                    context.SaveChanges();

                    return Json(new { success = true, obj = selectedCloth }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false , msg = "Such item already exist in your cart!" }, JsonRequestBehavior.AllowGet);
                }                
                
            }

            return Json(new { success = false } , JsonRequestBehavior.AllowGet);
        }
    

        [HttpPost]
        public void DeleteItemFromCart(string json)
        {
            var temp = JsonConvert.DeserializeObject<Clothing>(json);
            var removeObj = context.Selected.Where(e => e.Name == temp.Name && e.Size == temp.Size && e.Color == temp.Color).FirstOrDefault();
            context.Selected.Remove(removeObj);
            context.SaveChanges();
            // successfully
        }

        [HttpGet]
        public JsonResult GetWishList()
        {
            if (Session["UserName"] != null)
            {
                var sessionId = Session["UserID"].ToString();

                var wishedClothes = context.WhishList.Where(c => c.UserId.ToString() == sessionId).ToList();

                if (wishedClothes != null)
                {
                    var existedClothes = wishedClothes
                                        .Where(i => context.ShopItems.Any(cloth => cloth.Id == i.ClothId))
                                        .Select(i =>
                                        {
                                            var obj = context.ShopItems.First(c => c.Id == i.ClothId);
                                            obj.Color = i.Color;
                                            obj.Size = i.Size;
                                            return obj;
                                        })
                                        .ToList();

                    return Json(existedClothes, JsonRequestBehavior.AllowGet);
                }

            }
            return null;
        }

        [HttpGet]
        public JsonResult GetWishedIds()
        {
            if (Session["UserName"] != null)
            {
                var sessionId = Session["UserID"].ToString();

                var wishedClothes = context.WhishList.Where(c => c.UserId.ToString() == sessionId).ToList();

                if (wishedClothes != null)
                {
                    var existedClothes = wishedClothes
                                        .Where(i => context.ShopItems.Any(cloth => cloth.Id == i.ClothId))
                                        .Select(i => i)
                                        .ToList();

                    return Json(new { data = existedClothes }, JsonRequestBehavior.AllowGet);
                }

            }
            return null;
        }

        [HttpPost]
        public ActionResult AddToWishList(WishList wishItem)
        {
            if (wishItem != null)
            {
                var sessionId = Session["UserID"].ToString();

                var wishedClothes = context.WhishList.Where(c => c.UserId.ToString() == sessionId).ToList();

                if (!wishedClothes.Any(w => w.ClothId == wishItem.ClothId && w.Size == wishItem.Size && w.Color == wishItem.Color))
                {
                    context.WhishList.Add(new WishList
                    {
                        UserId = Int32.Parse(sessionId),
                        ClothId = wishItem.ClothId,
                        Name = wishItem.Name,
                        Color = wishItem.Color,
                        Size = wishItem.Size
                    });

                    context.SaveChanges();

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult RemoveFromWishList(WishList wishItem)
        {
            if (wishItem != null)
            {
                var sessionId = Session["UserID"].ToString();

                var wishedClothes = context.WhishList.Where(c => c.UserId.ToString() == sessionId).ToList();

                var objForRemoving = wishedClothes.FirstOrDefault(w => w.ClothId == wishItem.ClothId && w.Size == wishItem.Size && w.Color == wishItem.Color);

                if (objForRemoving != null)
                {
                    context.WhishList.Remove(objForRemoving);

                    context.SaveChanges();

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion


    }
}