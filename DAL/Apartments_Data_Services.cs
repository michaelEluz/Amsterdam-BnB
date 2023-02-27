using Final_Project_CGROUP2.Auxiliary_classes;
using Final_Project_CGROUP2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Final_Project_CGROUP2
{

    public class Apartments_Data_Services
    {
        public string updateUnLike(string ap_id, string userEmail)
        {
            SqlConnection con = Connect();
            SqlCommand command = creatUneLikeCommand(con, ap_id, userEmail);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            if (numAffected == 1)
            {
                return "succes 200";
            }
            return "error 404";

        }
        private SqlCommand creatUneLikeCommand(SqlConnection con, string ap_id, string userEmail)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", ap_id);
            command.Parameters.AddWithValue("@useremail", userEmail);
            command.CommandText = "spFP_ApartmentUpdateUnLike";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        public string updateLike(string ap_id,string userEmail)
        {
            SqlConnection con = Connect();
            SqlCommand command = createLikeCommand(con,ap_id,userEmail);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            if (numAffected == 1)
            {
                return "succes 200";
            }
            return "error 404";

        }
        private SqlCommand createLikeCommand(SqlConnection con,string ap_id,string userEmail)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", ap_id);
            command.Parameters.AddWithValue("@useremail", userEmail);
            command.CommandText = "spFP_ApartmentUpdateLike";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
             

        public IEnumerable<Apartment> get_FirstRenferApartment(string checkIn, string checkOut, int rooms, int gusts,int skip,int fetch)
        {
            SqlConnection con = Connect();
            SqlCommand command = create_FirstRenferApartment_command(con, checkIn, checkOut, rooms, gusts,skip,fetch);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> all_apartments = new List<Apartment>();
            while (dr.Read())
            {
                //Reviews reviews = new Reviews();
                Host host = new Host();
                ApartmentCoordinates a_c = new ApartmentCoordinates();
                a_c.Latitude = (double)dr["latitude"];
                a_c.Longitude = (double)dr["longitude"];
                Apartment ap = new Apartment();
                ap.Id = dr["id"].ToString();
                ap.Bedrooms = Convert.ToInt32(dr["bedrooms"]);
                ap.Price = dr["price"].ToString();
                try
                {
                    ap.Reviews = new Reviews();
                    ap.Reviews.Review_scores_rating = (double)dr["review_scores_rating"];
                }
                catch
                {
                    ap.Reviews.Review_scores_rating = 3;
                }
                ap.Picture_url = (string)dr["picture_url"];
                ap.Name = dr["name"].ToString();
                ap.Accomodates = Convert.ToInt32(dr["accommodates"]);
                ap.Room_type = dr["room_type"].ToString();
                //ap.Reviews = reviews;
                a_c.calcCenterDistance();
                ap.A_c = a_c;
                if (Convert.ToInt32(dr["host_is_superhost"]) == 1)
                {
                    host.Host_is_superHost =true ;
                }
                else
                {
                    host.Host_is_superHost = false;
                }
                
                host.Host_Id = dr["host_id"].ToString();
                ap.Host = host;
                all_apartments.Add(ap);
                
                
            }
            con.Close();
            return all_apartments;
        }


        public SqlCommand create_FirstRenferApartment_command
            (SqlConnection con, string checkIn, string checkOut, int rooms, int gusts,int skip,int fetch)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@Check_in", checkIn);
            command.Parameters.AddWithValue("@Check_out", checkOut);
            command.Parameters.AddWithValue("@bedrooms", rooms);
            command.Parameters.AddWithValue("@accommodates", gusts);
            command.Parameters.AddWithValue("@skip", skip);
            command.Parameters.AddWithValue("@fetch", fetch);

            command.CommandText = "SPGetIndexApartments";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        public IEnumerable<Host> get_AdminHosts()
        {
            SqlConnection con = Connect();
            SqlCommand command = create_AdminHosts_command(con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Host> hosts = new List<Host>();

            while (dr.Read())
            {
                Host host = new Host();
                host.Host_picture_url = (string)dr["host_picture_url"];
                host.Host_Id = (string)dr["host_id"];
                host.Host_name = (string)dr["host_name"];
                host.Host_total_cancellations = Convert.ToInt32(dr["Total_Cancellations"]);
                host.Host_listings_count = Convert.ToInt32(dr["host_listings_count"]);
                host.Host_since = (DateTime)dr["host_since"];

                hosts.Add(host);
            }
            con.Close();
            return hosts;
        }

        public SqlCommand create_AdminHosts_command(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SPGetAdminHosts";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        public IEnumerable<BestHost> get_BestHosts()
        {
            SqlConnection con = Connect();
            SqlCommand command = create_BestHosts_command(con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<BestHost> hosts = new List<BestHost>();

            while (dr.Read())
            {
                BestHost host = new BestHost();
                host.Host_picture_url = (string)dr["host_picture_url"];
                host.Host_url = (string)dr["host_url"];
                host.Host_id = (double)dr["host_id"];
                host.Host_name = (string)dr["host_name"];
                hosts.Add(host);
            }
            con.Close();
            return hosts;
        }

        public SqlCommand create_BestHosts_command(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SPGetBestosts";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }




        //-------------------------------------------------------show apartments on map quary

        public IEnumerable<Apartment> get_admin_apartments()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetAdminApartmentssCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> all_apartments = new List<Apartment>();
            while (dr.Read())
            {
                Apartment ap = new Apartment();
                ap.Total_cacellations = Convert.ToInt32(dr["Total_Cancellations"]);
                ap.Total_rented_days = Convert.ToInt32(dr["Total_rented_days"]);
                ap.Id = dr["id"].ToString();
                //ap.Price = dr["price"].ToString();
                ap.Name = dr["name"].ToString();
                all_apartments.Add(ap);
            }
            con.Close();
            return all_apartments;
        }
        public IEnumerable<Apartment> get_apartments()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetApartmentssCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> all_apartments = new List<Apartment>();
            while (dr.Read())
            {
                Apartment ap = new Apartment();
                ApartmentCoordinates a_c = new ApartmentCoordinates();
                a_c.Latitude = (double)dr["latitude"];
                a_c.Longitude = (double)dr["longitude"];

                ap.A_c = a_c;
                ap.Id = dr["id"].ToString();
                ap.Price = dr["price"].ToString();
                ap.Name = dr["name"].ToString();
                all_apartments.Add(ap);
            }
            con.Close();
            return all_apartments;
        }

        public SqlCommand CreateGetApartmentssCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "spFPgetApartments";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        public SqlCommand CreateGetAdminApartmentssCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "spFPgetAdminApartments";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        public Apartment get_ById(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetApartmentsIdCommand(con);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Apartment ap = new Apartment();
            ApartmentCoordinates a_c = new ApartmentCoordinates();
            Reviews rev = new Reviews();
            Host host = new Host();

            while (dr.Read())
            {

                if (dr["id"] != null)
                    ap.Id = dr["id"].ToString();

                if (dr["latitude"] != System.DBNull.Value)
                    a_c.Latitude = (double)dr["latitude"];

                if (dr["description"] != System.DBNull.Value)
                    ap.Description = (string)dr["description"];

                if (dr["picture_url"] != System.DBNull.Value)
                    ap.Picture_url = (string)dr["picture_url"];

                if (dr["neighborhood_overview"] != System.DBNull.Value)
                    ap.Neighborhood_overview = (string)dr["neighborhood_overview"];

                if (dr["host_url"] != System.DBNull.Value)
                    host.Host_url = (string)dr["host_url"];

                if (dr["host_name"] != System.DBNull.Value)
                    host.Host_name = (string)dr["host_name"];

                if (dr["host_about"] != System.DBNull.Value)
                    host.Host_about = (string)dr["host_about"];

                if (dr["host_picture_url"] != System.DBNull.Value)
                    host.Host_picture_url = (string)dr["host_picture_url"];

                if (dr["review_scores_rating"] != System.DBNull.Value)
                    rev.Review_scores_rating = (double)dr["Review_scores_rating"];

                if (dr["review_scores_cleanliness"] != System.DBNull.Value)
                    rev.Review_scores_cleanliness = (double)dr["review_scores_cleanliness"];

                if (dr["accommodates"] != System.DBNull.Value)
                    ap.Accomodates = Convert.ToInt32(dr["accommodates"]);

                if (dr["bedrooms"] != System.DBNull.Value)
                    ap.Bedrooms = Convert.ToInt32(dr["bedrooms"]);

                if (dr["price"] != System.DBNull.Value)
                    ap.Price = dr["price"].ToString();

                if (dr["longitude"] != System.DBNull.Value)
                    a_c.Longitude = (double)dr["longitude"];
                if (dr["name"] != System.DBNull.Value)
                    ap.Name = (string)dr["name"];
                host.Host_Id = dr["host_id"].ToString();
                ap.Amenities = dr["amenities"].ToString();
                a_c.calcCenterDistance();
                ap.A_c = a_c;
                ap.Reviews = rev;
                ap.Host = host;
            }
            con.Close();
            return ap;
        }
        public SqlCommand CreateGetApartmentsIdCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "spFPgetApartmentsById";
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

        public IEnumerable<Apartment> get_ApartmentSlider()
        {
            SqlConnection con = Connect();
            SqlCommand command = create_ApartmentSlider_command(con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> apartments = new List<Apartment>();

            while (dr.Read())
            {
                Apartment ap = new Apartment();
                Host host = new Host();
                Reviews reviews = new Reviews();
                if (dr["id"] != null)
                    ap.Id = dr["id"].ToString();

                // if (dr["latitude"] != System.DBNull.Value)
                // ap.Latitude = (double)dr["latitude"];

                if (dr["description"] != System.DBNull.Value)
                    ap.Description = (string)dr["description"];

                if (dr["picture_url"] != System.DBNull.Value)
                    ap.Picture_url = (string)dr["picture_url"];

                if (dr["neighborhood_overview"] != System.DBNull.Value)
                    ap.Neighborhood_overview = (string)dr["neighborhood_overview"];

                if (dr["host_url"] != System.DBNull.Value)
                    host.Host_url = (string)dr["host_url"];

                if (dr["host_name"] != System.DBNull.Value)
                    host.Host_name = (string)dr["host_name"];

                if (dr["host_about"] != System.DBNull.Value)
                    host.Host_about = (string)dr["host_about"];

                if (dr["host_picture_url"] != System.DBNull.Value)
                    host.Host_picture_url = (string)dr["host_picture_url"];

                if (dr["review_scores_rating"] != System.DBNull.Value)
                    reviews.Review_scores_rating = (double)dr["Review_scores_rating"];

                if (dr["review_scores_cleanliness"] != System.DBNull.Value)
                    reviews.Review_scores_cleanliness = (double)dr["review_scores_cleanliness"];

                //if (dr["bathrooms_text"] != System.DBNull.Value)
                //    ap.Bathrooms = (string)dr["bathrooms_text"];

                if (dr["bedrooms"] != System.DBNull.Value)
                    ap.Bedrooms = Convert.ToInt32(dr["bedrooms"]);

                //if (dr["price"] != System.DBNull.Value)
                //    ap.Price = (string)dr["price"];

                //if (dr["review_scores_rating"] != System.DBNull.Value)
                //    ap.Review_scores_rating = (double)dr["review_scores_rating"];
                //ap.calcCenterDistance();
                ap.Host = host;
                apartments.Add(ap);
            }
            con.Close();
            return apartments;
        }

        public SqlCommand create_ApartmentSlider_command(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SPGetApartmentSlider";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

    }
}