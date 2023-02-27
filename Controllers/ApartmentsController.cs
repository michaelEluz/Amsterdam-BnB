using Final_Project_CGROUP2.DAL;
using Final_Project_CGROUP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_Project_CGROUP2.Controllers
{
    public class ApartmentsController : ApiController
    {
        // GET api/<controller>
        //-------------------------------------show apartment on map quary
        public IEnumerable<Apartment> Get()
        {
            Apartments_Data_Services ds = new Apartments_Data_Services();
            return ds.get_apartments();
        }

        [HttpGet]
        [Route("api/Apartments/admin")]
        public IEnumerable<Apartment> GetAdmin()
        {
            Apartments_Data_Services ds = new Apartments_Data_Services();
            return ds.get_admin_apartments();
        }

        [HttpGet]
        [Route("api/Apartments/{id}")]
        // GET api/<controller>/5
        public Apartment Get(int id)
        {
            Apartments_Data_Services ds = new Apartments_Data_Services();
            return ds.get_ById(id);

        }

        
        //---------------------------search apartments to apartments.html quary
        [HttpGet]
        [Route("api/Apartments/{checkIn}/{checkOut}/{rooms}/{gust}/{skip}/{fetch}")]
        public IEnumerable<Apartment> Get(string checkIn, string checkOut,int rooms,int gust,int skip,int fetch)
        {
            Apartments_Data_Services ds = new Apartments_Data_Services();
            return ds.get_FirstRenferApartment(checkIn, checkOut, rooms, gust,skip,fetch);
        }

        [HttpGet]
        [Route("api/Apartments/apartmentSlider")]
        public IEnumerable<Apartment> GetSlider()
        {
            Apartments_Data_Services ds = new Apartments_Data_Services();
            return ds.get_ApartmentSlider();
        }


        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

      
        [HttpPut]
        [Route("api/Apartments/{ap_id}/{userEmail}/Like")]
        public string Put(string ap_id,string userEmail)
        {
            Apartments_Data_Services apartments_Data_Services = new Apartments_Data_Services();
            return apartments_Data_Services.updateLike(ap_id,userEmail);
        }

        [HttpPut]
        [Route("api/Apartments/{ap_id}/{userEmail}/UnLike")]
        public string PutUnlike(string ap_id, string userEmail)
        {
            Apartments_Data_Services apartments_Data_Services = new Apartments_Data_Services();
            return apartments_Data_Services.updateUnLike(ap_id, userEmail);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}