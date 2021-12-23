using EventBackEndApi.Db;
using EventBackEndApi.Models;
using EventBackEndApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventBackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Auth : ControllerBase
    {
        private readonly AppDbContext _context;
        public Auth(AppDbContext context)
        {
            _context = context;
        }
        // POST api/<auth>
        [HttpPost]
        [Route("signin")]
        public ResponseMessage SignIn([FromBody] AuthDTO user)
        {
            ResponseMessage ld = new();
            Users u = new();
            u = _context.Users.Where(x => (x.Email == user.Username_Email && x.Password == Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password))) || (x.Username == user.Username_Email && x.Password == Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password)))).SingleOrDefault();
            if(u != null)
            {
                Guid g = Guid.NewGuid();
                Sessions session = new();
                session.SessionGuid = g.ToString();
                session.UserId = u.Id;
                _context.Add(session);
                _context.SaveChanges();
            }
            try
            {
                ld.statusCode = 200;
                ld.resFlag = true;
                ld.msg = "List of users!";
                ld.data = _context.Sessions.Where(x => x.UserId == u.Id).FirstOrDefault();
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
        [HttpPost]
        [Route("signout")]
        public ResponseMessage SignOut([FromHeader] string SessionGuid)
        {
            
            ResponseMessage ld = new();
            if (SessionGuid == null)
            {
                ld.statusCode = 500;
                ld.resFlag = false;
                ld.msg = "No Session Found";
                return ld;
            }
            else
            {
                Sessions s = _context.Sessions.Where(x => (x.SessionGuid == SessionGuid)).FirstOrDefault();
                if (s != null)
                {
                    try
                    {
                        ld.statusCode = 200;
                        ld.resFlag = true;
                        _context.Sessions.Remove(s);
                        _context.SaveChanges();
                        ld.msg = "Successfully logged out!";
                        return ld;
                    }
                    catch (Exception ex)
                    {
                        ld.statusCode = 500;
                        ld.resFlag = false;
                        ld.msg = ex.Message;
                        return ld;
                    }
                }else
                {
                    ld.statusCode = 401;
                    ld.resFlag = false;
                    ld.msg = "No session Found";
                    return ld;
                }
            }
        }
    }
}
