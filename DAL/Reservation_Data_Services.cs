using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Final_Project_CGROUP2.Models;

namespace Final_Project_CGROUP2.DAL
{
    

    public class Reservation_Data_Services
    {
        public string cancelResarvation(string id,int totalNights,int totalPrice,string host_id,string apartment_id,string userEmail)
        {

            SqlConnection con = Connect();
            SqlCommand command = createCancelCommand(id, totalNights, totalPrice, host_id, apartment_id, userEmail, con);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            if (numAffected == 4)
            {
                return "succes 200";
            }
            return "error 404";
        }
        private SqlCommand createCancelCommand(string id,int totalNights,int totalPrice,string host_id,string apartment_id,string userEmail, SqlConnection con)
        {

            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@ResavationId", id);
            command.Parameters.AddWithValue("@totalNights", totalNights);
            command.Parameters.AddWithValue("@totalPrice", totalPrice);
            command.Parameters.AddWithValue("@apartment_id", apartment_id);
            command.Parameters.AddWithValue("@host_id", host_id);
            command.Parameters.AddWithValue("@userEmail", userEmail);
            command.CommandText = "spCancelResarvation";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;

        }

        //-------------------------------------------------------------get all user resavation into cart.html
        public List<Resavation> get_resarvation_list(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createResarvationListCommand(id, con);
           
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Resavation> resarvation_list = new List<Resavation>();
        
            while (dr.Read())
            {
                Resavation resavation = new Resavation();

                resavation.CheckIn = (DateTime)dr["Check_in"];
                resavation.CheckOut = (DateTime)dr["Check_out"];
                resarvation_list.Add(resavation);
            }

            if (resarvation_list.Count == 0)
            {
                Resavation resavationNull = new Resavation();

                resarvation_list.Add(resavationNull);
            }
            con.Close();
            return resarvation_list;

        }
        public SqlCommand createResarvationListCommand(int id, SqlConnection con)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spFPgetApartmentsResarvationById";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        public string addReservation(Resavation resavation)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateAddCommand(resavation, con);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            if (numAffected == 4)
            {
                return "status 200";
            }
            return "status 400";
        }

        private SqlCommand CreateAddCommand(Resavation resavation, SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@apartmentId", resavation.ApartmentId);
            command.Parameters.AddWithValue("@hostId", resavation.HostId);
            command.Parameters.AddWithValue("@email", resavation.User_email);
            command.Parameters.AddWithValue("@orderDate", resavation.OrderDate);
            command.Parameters.AddWithValue("@checkIn", resavation.CheckIn);
            command.Parameters.AddWithValue("@checkOut", resavation.CheckOut);
            command.Parameters.AddWithValue("@isCancel", resavation.Is_cancel);
            command.Parameters.AddWithValue("@total_price", resavation.TotalPrice);
            command.CommandText = "spFPInsertReservation";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        private SqlConnection Connect() //CONNECTION TO DB FUNCTION
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();
            return con;

        }
    }
}