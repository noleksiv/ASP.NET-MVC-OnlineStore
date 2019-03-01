using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using OnlineStore.Domain.Models;

namespace OnlineStore.WebUI.Models
{
    static public class Converter
    {
        static public string ListToUrl(List<Clothing> llist)
        {
            var param = new StringBuilder("");

            foreach (var item in llist)
            {
                param.Append($"?name={item.Name_}&size={item.Size}&color={item.Color}&number={item.Number}");
            }

            //System.Diagnostics.Debug.WriteLine(param.ToString());
            return param.ToString();
        }

        static public List<Clothing> UrlToList (string param)
        {
            //System.Diagnostics.Debug.WriteLine(param);

            var llist = new List<Clothing>();
            var dividedStr = param.Remove(0, 1).Split('?');

            foreach (var subStr in dividedStr)
            {
                System.Diagnostics.Debug.WriteLine(subStr);

                var parameters = subStr.Split('&');

                // url parameters into object's fields : name=x&size=x&color=X&number=x
                llist.Add( new Clothing
                {
                    Name = parameters[0].Substring(parameters[0].IndexOf('=')+1),
                    Size = parameters[1].Substring(parameters[1].IndexOf('=')+1),
                    Color = parameters[2].Substring(parameters[2].IndexOf('=')+1),
                    Number = int.Parse(subStr.Substring(subStr.LastIndexOf('=')+1))
                });
            }

            foreach (var item in llist)
            {
                System.Diagnostics.Debug.WriteLine(item.Name + "\t" + item.Size + "\t" + item.Color + "\t" + item.Number);
            }

            return llist;
        }

        static public string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }
    }
}