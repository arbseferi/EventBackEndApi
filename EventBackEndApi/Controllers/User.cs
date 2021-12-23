using EventBackEndApi.Db;
using EventBackEndApi.Models;
using EventBackEndApi.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        // GET: api/<User>
        [HttpGet]
        public ResponseMessage Get()
        {
           ResponseMessage ld = new();
            try
            {
                ld.statusCode = 200;
                ld.resFlag = true;
                ld.msg = "List of users!";
                ld.data = _context.Users.ToList();
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

        // GET api/<User>/5
        [HttpGet("{id}")]
        public ResponseMessage Get(int id)
        {
            ResponseMessage ld = new();
            try
            {
                ld.statusCode = 200;
                ld.resFlag = true;
                ld.msg = "List of users!";
                ld.data = _context.Users.Where(user => user.Id == id).FirstOrDefault();
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

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
