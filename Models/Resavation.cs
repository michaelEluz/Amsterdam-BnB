using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class Resavation
    {
        private string resavation_id;
        private string user_email;
        private DateTime orderDate;
        private DateTime checkIn;
        private DateTime checkOut;
        private string hostId;
        private int apartmentId;
        private int totalPrice;
        private bool is_cancel;
      
        public Resavation()
        {
            this.OrderDate = DateTime.Now;

        }

        public string Resavation_id { get => resavation_id; set => resavation_id = value; }
        public int ApartmentId { get => apartmentId; set => apartmentId = value; }
        public string User_email { get => user_email; set => user_email = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public DateTime CheckIn { get => checkIn; set => checkIn = value; }
        public DateTime CheckOut { get => checkOut; set => checkOut = value; }
        public bool Is_cancel { get => is_cancel; set => is_cancel = value; }
        public string HostId { get => hostId; set => hostId = value; }
        public int TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}