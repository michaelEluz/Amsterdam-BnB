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
    public class ResarvationsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string id)
        {

            return "succes 200";
        }

        [HttpGet]
        [Route("api/Resarvations/{Id}")]
        public IEnumerable<Resavation> GetResarvation(int Id)
        {
            Reservation_Data_Services reservation_Data_Services = new Reservation_Data_Services();
            return reservation_Data_Services.get_resarvation_list(Id);

        }



        // POST api/<controller>
        public string Post(Resavation reservation)
        {
            Reservation_Data_Services RDS = new Reservation_Data_Services();
            return RDS.addReservation(reservation);
        }
        
        [HttpPut]
        [Route("api/Resarvations/{id}/{totalNights}/{totalPrice}/{apartment_id}/{host_id}/{userEmail}/cancel")]
        //PUT api/<controller>/5
        public string Put( string id,int totalNights,int totalPrice,string apartment_id,string host_id,string userEmail)
        {
            Reservation_Data_Services RDS = new Reservation_Data_Services();
            return RDS.cancelResarvation(id, totalNights, totalPrice, host_id, apartment_id, userEmail);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}