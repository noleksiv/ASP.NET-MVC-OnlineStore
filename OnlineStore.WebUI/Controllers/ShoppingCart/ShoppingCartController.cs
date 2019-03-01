using OnlineStore.WebUI.Database;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Domain.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationContext context;
        List<Country> Countries;

        public ShoppingCartController()
        {
            context = new ApplicationContext();
            // ReadCountryData();

        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View("ShoppingCart");
        }

        [HttpGet]
        public JsonResult Checkout()
        {
            var sessionId = Int32.Parse(Session["UserID"].ToString());
            var selectedItems = context.Selected.Where(i => i.UserId == sessionId).ToList();

            var latestData = from s in selectedItems
                             from i in context.ShopItems.Include(a => a.Colors).Include(a => a.Sizes).ToList()
                             // if sizes > 0 - it's not a watch, so we can compare sizes in the table
                             where i.Id==s.ClothId && i.Name == s.Name && i.Colors.Any(c => c.Name == s.Color) /*&& ((i.Sizes.Count > 0) ? 
                                                                                                ((i.Gender== "shoes") ? i.Sizes.Any(n => SizeEstablisher(n.Name) == s.Size) : i.Sizes.Any(n => n.Name == s.Size) ) : s.Size.Length == 0)*/
                                                                                                //(i.Gender == "Shoes") ? i.Sizes.Any(n => SizeEstablisher(n.Name) == s.Size) : s.Size.Length == 0)
                             select new ShopItem
                             {
                                 Id = s.Id,
                                 Name = i.Name,
                                 Price = i.Price,
                                 TitleImg = i.TitleImg,
                                 Number = s.Number,
                                 Color = s.Color,
                                 Size = s.Size,
                                 Quantity = i.Quantity
                             };

            return Json(latestData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCart(List<SelectedClothes> param)
        {
            try
            {
                // change quantity
                foreach (var item in param)
                {
                    var cloth = context.Selected.First(i => i.Id == item.Id);
                    cloth.Number = item.Number;
                }

                context.SaveChanges();

                //var forRemoving = context.Selected.Where(c => !param.Any(i => i.Id == c.Id)).ToList();
                List<SelectedClothes> forRemoving = new List<SelectedClothes>();

                // get element which was deleted
                foreach (var item in context.Selected)
                {
                    if (!param.Any(i => i.Id == item.Id))
                        forRemoving.Add(item);
                }


                foreach (var item in forRemoving)
                {
                    System.Diagnostics.Debug.WriteLine(item.Id + "\t" + item.Name);

                }

                if (forRemoving != null)
                {
                    forRemoving.ForEach(elem => context.Selected.Remove(elem));

                    context.SaveChanges();
                }
                return Json("successfull", JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult SellItems()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("sell items");

                var sessionId = Session["UserID"].ToString();

                var selectedClothes = context.Selected.Where(c => c.UserId.ToString() == sessionId).ToList();

                if (selectedClothes.All(c => c.Number <= context.ShopItems.FirstOrDefault(i => i.Id == c.ClothId).Quantity))
                {
                    (from s in selectedClothes
                     from i in context.ShopItems.Include(a => a.Colors).Include(a => a.Sizes).ToList()
                     where i.Id == s.ClothId && i.Name == s.Name && i.Colors.Any(c => c.Name == s.Color) /* && ((i.Sizes.Count > 0) ? i.Sizes.Any(n => n.Name == s.Size) : s.Size.Length == 0)*/ && i.Quantity >= s.Number
                     select i - s).ToList();

                    foreach (var selected in selectedClothes)
                    {
                        context.Selected.Remove(selected);
                    }

                    context.SaveChanges();

                    return Json(new { success = true, url = "/Home/Index" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, msg="Ops... select less quantity" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        private async void ReadCountryData()
        {
            await ReadFromFile();
        }

        private async Task ReadFromFile()
        {
            await Task.Run(async () =>
            {
                string json = String.Empty;
                using (StreamReader reader = new StreamReader("ListOfCountries.json"))
                {
                    json = await reader.ReadToEndAsync();
                }
                Countries = JsonConvert.DeserializeObject<List<Country>>(json);
            });
        }

        private string SizeEstablisher(string size)
        {
            switch (size)
            {
                case "37":
                    return ("s");

                case "38":
                    return ("M");

                case "39":
                    return ("L");

                case "40":
                    return ("XL");

                case "42":
                    return ("XXL");

                case "43":
                    return ("XXXL");

                default:
                    return null;
            }
        }
    }
}