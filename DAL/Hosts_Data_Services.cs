using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Final_Project_CGROUP2.DAL
{
    public class Hosts_Data_Services
    {

        
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