

using Final_Project_CGROUP2.DAL;
using Final_Project_CGROUP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class User
    {

        private string email;
        private string userName;
        private string password;
        private int total_income;
        private int total_reservations;
        private int total_cancellations;
        private DateTime sign_up_date;
        private Dictionary<string,int> userlikes;
        public User(string email,string userName, string password) {
            this.email=email;
            this.userName=userName;
            this.password = password;
            this.sign_up_date = DateTime.Now.Date;
            this.userlikes = new Dictionary<string, int>();

        }
        public User() {
            this.sign_up_date = DateTime.Now.Date;
            this.userlikes = new Dictionary<string, int>();

        }

        public string Email { get => email; set => email = value; }
        public string UserName { get => userName; set => userName = value; }

        public string Password { get => password; set => password = value; }
        public int Total_income { get => total_income; set => total_income = value; }
        public int Total_reservations { get => total_reservations; set => total_reservations = value; }
        public int Total_cancellations { get => total_cancellations; set => total_cancellations = value; }
        public DateTime Sign_up_date { get => sign_up_date; set => sign_up_date = value; }
        public Dictionary<string, int> Userlikes { get => userlikes; set => userlikes = value; }

        public int add_user()
        {
            Users_Data_Services ds = new Users_Data_Services();
            int status=ds.add_user(this);
            return status;
        }
    }
}