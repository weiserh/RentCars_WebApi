using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using RentCarsWebApi.Models;
using System.Xml.Linq;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;

namespace RentCarsWebApi.BL
{
    public class DbDal
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["RentCarsDb"].ConnectionString;

        public void GetOrders()
        { }

        public List<Order> GetOrders(int userId)
        {
            List<Order> orders = new List<Order>();
            string query = GetQueryFromXml("get_orders");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@userid", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order currentOrder = new Order
                        {
                            StartDate = reader.GetDateTime(0),
                            ReturnDate = reader.GetDateTime(1),
                            ActualReturnDate = reader.IsDBNull(2) ? new DateTime?() : reader.GetDateTime(2),
                            UserId = int.Parse(reader["UserId"].ToString()),
                            CarId = int.Parse(reader["CarId"].ToString()),
                            IsActive = bool.Parse(reader["IsActive"].ToString()),
                            Id = int.Parse(reader["Id"].ToString())
                        };
                        orders.Add(currentOrder);
                    }
                    con.Close();
                }
            }
            return orders;
        }

        //public void AddOrder(int userId, int priceListId, string startDate, string returnDate)
        public int AddOrder(int userId, int priceListId, DateTime startDate, DateTime returnDate)
        {
            string query = GetQueryFromXml("add_order");
            int orderId = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@returnDate", returnDate);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@isActive", false);
                cmd.Parameters.AddWithValue("@priceListId", priceListId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orderId = int.Parse(reader["orderId"].ToString());
                    }
                }
                con.Close();
            }
            return orderId;
        }

        public void DeleteOrder(int orderId)
        {
            string query = GetQueryFromXml("delete_order");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void EditOrder(int orderId, DateTime startDate, DateTime returnDate, int userId, int carId, bool isActive)
        {
            string query = GetQueryFromXml("update_order");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", orderId);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@returnDate", returnDate);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@isActive", false);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<PriceList> GetCars()
        {
            string query = GetQueryFromXml("search_cars");
            List<PriceList> availablePriceList = new List<PriceList>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PriceList currentPriceList = new PriceList
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Manufacturer = reader["Manufacturer"].ToString(),
                            Model = reader["Model"].ToString(),
                            Price = int.Parse(reader["Price"].ToString()),
                            DelayPrice = int.Parse(reader["DelayPrice"].ToString()),
                            Year = int.Parse(reader["Year"].ToString()),
                            Gear = bool.Parse(reader["Gear"].ToString()),
                            CarGroup = reader["Group"].ToString(),
                            Image = reader["Image"].ToString()
                        };
                        availablePriceList.Add(currentPriceList);
                    }
                    con.Close();
                }
            }
            return availablePriceList;
        }

        public List<PriceList> GetCars(int? year, string manufacturer, string model, string freeText, DateTime? startDate, DateTime? returnDate, bool? gearAuto)
        {
            var availablePriceLists = GetCars();
            if (freeText != "")
                availablePriceLists = availablePriceLists.Where(pl => pl.Manufacturer == freeText || pl.Model == freeText).ToList();

            if (year != null)
                availablePriceLists = availablePriceLists.Where(pl => pl.Year == year).ToList();

            if (manufacturer != "")
                availablePriceLists = availablePriceLists.Where(pl => pl.Manufacturer == manufacturer).ToList();

            if (model != "")
                availablePriceLists = availablePriceLists.Where(pl => pl.Model == model).ToList();

            if (gearAuto != null)
                availablePriceLists = availablePriceLists.Where(pl => pl.Gear == gearAuto).ToList();

            return availablePriceLists;
        }

        private string GetQueryFromXml(string queryName)
        {
            string dir = Directory.GetCurrentDirectory();
            XDocument doc = XDocument.Load(@"C:\Users\ronesn\Documents\Visual Studio 2017\Projects\RentCars_WebApi\RentCarsWebApi\BL\DbCmd.xml");
            var query = doc.Element("sqls").Element(queryName);
            return query.Value;
        }

        public bool Register(int id, string fullName, string username, string email, string password, string dob = null, string image = null)
        {
            string query = GetQueryFromXml("add_user");
            bool rows_effected = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fullName", fullName);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@image", image);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows_effected = bool.Parse(reader["rows_effected"].ToString());
                    }
                }
                con.Close();
            }
            return rows_effected;
        }

        public bool Signin(string username, string password)
        {
            string query = GetQueryFromXml("check_username_password");

            bool match = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        match = bool.Parse(reader["match"].ToString());
                    }
                }
                con.Close();
            }
            return match;
        }

        public bool WorkerSignin(string username, string password)
        {
            string query = GetQueryFromXml("check_worker_username_password");
            string table = "Worker";
            bool match = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@table", table);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        match = bool.Parse(reader["match"].ToString());
                    }
                }
                con.Close();
            }
            return match;
        }

    }
}