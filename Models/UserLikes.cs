using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class UserLikes
    {
        private string useremail;
        private string apartment_id;
        public UserLikes()
        {

        }
        public string Useremail { get => useremail; set => useremail = value; }
        public string Apartment_id { get => apartment_id; set => apartment_id = value; }
    }
}