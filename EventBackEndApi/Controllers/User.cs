using EventBackEndApi.Db;
using EventBackEndApi.Models;
using EventBackEndApi.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventBackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        private readonly AppDbContext _context;
        public User(AppDbContext context)
        {
            _context = context;
        }
        // POST api/<User>
        [HttpPost]
        public ResponseMessage Post([FromBody] Users user)
        {
            ResponseMessage ld = new();
            try
            {
                ld.statusCode = 200;
                ld.resFlag = true;
                ld.msg = "List of users!";
                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
                user.Password = encodedStr;
                _context.Users.Add(user);
                _context.SaveChanges();
                ld.data = user;
                return ld;
            }
            catch (Exception ex)
            {
                ld.statusCode = 500;
                ld.resFlag = false;
                ld.msg = ex.Message;
                return ld;
            }
        }
    }
}
