using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class ApartmentCoordinates
    {
        private const double centerLat = 52.379189;
        private const double centerLng = 4.899431;
        private double center_distance;
        private double latitude = 0;
        private double longitude = 0;



        public ApartmentCoordinates() { }




        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        public double Center_distance { get => center_distance; set => center_distance = value; }




        public void calcCenterDistance()
        {
            double R = 6371; // km
            double dLat = toRad(centerLat - Latitude);
            double dLon = toRad(centerLng - Longitude);
            double tempLat1 = toRad(Latitude);
            double tempLat2 = toRad(centerLat);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(tempLat1) * Math.Cos(tempLat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            Center_distance = d*1000;
        }

        // Converts numeric degrees to radians
        public double toRad(double Value)
        {
            return Value * Math.PI / 180;
        }


    }
}