using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace RentCarsWebApi.Models
{
    [Table(Name = "Car")]
    public class Car
    {
        public int Id { get; set; }
        public int PriceListId { get; set; }
        public int Mileage { get; set; }
        public bool IsValid { get; set; }
        public bool IsAvailable { get; set; }
    }
}