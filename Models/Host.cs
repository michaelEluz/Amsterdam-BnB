using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class Host
    {
        private string host_Id;
        private string host_url;
        private string host_name;
        private DateTime host_since;
        private string host_loctaion;
        private string host_about;
        private string host_response_time;
        private string host_acceptance_rate;
        private bool host_is_superHost;
        private string host_thumbnail_url;
        private string host_picture_url;
        private string Host_neighbourhood;
        private int host_listings_count;
        private string Host_verifications;
        private int host_total_cancellations;
        private double host_income;

        public Host() { }

        public string Host_Id { get => host_Id; set => host_Id = value; }
        public string Host_url { get => host_url; set => host_url = value; }
        public string Host_name { get => host_name; set => host_name = value; }
        public DateTime Host_since { get => host_since; set => host_since = value; }
        public string Host_loctaion { get => host_loctaion; set => host_loctaion = value; }
        public string Host_about { get => host_about; set => host_about = value; }
        public string Host_response_time { get => host_response_time; set => host_response_time = value; }
        public string Host_acceptance_rate { get => host_acceptance_rate; set => host_acceptance_rate = value; }
        public bool Host_is_superHost { get => host_is_superHost; set => host_is_superHost = value; }
        public string Host_thumbnail_url { get => host_thumbnail_url; set => host_thumbnail_url = value; }
        public string Host_picture_url { get => host_picture_url; set => host_picture_url = value; }
        public string Host_neighbourhood1 { get => Host_neighbourhood; set => Host_neighbourhood = value; }
        public int Host_listings_count { get => host_listings_count; set => host_listings_count = value; }
        public string Host_verifications1 { get => Host_verifications; set => Host_verifications = value; }
        public int Host_total_cancellations { get => host_total_cancellations; set => host_total_cancellations = value; }
        public double Host_income { get => host_income; set => host_income = value; }
    }
}