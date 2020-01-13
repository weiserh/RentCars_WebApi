using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCars.BL // note that is different from old project
{
    public class RentFunc
    {
        RentCarsDBEntities rentCarsDBE = new RentCarsDBEntities();

        public List<CarPriceList> SearchCars()
        {
            var availablePriceLists = (from cpl in rentCarsDBE.CarPriceLists
                                       join ci in rentCarsDBE.CarsInventories on

                                       cpl.CarPriceListId equals ci.CarPriceListId
                                       where ci.IsAvailable == true && ci.IsValid == true
                                       select cpl).Distinct().ToList();

            return availablePriceLists;
        }

        public List<CarPriceList> SearchCars(int? year, string manufacturer, string model, bool? gear, string freeText, DateTime? startDate, DateTime? returnDate)
        {
            var availablePriceLists = SearchCars();
            if (freeText != "")
            {
                availablePriceLists = availablePriceLists.Where(pl => pl.Manufacturer == freeText || pl.Model == freeText).ToList();
            }

            if (year != null)
            {
                availablePriceLists = availablePriceLists.Where(pl => pl.Year == year).ToList();
            }

            if (manufacturer != "")
            {
                availablePriceLists = availablePriceLists.Where(pl => pl.Manufacturer == manufacturer).ToList();
            }

            if (model != "")
            {
                availablePriceLists = availablePriceLists.Where(pl => pl.Model == model).ToList();
            }

            if (gear != null)
            {
                availablePriceLists = availablePriceLists.Where(pl => pl.Gear == gear).ToList();
            }

            //if (freeText != "")
            //{

            //}
            return availablePriceLists;

        }







        public RecordResponse<User> Register(int tz, string fullName, string username, string sex, string email, string password, string dob = null, string image = null)
        {
            //  UserResponse userResponse = new UserResponse();
            RecordResponse<User> userResponse = new RecordResponse<User>();

            userResponse.t = new User()
            {
                Tz = tz,
                FullName = fullName,
                Username = username,
                //DOB = DateTime.Parse(dob),
                Sex = sex,
                Email = email,
                Password = password,
                Image = image
            };
            if (dob != "")
            {
                userResponse.t.DOB = DateTime.Parse(dob);
            }
            if (rentCarsDBE.Users.Any(u => u.Username == userResponse.t.Username))
            {
                userResponse.success = false;
                userResponse.Message = "Username already exist!";
                return userResponse;
            }

            rentCarsDBE.Users.Add(userResponse.t);
            rentCarsDBE.SaveChanges();

            userResponse.success = true;
            userResponse.Message = "";
            return userResponse;
        }

        public string Signin(string username, string password)
        {
            var user = rentCarsDBE.Users.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return "Username doesn't exist";
            }
            else
            {
                if (user.Password != password)
                {
                    return "Password is incorrect";
                }
            }
            return "Login success";
        }

        public string WorkerSignin(string username, string password)
        {
            var worker = rentCarsDBE.Workers.FirstOrDefault(x => x.Username == username);
            if (worker == null)
            {
                return "Username doesn't exist";
            }
            else
            {
                if (worker.Password != password)
                {
                    return "Password is incorrect";
                }
            }

            return $"{worker.Role} login success";
        }

        public List<Worker> GetWorkers()
        {
            var workers = rentCarsDBE.Workers.ToList();

            return workers;
        }

        public List<Worker> GetWorkers(string workerName) //filter workers 
        {
            var workers = rentCarsDBE.Workers.Where(w => w.Name == workerName).ToList();
            return workers;
        }

        public string AddWorker(string name, string username, string password, string role)
        {
            Worker worker = new Worker()
            {
                Name = name,
                Username = username,
                Password = password,
                Role = role
            };
            if (rentCarsDBE.Workers.Any(w => w.Username == worker.Username))
            {
                return $"Username {worker.Username} already exist!";
            }

            rentCarsDBE.Workers.Add(worker);
            rentCarsDBE.SaveChanges();

            return $"Employee {worker.Username} was added";
        }

        public string DeleteWorker(int workerId)
        {
            Worker worker = rentCarsDBE.Workers.FirstOrDefault(w => w.WorkerId == workerId);

            rentCarsDBE.Workers.Remove(worker);
            rentCarsDBE.SaveChanges();

            return $"Employee {worker.Username} was deleted";
        }

        public string EditWorker(int workerId, string name, string username, string password, string role)
        {
            var worker = rentCarsDBE.Workers.SingleOrDefault(w => w.WorkerId == workerId);
            if (worker != null)
            {
                worker.Name = name;
                worker.Username = username;
                worker.Password = password;
                worker.Role = role;
                rentCarsDBE.SaveChanges();
                return $"Employee {workerId} was updated";
            }
            else
            {
                return $"Something went wrong, employee {workerId} wasn't updated";
            }
        }

        public List<CarsInventory> GetCars()
        {
            var cars = rentCarsDBE.CarsInventories.ToList();
            return cars;
        }

        public string AddCar(int carNumber, int carPriceList, int mileage, bool isValid, bool isAvailable)
        {
            CarsInventory car = new CarsInventory()
            {
                CarNumber = carNumber,
                CarPriceListId = carPriceList,
                Mileage = mileage,
                IsValid = isValid,
                IsAvailable = isAvailable
            };
            if (rentCarsDBE.CarsInventories.Any(c => c.CarNumber == car.CarNumber))
            {
                return $"Car number {car.CarNumber} already exist!";
            }

            rentCarsDBE.CarsInventories.Add(car);
            rentCarsDBE.SaveChanges();

            return $"Car {car.CarNumber} was added";
        }

        public string DeleteCar(int carNumber)
        {
            CarsInventory car = rentCarsDBE.CarsInventories.FirstOrDefault(c => c.CarNumber == carNumber);

            if (rentCarsDBE.Orders.Any(c => c.CarNumber == carNumber))
            {
                return "This car has existing orders, and cannot be deleted.";
            }
            else
            {
                rentCarsDBE.CarsInventories.Remove(car);
                rentCarsDBE.SaveChanges();
                return $"Car {carNumber} was deleted";
            }
        }

        public string EditCar(int carNumber, int carPriceList, int mileage, bool isValid, bool isAvailable)
        {
            var car = rentCarsDBE.CarsInventories.SingleOrDefault(c => c.CarNumber == carNumber);
            if (car != null)
            {
                car.CarNumber = carNumber;
                car.CarPriceListId = carPriceList;
                car.Mileage = mileage;
                car.IsValid = isValid;
                car.IsAvailable = isAvailable;
                rentCarsDBE.SaveChanges();
                return $"Car {car.CarNumber} was updated";
            }
            else
            {
                return $"Something went wrong, car {car.CarNumber} wasn't updated";
            }
        }

        public List<CarPriceList> GetCarPriceLists()
        {
            var carPriceLists = rentCarsDBE.CarPriceLists.ToList();
            return carPriceLists;
        }

        public string AddPriceList(string manufacturer, string model, int price, int delayPrice, int year, bool gear, string carGroup, string image = null)
        {
            CarPriceList carPriceList = new CarPriceList()
            {
                Manufacturer = manufacturer,
                Model = model,
                Price = price,
                DelayPrice = delayPrice,
                Year = year,
                Gear = gear,
                CarGroup = carGroup,
                Image = image
            };

            rentCarsDBE.CarPriceLists.Add(carPriceList);
            rentCarsDBE.SaveChanges();

            return $"{carPriceList.Manufacturer} {carPriceList.Model} car price-list was added - PriceListId is: {carPriceList.CarPriceListId}";
        }

        public string DeletePriceList(int priceListId)
        {
            CarPriceList priceList = rentCarsDBE.CarPriceLists.FirstOrDefault(pl => pl.CarPriceListId == priceListId);

            if (rentCarsDBE.CarsInventories.Any(c => c.CarPriceListId == priceListId))
            {
                return "This price-list has existing cars and cannot be deleted.";
            }
            else
            {
                rentCarsDBE.CarPriceLists.Remove(priceList);
                rentCarsDBE.SaveChanges();

                return $"{priceList.Manufacturer} {priceList.Model} car price-list was deleted";
            }
        }


        public string EditPriceList(int carPriceListId, string manufacturer, string model, int price, int delayPrice, int year, bool gear, string carGroup, string image = null)
        {
            var carPriceList = rentCarsDBE.CarPriceLists.SingleOrDefault(cpl => cpl.CarPriceListId == carPriceListId);
            if (carPriceList != null)
            {
                carPriceList.Manufacturer = manufacturer;
                carPriceList.Model = model;
                carPriceList.Price = price;
                carPriceList.DelayPrice = delayPrice;
                carPriceList.Year = year;
                carPriceList.Gear = gear;
                carPriceList.CarGroup = carGroup;
                carPriceList.Image = image;

                rentCarsDBE.SaveChanges();
                return $"{carPriceList.Manufacturer} {carPriceList.Model} car price-list was updated";
            }
            else
            {
                return $"Something went wrong, {carPriceList.Manufacturer} {carPriceList.Model} car price-list wasn't updated";
            }
        }

        public List<Order> GetOrders()
        {
            var orders = rentCarsDBE.Orders.ToList();
            return orders;
        }

        public List<Order> GetMyOrders(string username)
        {
            User user = rentCarsDBE.Users.FirstOrDefault(u => u.Username == username);

            var orders = rentCarsDBE.Orders.Where(o => o.TZ == user.Tz).ToList();
            return orders;
        }

        public string AddOrder(string username, int carPriceListId, string startDate, string returnDate)
        {
            Order order = new Order();
            User user = rentCarsDBE.Users.FirstOrDefault(u => u.Username == username);
            CarsInventory car = rentCarsDBE.CarsInventories.FirstOrDefault(c => c.CarPriceListId == carPriceListId && c.IsAvailable == true);


            order.StartDate = DateTime.Parse(startDate);
            //order.StartDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(startDate);
            order.ReturnDate = DateTime.Parse(returnDate);
            order.TZ = user.Tz;
            order.CarNumber = car.CarNumber;
            order.IsActive = true;

            car.IsAvailable = false;

            rentCarsDBE.Orders.Add(order);
            rentCarsDBE.SaveChanges();

            return $"Order {order.Id} was completed successfully";
        }

        public string DeleteOrder(int orderId)
        {
            Order order = rentCarsDBE.Orders.FirstOrDefault(o => o.Id == orderId);

            rentCarsDBE.Orders.Remove(order);
            rentCarsDBE.SaveChanges();

            return $"Order {order.Id} was deleted";
        }

        public string EditOrder(int orderId, DateTime startDate, DateTime returnDate, DateTime actualReturnDate, int tz, int carNumber, bool isActive)

        {
            var order = rentCarsDBE.Orders.SingleOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.StartDate = startDate;
                order.ReturnDate = returnDate;
                order.ActualReturnDate = actualReturnDate;
                order.TZ = tz;
                order.CarNumber = carNumber;
                order.IsActive = isActive;

                rentCarsDBE.SaveChanges();
                return $"Order {order.Id} was updated";
            }
            else
            {
                return $"Something went wrong, order {order.Id} wasn't updated";
            }
        }


        public RecordResponse<CarsInventory> ReturnCar(int carNum)
        {
            RecordResponse<CarsInventory> returnedCarResponse = new RecordResponse<CarsInventory>();
            returnedCarResponse.t = rentCarsDBE.CarsInventories.SingleOrDefault(c => c.CarNumber == carNum);

            if (returnedCarResponse.t == null)
            {
                returnedCarResponse.Message = $"Car number {carNum} doesn't exist in inventory";
                return returnedCarResponse;
            }
            if (returnedCarResponse.t.IsAvailable == true)
            {
                returnedCarResponse.Message = $"Car number {carNum} is not rented";
                return returnedCarResponse;
            }

            Order order = rentCarsDBE.Orders.FirstOrDefault(o => o.CarNumber == carNum && o.IsActive == true);

            var carPriceList = (from cpl in rentCarsDBE.CarPriceLists
                                join ci in rentCarsDBE.CarsInventories on

                                cpl.CarPriceListId equals ci.CarPriceListId
                                where ci.CarNumber == carNum
                                select cpl).ToList();

            order.ActualReturnDate = DateTime.Now;
            returnedCarResponse.t.IsAvailable = true;
            order.IsActive = false;
            rentCarsDBE.SaveChanges();

            double regularCost = ((DateTime.Now - order.ReturnDate).TotalDays + 1) * carPriceList[0].Price;
            double delayCost = carPriceList[0].DelayPrice;
            returnedCarResponse.Message = $"Car number {carNum} is now available in inventory\n" +
                $"Cost for rent in regular fee: {regularCost}\n";
            if ((order.ReturnDate - order.StartDate).TotalDays > 0)
            {
                delayCost = (order.ReturnDate - order.StartDate).TotalDays * carPriceList[0].Price;
                returnedCarResponse.Message += $"Cost for rent in delay fee: {delayCost}\n";
            }
            returnedCarResponse.Message += $"Totsl rental cost {regularCost + delayCost}";

            return returnedCarResponse;
        }
    }
}