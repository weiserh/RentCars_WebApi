using RentCars.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCars.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult ReturnCar()
        {
            if (Session["employee"] != "employee")
            {
                return RedirectToAction("Index", "Guest");
            }
            return View();
        }

        public ActionResult Return(string carNum_s)
        {
            int carNum = int.Parse(carNum_s);
            RentFunc rf = new RentFunc();
            ViewBag.returnResponse = rf.ReturnCar(carNum);

            return View("ReturnCarWindow");
        }
    }
}