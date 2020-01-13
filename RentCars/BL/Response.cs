using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCars.BL
{
    public class RecordResponse<T>
    {
        public bool success { get; set; }
        public string Message { get; set; }
        public T t { get; set; }
    }

}