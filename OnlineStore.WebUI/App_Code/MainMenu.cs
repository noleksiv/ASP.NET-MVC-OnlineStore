using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace OnlineStore.WebUI.App_Code
{
    public class MainMenu
    {
        public static MvcHtmlString CreateTopBar(Dictionary<string,string> items, object htmlAttributes = null)
        {
            var divParent = new TagBuilder("div");
            divParent.AddCssClass("right-top-bar flex-w h-full");
            var divChild = new TagBuilder("div");
            divChild.AddCssClass("flex-c-m trans-04 p-lr-25");
            foreach (var item in items)
            {
                TagBuilder a = new TagBuilder("a");
                a.Attributes["href"] = item.Value;
                a.SetInnerText(item.Key);
                a.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
                divChild.InnerHtml += a.ToString();
            }
            divParent.InnerHtml += divChild;
            return  MvcHtmlString.Create(divParent.ToString());
        }

        public static MvcHtmlString CreateMenuItems(string[] items, object htmlAttributes = null)
        {
            TagBuilder ul = new TagBuilder("ul");            

            foreach (string item in items)
            {                
                TagBuilder a = new TagBuilder("a");
                TagBuilder li = new TagBuilder("li");

                //a.Attributes["href"] = "/Home/" + item;
                a.Attributes["href"] = $"/{item}/Index";
                a.SetInnerText(item);                

                if (item == "Shop")
                {
                    if (htmlAttributes.ToString().EndsWith("-m"))
                    {
                        a.AddCssClass("label1 rs1");
                        a.Attributes["data-label1"] = "hot";
                    }
                    else
                    {                       
                        li.AddCssClass("label1");
                        li.Attributes["data-label1"] = "hot";
                    }
                }

                li.InnerHtml += a;
                ul.InnerHtml += li;
            }
            
            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return MvcHtmlString.Create(ul.ToString());
        }

        public static MvcHtmlString IconHeader(string firstPar=null, string secondParam=null, string thirdParam = null)
        {  
            var div = new TagBuilder("div");
            div.AddCssClass("wrap-icon-header flex-w flex-r-m " + firstPar);
            var childDiv1 = new TagBuilder("div");
            childDiv1.AddCssClass("icon-header-item cl2 hov-cl1 trans-04 " + secondParam + " p-r-11 js-show-modal-search");
            var i = new TagBuilder("i");
            i.AddCssClass("zmdi zmdi-search");

            childDiv1.InnerHtml += i;
            div.InnerHtml += childDiv1;

            var childDiv3 = new TagBuilder("div");
            childDiv3.AddCssClass("dis-block icon-header-item cl2 hov-cl1 trans-04 " + thirdParam + " js-show-list");

            var i3 = new TagBuilder("i");
            i3.AddCssClass("zmdi zmdi-favorite-outline");

            childDiv3.InnerHtml += i3;
            div.InnerHtml += childDiv3;

            var childDiv2 = new TagBuilder("div");
            childDiv2.AddCssClass("icon-header-item cl2 hov-cl1 trans-04 " + secondParam + " p-r-11 icon-header-noti js-show-cart quantity-in-cart");
            // Notify in menu.
            childDiv2.Attributes["data-notify"] = "0";
            var i2 = new TagBuilder("i");
            i2.AddCssClass("zmdi zmdi-shopping-cart");

            childDiv2.InnerHtml += i2;
            div.InnerHtml += childDiv2;

          

            return MvcHtmlString.Create(div.ToString());
        }

        public static MvcHtmlString MobileMenuItems(string[] LeftTopMenu, object LeftTopAttributes, Dictionary<string, string> RightTopMenu, object RightTopAttributes, string freeDelivery=null)
        {
            var divParent = new TagBuilder("div");
            divParent.AddCssClass("menu-mobile");
            var ul = new TagBuilder("ul");
            ul.AddCssClass("topbar-mobile");
            var li1 = new TagBuilder("li");
            var divChild1 = new TagBuilder("div");
            divChild1.AddCssClass("left-top-bar");
            divChild1.InnerHtml = freeDelivery;

            li1.InnerHtml += divChild1;
            ul.InnerHtml += li1;            

            var li2 = new TagBuilder("li");
            var divChild2 = new TagBuilder("div");
            divChild2.AddCssClass("right-top-bar flex-w h-full");

            divChild2.InnerHtml += CreateTopBar(RightTopMenu, RightTopAttributes).ToString();

            li2.InnerHtml += divChild2;
            ul.InnerHtml += li2;


            divParent.InnerHtml += ul;
            divParent.InnerHtml += CreateMenuItems(LeftTopMenu, LeftTopAttributes).ToString();

            return MvcHtmlString.Create(divParent.ToString());

        }
    }
}

