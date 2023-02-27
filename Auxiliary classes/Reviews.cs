using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_CGROUP2.Auxiliary_classes
{
    public class Reviews
    {
        private double review_scores_rating = 0;
        private double review_scores_cleanliness = 0;
        private double review_scores_checkin = 0;
        private double review_scores_communication = 0;

        public Reviews() { }

        public double Review_scores_rating { get => review_scores_rating; set => review_scores_rating = value; }
        public double Review_scores_cleanliness { get => review_scores_cleanliness; set => review_scores_cleanliness = value; }
        public double Review_scores_checkin { get => review_scores_checkin; set => review_scores_checkin = value; }
        public double Review_scores_communication { get => review_scores_communication; set => review_scores_communication = value; }
    }
}