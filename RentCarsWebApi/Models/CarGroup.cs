using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace RentCarsWebApi.Models
{
    [Table(Name = "CarGroup")]
    public class CarGroup
    {
        public int Id { get; set; }
        public string Group { get; set; }
    }
}