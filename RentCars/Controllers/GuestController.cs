using RentCars.BL;
using RentCarsWebApi.BL;
using RentCarsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCars.Controllers
{
    public class GuestController : Controller
    {
        List<CarPriceList> availablePriceLists = null;
        List<PriceList> availablePriceLists_new = null;
        string loginStatus = null;
        object error = null;
        // GET: General
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult recentSearch()
        {

            return View();
        }

        public ActionResult SearchCarWindow() //SearchCars
        {
            RentFunc rf = new RentFunc(); 

            availablePriceLists = rf.SearchCars();
            //DbDal db = new DbDal();
            //availablePriceLists_new = db.SearchCars();
            return View(availablePriceLists_new);
        }

        public ActionResult SearchCars(int? year, string manufacturer, string model, string freeText, DateTime? startDate, DateTime? returnDate, string gear = "Both")
        {
            RentFunc rf = new RentFunc();
            bool? gearBool = null;

            if (gear != "Both" && gear != "")
            {
                if (gear == "Automatic")
                {
                    gearBool = true;
                }
                else
                {
                    gearBool = false;
                }
            }

            availablePriceLists = rf.SearchCars(year, manufacturer, model, gearBool, freeText, startDate, returnDate);
            return View("SearchCarWindow", availablePriceLists);
        }

        public ActionResult Login()
        {
            return View(error);
        }
        public ActionResult LogOut()
        {
            Session["UserName"] = null;
            Session["workerUsername"] = null;
            Session["admin"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult WorkerLogin()
        {
            return View(error);
        }


        // GET: General
        public ActionResult Registration()
        {
            return View(error);
        }

        // GET: General
        public ActionResult About()
        {
            return View();
        }

        // GET: General
        public ActionResult Contactus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(int tz, string fullName, string username, string dob, string genderRadio, string email, string password, string image = null)
        {

            try
            {
                RentFunc rf = new RentFunc();

                RecordResponse<User> userResponse = rf.Register(tz, fullName, username, genderRadio, email, password, dob, image);
                // TODO: Add insert logic here
                if (userResponse.success)
                {
                    return Signin(userResponse.t.Username, userResponse.t.Password);
                }
                else
                {
                    return View("Registration", userResponse);
                }
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("Registration", null, error);
            }
        }

        public ActionResult Signin(string username, string password)
        {
            RentFunc rf = new RentFunc();

            loginStatus = rf.Signin(username, password);

            if (loginStatus != "Login success")
            {
                ViewBag.loginStatus = loginStatus;

                return View("Registration");
            }

            // TODO: Add insert logic here
            Session["UserName"] = username;

            return RedirectToAction("Index");
        }

        public ActionResult WorkerSignin(string username, string password)
        {
            RentFunc rf = new RentFunc();

            loginStatus = rf.WorkerSignin(username, password);

            if (!loginStatus.Contains("success"))
            {
                ViewBag.loginStatus = loginStatus;

                return View("Registration"); //contact your Administrator
            }


            if (loginStatus == "Manager login success")
            {
                Session["admin"] = "admin";
            }
            else
            {
                Session["employee"] = "employee";
            }

            // TODO: Add insert logic here
            Session["workerUsername"] = username;

            return RedirectToAction("ReturnCar", "Employee");
        }

        public ActionResult Signout()
        {
            // TODO: Add insert logic here
            Session["UserName"] = "";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult isSignin(string username, string password, string startDate, string returnDate)
        {
            // some code 

            return Json(new { message = Session["UserName"] }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SigninW(string username, string password, string startDate, string returnDate)
        {
            // some code 

            RentFunc rf = new RentFunc();
            loginStatus = rf.Signin(username, password);
            if (loginStatus.Contains("success"))
            {
                Session["UserName"] = username;
            }
            
            return Json(new { message = loginStatus, StartDate = startDate, ReturnDate = returnDate }, JsonRequestBehavior.AllowGet);
            //  rf.Signin(username, password);

            //if (loginStatus == "Login success")
            //{
            //    //ViewBag.loginStatus = loginStatus;
            //    return Json(new { success = true, message = "LogIn successfully" }, JsonRequestBehavior.AllowGet);

            //}
            //else
            //{
            //    return Json(new { success = false, message = "Login Faild" }, JsonRequestBehavior.AllowGet);
            //}
        }
    }
}
