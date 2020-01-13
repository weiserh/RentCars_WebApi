using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace RentCarsWebApi.Models
{
    [Table(Name = "PriceList")]
    public class PriceList
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public int DelayPrice { get; set; }
        public int Year { get; set; }
        public bool Gear { get; set; }
        public string CarGroup { get; set; }
        public string Image { get; set; }
    }
}