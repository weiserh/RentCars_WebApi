using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace RentCarsWebApi.Models
{
    [Table(Name = "Order")]
    public class Order
    {
        public int Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime ReturnDate { get; set; }
        public Nullable<System.DateTime> ActualReturnDate { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public bool IsActive { get; set; }
    }
}