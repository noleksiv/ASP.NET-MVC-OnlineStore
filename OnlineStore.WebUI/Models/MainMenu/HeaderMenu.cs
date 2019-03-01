using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models.MainMenu
{
    public class HeaderMenu
    {
        public Dictionary<string,string> MenuItems { get; set; }

        public HeaderMenu()
        {
            MenuItems = new Dictionary<string, string>
            {
                { "Shop", "Shop" },
                { "Features", "Features" },
                { "Blog", "Blog" },
                { "About", "About" },
                { "Contact", "Contact" }
            };

        }
    }
}