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
    public class GuestController : ApiController
    {

        public List<PriceList> GetCars()
        {
            DbDal dbDal = new DbDal();
            return dbDal.GetCars();
        }

        [HttpGet]
        [ActionName("filter-cars")]
        public List<PriceList> GetCars(int? year, string manufacturer, string model, string freeText, DateTime? startDate, DateTime? returnDate, string gear = "Both")
        {
            bool? gearAuto = null;

            if (gear != "Both" && gear != "")
            {
                if (gear == "Automatic")
                {
                    gearAuto = true;
                }
                else
                {
                    gearAuto = false;
                }
            }
            DbDal dbDal = new DbDal();
            return dbDal.GetCars(year, manufacturer, model, freeText, startDate, returnDate, gearAuto);
        }

        //string loginStatus = null;
        //object error = null;

        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "testtt";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public bool Register(int id, string fullName, string username, string dob, string email, string password, string image = null)
        {
            DbDal dbDal = new DbDal();

            return dbDal.Register(id, fullName, username, email, password, dob, image);
            // TODO: Add insert logic here
            //if (success)
                   // return Signin(userResponse.t.Username, userResponse.t.Password);
        }

        [HttpGet]
        public bool Signin(string username, string password)
        {
            DbDal dbDal = new DbDal();

            return dbDal.Signin(username, password);
        }

        //public void WorkerSignin(string username, string password)
        //{
        //    DbDal dbDal = new DbDal();
        //    return dbDal.WorkerSignin(username, password);

        //}
    }
}
