using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_Project_CGROUP2.DAL;
using Final_Project_CGROUP2.Models;

namespace Final_Project_CGROUP2.Controllers
{
    public class ReviewsController : ApiController
    {
        [HttpGet]
        [Route("api/Reviews/last")]
        // GET api/<controller>
        public string Get()
        {
            Reviews_Data_Services reviews_Data_Services = new Reviews_Data_Services();
            return reviews_Data_Services.getLastReviewId();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route ("api/Reviews/{apartmentId}")] 
        public IEnumerable<Review> Get(string apartmentId)
        {
            Reviews_Data_Services reviews_Data_Services = new Reviews_Data_Services();
            return reviews_Data_Services.GetReviews(apartmentId);
        }

        // POST api/<controller>
        public int Post(Review review)
        {
            Reviews_Data_Services RDS = new Reviews_Data_Services();
            return RDS.addReview(review);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}