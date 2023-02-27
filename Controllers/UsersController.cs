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
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<User> Get()
        {
            Users_Data_Services ds = new Users_Data_Services();
            return ds.GetAdminUsers();

        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "";
        }
        [HttpGet]
        [Route("api/Users/chat")]

        public IEnumerable<User> GetAllUser_chat()
        {
            Users_Data_Services ds = new Users_Data_Services();
            return ds.getAllUser_Chat();
        }
        [HttpGet]
        [Route("api/Users/{email}/email")]

        public User GetUser(string email)
        {
            Users_Data_Services ds = new Users_Data_Services();
            return ds.getUser(email);
        }
        [HttpGet]
        [Route("api/Users/{email}/favorites")]

        public List<Apartment> GetUserFavorites(string email)
        {
            Users_Data_Services ds = new Users_Data_Services();
            return ds.getUserFavorites(email);
        }


        [HttpGet]
        [Route("api/Users/{email}/GetLikes")]

        public Dictionary<string,int> GetUserLikes(string email)
        {
            Users_Data_Services ds = new Users_Data_Services();
            return ds.getUserLikes(email);
        }
        [HttpGet]
        [Route("api/Users/{email}/email_2")]

        public IEnumerable<Apartment> GetUserApartments(string email)
        {
            Users_Data_Services ds = new Users_Data_Services();
            return ds.get_all_usersApartments(email);
        }



        // POST api/<controller>
        public int Post(User user)
        {
            int status = user.add_user();
            return status;
        }
       
       
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}