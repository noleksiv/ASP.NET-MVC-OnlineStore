using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneCode { get; set; }

        public List<State> States { get; set; }

        public Country()
        {
            States = new List<State>();
        }
    }
}