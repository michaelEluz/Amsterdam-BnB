using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Models
{
    public class Review
    {
        private string apartmentId;
        private string reviewId;
        private DateTime reviewDate;
        private string reviewer_Id;
        private string reviewer_Name;
        private string content;

        public Review() {
            this.ReviewDate = DateTime.Now;
        }

        public string ApartmentId { get => apartmentId; set => apartmentId = value; }
        public string ReviewId { get => reviewId; set => reviewId = value; }
        public DateTime ReviewDate { get => reviewDate; set => reviewDate = value; }
        public string Reviewer_Id { get => reviewer_Id; set => reviewer_Id = value; }
        public string Reviewer_Name { get => reviewer_Name; set => reviewer_Name = value; }
        public string Content { get => content; set => content = value; }
    }
}