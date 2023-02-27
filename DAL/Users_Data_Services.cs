using Final_Project_CGROUP2.Auxiliary_classes;
using Final_Project_CGROUP2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Final_Project_CGROUP2.DAL
{
    public class Users_Data_Services
    {
        public IEnumerable<User> getAllUser_Chat()
        {

            SqlConnection con = Connect();
            SqlCommand command = getAllUsersChatCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> Users = new List<User>();
            while (dr.Read())
            {
                User user = new User();
                if (dr["userName"] != System.DBNull.Value)
                    user.UserName = dr["userName"].ToString();

                if (dr["email"] != System.DBNull.Value)
                    user.Email = dr["email"].ToString();
                Users.Add(user);
            }
            con.Close();
            return Users;
        }
        public SqlCommand getAllUsersChatCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            
            command.CommandText = "spFPGetAllUsers_chat";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        public List<Apartment> getUserFavorites(string email)
        {
            SqlConnection con = Connect();
            SqlCommand command = GetUserFavoritesCommand(con, email);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> apartments = new List<Apartment>();
            while (dr.Read())
            {
                Apartment ap = new Apartment();
                Reviews rev = new Reviews();
                if (dr["picture_url"] != System.DBNull.Value)
                    ap.Picture_url = (string)dr["picture_url"];

                if (dr["description"] != System.DBNull.Value)
                    ap.Description = (string)dr["description"];

                if (dr["name"] != System.DBNull.Value)
                    ap.Name = dr["name"].ToString();

                if (dr["accommodates"] != System.DBNull.Value)
                    ap.Accomodates = Convert.ToInt32(dr["accommodates"]);

                if (dr["id"] != System.DBNull.Value)
                    ap.Id = (dr["id"]).ToString();

                if (dr["review_scores_rating"] != System.DBNull.Value)
                    rev.Review_scores_rating = (double)dr["Review_scores_rating"];

                if (dr["bedrooms"] != System.DBNull.Value)
                    ap.Bedrooms = Convert.ToInt32(dr["bedrooms"]);

                if (dr["price"] != System.DBNull.Value)
                    ap.Price = dr["price"].ToString();
                ap.Reviews = rev;
                apartments.Add(ap);
            }
            con.Close();
            return apartments;
        }
        private SqlCommand GetUserFavoritesCommand(SqlConnection con, string email)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@useremail", email);
            command.CommandText = "spFPGetUserFavorites";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }



        public Dictionary<string, int> getUserLikes(string useremail)
        {
            SqlConnection con = Connect();
            SqlCommand command = GetUserLikesCommand(con, useremail);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Dictionary<string,int> currUserLikes = new Dictionary<string, int>();
            while (dr.Read())
            {
              
                currUserLikes.Add((string)dr["Apartment_id"],1);
            }
            con.Close();
            return currUserLikes;


        }

        private SqlCommand GetUserLikesCommand(SqlConnection con, string email)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@useremail", email);
            command.CommandText = "spFPGetUserLikes";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
     

        public IEnumerable<User> GetAdminUsers()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadAllUsersCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> users = new List<User>();
            while (dr.Read())
            {
                User user = new User();
                if (dr["userName"] != System.DBNull.Value)
                    user.UserName = dr["userName"].ToString();

                if (dr["email"] != System.DBNull.Value)
                    user.Email = dr["email"].ToString();

                if (dr["Sign_up_date"] != System.DBNull.Value)
                    user.Sign_up_date = (DateTime)dr["Sign_up_date"];

                if (dr["User_total_income"] != System.DBNull.Value)
                    user.Total_income = Convert.ToInt32( dr["User_total_income"]);

                if (dr["Total_reservations"] != System.DBNull.Value)
                    user.Total_reservations = Convert.ToInt32(dr["Total_reservations"]);

                if (dr["Total_Cancellations"] != System.DBNull.Value)
                    user.Total_cancellations = Convert.ToInt32(dr["Total_Cancellations"]);

                users.Add(user);
            }
            con.Close();
            return users;
        }
        private SqlCommand CreateReadAllUsersCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "spFPGetAdminUsers";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

    
        public IEnumerable<Apartment> get_all_usersApartments(string email)
        {

            SqlConnection con = Connect();
            SqlCommand command = CreateReadUsersApartmentsCommand(con, email);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> user_apartments = new List<Apartment>();

            while (dr.Read())
            {
                Apartment ap = new Apartment();
                Resavation resavation = new Resavation();
                Host host = new Host();
                if (dr["host_id"] != System.DBNull.Value)
                    host.Host_Id = (dr["host_id"]).ToString();

                if (dr["picture_url"] != System.DBNull.Value)
                    ap.Picture_url = (string)dr["picture_url"];

                if (dr["Check_in"] != System.DBNull.Value)
                    resavation.CheckIn = (DateTime)dr["Check_in"];

                if (dr["Check_out"] != System.DBNull.Value)
                    resavation.CheckOut = (DateTime)dr["Check_out"];

                if (dr["Resavation_Date"] != System.DBNull.Value)
                    resavation.OrderDate = (DateTime)dr["Resavation_Date"];

                if (dr["Cancalled"] != System.DBNull.Value)
                    resavation.Is_cancel = (bool)dr["Cancalled"];

                if (dr["name"] != System.DBNull.Value)
                    ap.Name = dr["name"].ToString();

                if (dr["Total_price"] != System.DBNull.Value)
                    resavation.TotalPrice = Convert.ToInt32(dr["Total_price"]);

                if (dr["accommodates"] != System.DBNull.Value)
                    ap.Accomodates = Convert.ToInt32(dr["accommodates"]);
              
                if (dr["id"] != System.DBNull.Value)
                    ap.Id = (dr["id"]).ToString();

                if (dr["Resavation_id"] != System.DBNull.Value)
                    resavation.Resavation_id = (dr["Resavation_id"]).ToString();

                ap.Host = host;
                ap.Resavations_list = new List<Resavation>();
                ap.Resavations_list.Add(resavation);
                user_apartments.Add(ap);
            }
            if (user_apartments.Count == 0)
            {
                Apartment nullapartment = new Apartment();
                user_apartments.Add(nullapartment);
            }
            con.Close();
            return user_apartments;

        }

        private SqlCommand CreateReadUsersApartmentsCommand(SqlConnection con, string email)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@email", email);
            command.CommandText = "spFPGetUsersApartments";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }







        public User getUser(string email)
        {

            SqlConnection con = Connect();
            SqlCommand command = CreateReadUsersCommand(con, email);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            User user = new User();
            while (dr.Read())
            {
                user.UserName = dr["userName"].ToString();
                user.Password = dr["password"].ToString();
                user.Email = dr["email"].ToString();
            }
            con.Close();
            return user;
        }
        private SqlCommand CreateReadUsersCommand(SqlConnection con, string email)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@email", email);
            command.CommandText = "spFPGetUsers";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }


        public int add_user(User user)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateInsertCommand(con, user);
            int numAffected = -1;
            try
            {
                numAffected = command.ExecuteNonQuery();
            }catch(Exception e)
            {
                numAffected = -1;
            }
            con.Close();
            return numAffected;

        }



        private SqlCommand CreateInsertCommand(SqlConnection con, User user) //INSERT COMMAND FOR SQL USER
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@userName", user.UserName);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@Sign_up_date", user.Sign_up_date);
            command.CommandText = "spInsertUser";
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