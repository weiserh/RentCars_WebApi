using RentCars.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCars.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult MyOrders()
        {
            RecordResponse<List<Order>> ordersResponse = new RecordResponse<List<Order>>();
            if (Session["UserName"] != null)
            {
                string username = Session["UserName"].ToString();
                RentFunc rf = new RentFunc();
                List<Order> orders = rf.GetMyOrders(username);
                ordersResponse.t = orders;                
            }
            return View(ordersResponse);
            
        }

        [HttpGet]
        public ActionResult OrderCar(string username, int carPriceListId, string startDate, string returnDate)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.AddOrder(username, carPriceListId, startDate, returnDate);

                List<Order> orders = rf.GetMyOrders(username);
                RecordResponse<List<Order>> ordersResponse = new RecordResponse<List<Order>>();
                ordersResponse.t = orders;
                ordersResponse.Message = message;

                return View("MyOrders", ordersResponse);
            }
            catch (Exception e)
            {
               string error = e.ToString();
                return View("MyOrders", username, error);
                //return View("MyOrders", e.Message);

            }
        }
    }
}