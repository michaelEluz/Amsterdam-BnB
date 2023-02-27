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
    public class Reviews_Data_Services
    {
        public int addReview(Review review)
        {
            SqlConnection con = Connect();
            SqlCommand command = createInsertReviewCommand(con,review);
            int numAffected= command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        private SqlCommand createInsertReviewCommand(SqlConnection con, Review review)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@ApartmentId", review.ApartmentId);
            command.Parameters.AddWithValue("@ReviewId", review.ReviewId);
            command.Parameters.AddWithValue("@ReviewDate", review.ReviewDate);
            command.Parameters.AddWithValue("@Reviewer_Id","ChillaxUser");
            command.Parameters.AddWithValue("@Reviewer_Name", review.Reviewer_Name);
            command.Parameters.AddWithValue("@Content", review.Content);
            command.CommandText = "spFPAddReview";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        public string getLastReviewId()
        {
            SqlConnection con = Connect();
            //SqlCommand command = createReadLastReviewIdCommand(con);
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT top 1 id FROM FP_Review ORDER BY id DESC";
            command.Connection = con;
            command.CommandTimeout = 10;
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            string id = "NO ID";
            while (dr.Read())
            {
                id = dr["id"].ToString();
            }
            con.Close();
            return id;
        }

        public IEnumerable<Review> GetReviews(string apartmentId)
        {
            SqlConnection con = Connect();
            SqlCommand command = createReadReviewsCommand(con, apartmentId);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Review> reviews = new List<Review>();
            while (dr.Read())
            {
                Review review = new Review();
                review.ReviewId = dr["id"].ToString();
                review.Reviewer_Name = dr["reviewer_name"].ToString();
                review.ReviewDate = (DateTime)dr["date"];
                review.Content = dr["comments"].ToString();
                reviews.Add(review);
            }
            con.Close();
            return reviews;
        }

        private SqlCommand createReadReviewsCommand(SqlConnection con, string apartmentId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@apartmentId", apartmentId);
            command.CommandText = "spFPGetReviews";
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