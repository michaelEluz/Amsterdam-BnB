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
    public class HostsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("api/Hosts/admin")]
        public IEnumerable<Host> GetAdminHosts()
        {
            Apartments_Data_Services ds = new Apartments_Data_Services();
            return ds.get_AdminHosts();
        }

        [HttpGet]
        [Route("api/Hosts/bestHosts")]
        public IEnumerable<BestHost> GetSlider()
        {
            Apartments_Data_Services ds = new Apartments_Data_Services();
            return ds.get_BestHosts();
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
       
       
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}