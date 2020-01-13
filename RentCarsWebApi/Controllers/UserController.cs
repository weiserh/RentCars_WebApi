using RentCarsWebApi.BL;
using RentCarsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentCarsWebApi.Controllers
{
    public class UserController : ApiController
    {
        //[Route("api/orders/{userId}")]
        [HttpGet]
        public List<Order> GetOrders(int userId)
        {
            DbDal dbDal = new DbDal();
            return dbDal.GetOrders(userId);
        }

        // public void AddOrder(int userId, int priceListId, string startDate, string returnDate)
        [HttpGet]
        public int AddOrder()
        {
            int userId = 333333333;
            int priceListId = 23;
            DateTime startDate = new DateTime(2020, 01, 11);
            DateTime returnDate = new DateTime(2020, 01, 14);
            DbDal dbDal = new DbDal();
            return dbDal.AddOrder(userId, priceListId, startDate, returnDate);
        }



    }
}
