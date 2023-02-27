using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class BestHost
    {
        private string host_url;
        private double host_id;
        private string host_picture_url;
        private string host_name;

        public BestHost() { }
        public string Host_url { get => host_url; set => host_url = value; }
        public double Host_id { get => host_id; set => host_id = value; }
        public string Host_picture_url { get => host_picture_url; set => host_picture_url = value; }
        public string Host_name { get => host_name; set => host_name = value; }
    }
}