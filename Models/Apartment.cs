using Final_Project_CGROUP2.Auxiliary_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class Apartment
    {
        private string id;
        private Host host;
        private ApartmentCoordinates a_c;
        private string listing_url;
        private string name = "";
        private string description = "";
        private string picture_url = "";
        private string neighborhood;
        private string neighborhood_overview = "";
        private string neighborhood_cleansed= "";
        private string property_type = "";
        private string room_type = "";
        private int accomodates = 0;
        private string bathrooms_text;
        private int bedrooms;
        private int beds;
        private string amenities = "";
        private string price = "";
        private string minimum_nights = "";
        private string maximum_nights = "";
        private bool has_avaolability;
        private int number_of_reviews;
        private DateTime last_review ;
        private bool instant_bookable;
        private int total_cacellations;
        private int total_rented_days;
        private Reviews reviews;
        private List<Resavation> resavations_list;
        public Apartment() { }

        public string Id { get => id; set => id = value; }
        //public string Host_id { get => host_id; set => host_id = value; }
        public ApartmentCoordinates A_c { get => a_c; set => a_c = value; }
        public string Listing_url { get => listing_url; set => listing_url = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Picture_url { get => picture_url; set => picture_url = value; }
        public string Neighborhood { get => neighborhood; set => neighborhood = value; }
        public string Neighborhood_overview { get => neighborhood_overview; set => neighborhood_overview = value; }
        public string Neighborhood_cleansed { get => neighborhood_cleansed; set => neighborhood_cleansed = value; }
        public string Property_type { get => property_type; set => property_type = value; }
        public string Room_type { get => room_type; set => room_type = value; }
        public int Accomodates { get => accomodates; set => accomodates = value; }
        public string Bathrooms_text { get => bathrooms_text; set => bathrooms_text = value; }
        public int Bedrooms { get => bedrooms; set => bedrooms = value; }
        public int Beds { get => beds; set => beds = value; }
        public string Amenities { get => amenities; set => amenities = value; }
        public string Price { get => price; set => price = value; }
        public string Minimum_nights { get => minimum_nights; set => minimum_nights = value; }
        public string Maximum_nights { get => maximum_nights; set => maximum_nights = value; }
        public bool Has_avaolability { get => has_avaolability; set => has_avaolability = value; }
        public int Number_of_reviews { get => number_of_reviews; set => number_of_reviews = value; }
        public DateTime Last_review { get => last_review; set => last_review = value; }
        public bool Instant_bookable { get => instant_bookable; set => instant_bookable = value; }
        public int Total_cacellations { get => total_cacellations; set => total_cacellations = value; }
        public int Total_rented_days { get => total_rented_days; set => total_rented_days = value; }
        public Reviews Reviews { get => reviews; set => reviews = value; }
        public Host Host { get => host; set => host = value; }
        public List<Resavation> Resavations_list { get => resavations_list; set => resavations_list = value; }
    }
}