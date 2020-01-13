using RentCars.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentCars.ApiControllers
{
    [Route("")]
    public class AdminApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void SignIn([FromBody]string username, string password)
        {
            RentFunc rf = new RentFunc();
            rf.Signin(username, password);
        }

        // POST api/<controller>
        [HttpPost]
        public void EditWorker([FromBody]Worker worker)
        {
            RentFunc rf = new RentFunc();
            rf.EditWorker(worker.WorkerId, worker.Name, worker.Username, worker.Password, worker.Role);
        }


        [HttpPost]
        public void EditOrder([FromBody]Order order)
        {
            RentFunc rf = new RentFunc();
            rf.EditOrder(order.Id, order.StartDate, order.ReturnDate, order.ActualReturnDate, order.TZ, order.CarNumber, order.IsActive);
        }

        [HttpPost]
        public void EditCar([FromBody]CarsInventory car)
        {
            RentFunc rf = new RentFunc();
            rf.EditCar(car.CarNumber, car.CarPriceListId, car.Mileage, car.IsValid, car.IsAvailable);
        }

        [HttpPost]
        public void EditPriceList([FromBody]CarPriceList priceList)
        {
            RentFunc rf = new RentFunc();
            rf.EditPriceList(priceList.CarPriceListId, priceList.Manufacturer, priceList.Model, priceList.Price, priceList.DelayPrice, priceList.Year, priceList.Gear, priceList.CarGroup, priceList.Image);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}