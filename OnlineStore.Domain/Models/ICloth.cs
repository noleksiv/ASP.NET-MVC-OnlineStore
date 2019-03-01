using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Models
{
    public interface ICloth
    {
         int Id { get; set; }
         string Name { get; set; }
         string Color { get; set; }
         string Size { get; set; }
    }
}
