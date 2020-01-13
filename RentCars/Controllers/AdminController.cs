using RentCars.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCars.Controllers
{
    public class AdminController : Controller
    {
        object error = null;

        // GET: Admin
        public ActionResult Workers()
        {
            if (Session["admin"] != "admin")
            {
                return RedirectToAction("Index", "Guest");
            }

            RentFunc rf = new RentFunc();
            List<Worker> workers = rf.GetWorkers();
            RecordResponse<List<Worker>> workersResponse = new RecordResponse<List<Worker>>();
            workersResponse.t = workers;

            return View(workersResponse);
        }

        public ActionResult AddWorker(string name, string username, string password, string role)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.AddWorker(name, username, password, role);

                List<Worker> workers = rf.GetWorkers();
                RecordResponse<List<Worker>> workersResponse = new RecordResponse<List<Worker>>();
                workersResponse.t = workers;
                workersResponse.Message = message;
                //
                // TODO: Add insert logic here

                return View("Workers", workersResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("Workers", null, error);
            }
        }

        public ActionResult DeleteWorker(int workerId)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.DeleteWorker(workerId);

                List<Worker> workers = rf.GetWorkers();
                RecordResponse<List<Worker>> workersResponse = new RecordResponse<List<Worker>>();
                workersResponse.t = workers;
                workersResponse.Message = message;
                //
                // TODO: Add insert logic here

                return View("Workers", workersResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("Workers", null, error);
            }
        }

        public ActionResult EditWorker(int workerId, string name, string username, string password, string role)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.EditWorker(workerId, name, username, password, role);

                List<Worker> workers = rf.GetWorkers();
                RecordResponse<List<Worker>> workersResponse = new RecordResponse<List<Worker>>();
                workersResponse.t = workers;
                workersResponse.Message = message;

                // TODO: Add insert logic here

                return View("Workers", workersResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("Workers", null, error);
            }
        }

        public ActionResult CarsInventory()
        {
            if (Session["admin"] != "admin")
            {
                return RedirectToAction("Index", "Guest");
            }

            RentFunc rf = new RentFunc();
            List<CarsInventory> cars = rf.GetCars();
            RecordResponse<List<CarsInventory>> carsResponse = new RecordResponse<List<CarsInventory>>();
            carsResponse.t = cars;

            return View(carsResponse);
        }

        public ActionResult AddCar(int carNumber, int carPriceList, int mileage, string valid, string available)
        {
            try
            {
                bool isvalid = false;
                bool isavailable = false;

                if (valid == "Valid")
                {
                    isvalid = true;
                }
                if (available == "Available")
                {
                    isavailable = true;
                }

                RentFunc rf = new RentFunc();
                string message = rf.AddCar(carNumber, carPriceList, mileage, isvalid, isavailable);

                List<CarsInventory> cars = rf.GetCars();
                RecordResponse<List<CarsInventory>> carsResponse = new RecordResponse<List<CarsInventory>>();
                carsResponse.t = cars;
                carsResponse.Message = message;
                //
                // TODO: Add insert logic here

                return View("CarsInventory", carsResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("CarsInventory", null, error);
            }
        }

        public ActionResult DeleteCar(int carNumber)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.DeleteCar(carNumber);

                List<CarsInventory> cars = rf.GetCars();
                RecordResponse<List<CarsInventory>> carsResponse = new RecordResponse<List<CarsInventory>>();
                carsResponse.t = cars;
                carsResponse.Message = message;
                //
                // TODO: Add insert logic here

                return View("CarsInventory", carsResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("CarsInventory", null, error);
            }
        }

        public ActionResult EditCar(int carNumber, int carPriceList, int mileage, bool isValid, bool isAvailable)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.EditCar(carNumber, carPriceList, mileage, isValid, isAvailable);

                List<CarsInventory> cars = rf.GetCars();
                RecordResponse<List<CarsInventory>> carsResponse = new RecordResponse<List<CarsInventory>>();
                carsResponse.t = cars;
                carsResponse.Message = message;

                // TODO: Add insert logic here

                return View("CarsInventory", carsResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("CarsInventory", null, error);
            }
        }

        public ActionResult CarPriceLists()
        {
            if (Session["admin"] != "admin")
            {
                return RedirectToAction("Index", "Guest");
            }
            RentFunc rf = new RentFunc();
            List<CarPriceList> carPriceLists = rf.GetCarPriceLists();
            RecordResponse<List<CarPriceList>> carPriceListsResponse = new RecordResponse<List<CarPriceList>>();
            carPriceListsResponse.t = carPriceLists;

            return View(carPriceListsResponse);
        }

        public ActionResult AddCarPriceList(string manufacturer, string model, int price, int delayPrice, int year, string gear_s, string carGroup, string image = null)
        {
                bool gear = false;
                
                if (gear_s == "Automatic")
                {
                    gear = true;
                }
            try
            {

                RentFunc rf = new RentFunc();
                string message = rf.AddPriceList(manufacturer, model, price, delayPrice, year, gear, carGroup, image);

                List<CarPriceList> carPriceLists = rf.GetCarPriceLists();
                RecordResponse<List<CarPriceList>> carPriceListsResponse = new RecordResponse<List<CarPriceList>>();
                carPriceListsResponse.t = carPriceLists;
                carPriceListsResponse.Message = message;
                //
                // TODO: Add insert logic here

                return View("CarPriceLists", carPriceListsResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("CarPriceLists", null, error);
            }
        }

        public ActionResult DeletePriceList(int carPriceListId)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.DeletePriceList(carPriceListId);

                List<CarPriceList> carPriceLists = rf.GetCarPriceLists();
                RecordResponse<List<CarPriceList>> carPriceListsResponse = new RecordResponse<List<CarPriceList>>();
                carPriceListsResponse.t = carPriceLists;
                carPriceListsResponse.Message = message;
                //
                // TODO: Add insert logic here

                return View("CarPriceLists", carPriceListsResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("CarPriceLists", null, error);
            }
        }

        public ActionResult EditPriceList(int carPriceListId, string manufacturer, string model, int price, int delayPrice, int year, string gear_s, string carGroup, string image = null)
        {
                bool gear = false;

                if (gear_s == "Automatic")
                {
                    gear = true;
                }
            try
            {

                RentFunc rf = new RentFunc();
                string message = rf.EditPriceList(carPriceListId, manufacturer, model, price, delayPrice, year, gear, carGroup, image);

                List<CarPriceList> carPriceLists = rf.GetCarPriceLists();
                RecordResponse<List<CarPriceList>> carPriceListsResponse = new RecordResponse<List<CarPriceList>>();
                carPriceListsResponse.t = carPriceLists;
                carPriceListsResponse.Message = message;

                // TODO: Add insert logic here

                return View("CarPriceLists", carPriceListsResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("CarPriceLists", null, error);
            }

        }

        public ActionResult Orders()
        {
            if (Session["admin"] != "admin")
            {
                return RedirectToAction("Index", "Guest");
            }
            RentFunc rf = new RentFunc();
            List<Order> orders = rf.GetOrders();
            RecordResponse<List<Order>> ordersResponse = new RecordResponse<List<Order>>();
            ordersResponse.t = orders;

            return View(ordersResponse);
        }

        public ActionResult DeleteOrder(int orderId)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.DeleteOrder(orderId);

                List<Order> orders = rf.GetOrders();
                RecordResponse<List<Order>> ordersResponse = new RecordResponse<List<Order>>();
                ordersResponse.t = orders;
                ordersResponse.Message = message;
                //
                // TODO: Add insert logic here

                return View("Orders", ordersResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("Orders", null, error);
            }
        }

        public ActionResult EditOrder(int orderId, DateTime startDate, DateTime returnDate, DateTime actualReturnDate, int tz, int carNumber, bool isActive)
        {
            try
            {
                RentFunc rf = new RentFunc();
                string message = rf.EditOrder(orderId, startDate, returnDate, actualReturnDate, tz, carNumber, isActive);

                List<Order> orders = rf.GetOrders();
                RecordResponse<List<Order>> ordersResponse = new RecordResponse<List<Order>>();
                ordersResponse.t = orders;
                ordersResponse.Message = message;

                // TODO: Add insert logic here

                return View("Orders", ordersResponse);
            }
            catch (Exception e)
            {
                error = e.ToString();
                return View("Orders", null, error);
            }
        }

    }
}