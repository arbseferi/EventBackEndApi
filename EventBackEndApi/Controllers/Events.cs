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
    public class Events : ControllerBase
    {
        private readonly AppDbContext _context;
        public Events(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<Events>
        [HttpGet]
        public ResponseMessage Get([FromHeader] string SessionGuid)
        {
            ResponseMessage ld = new();
            if (SessionGuid == null)
            {
                ld.statusCode = 500;
                ld.resFlag = false;
                ld.msg = "No session found in header!";
                return ld;
            }
            else
            {
                Sessions s = _context.Sessions.Where(x => (x.SessionGuid == SessionGuid)).FirstOrDefault();
                if(s != null)
                {
                    Users u = _context.Users.Where(x => x.Id == s.UserId).FirstOrDefault();
                    try
                    {
                        ld.statusCode = 200;
                        ld.resFlag = true;
                        ld.msg = "List of events!";
                        ld.data = _context.Events.Where(x => x.UserId == u.Id);
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
                    ld.statusCode = 500;
                    ld.resFlag = false;
                    ld.msg = "No session found with this guid!";
                    return ld;
                }
                
            }
        }

        // GET api/<Events>/5
        [HttpGet("{id}")]
        public ResponseMessage Get(int id, [FromHeader] string SessionGuid)
        {
            ResponseMessage ld = new();
            if (SessionGuid == null)
            {
                ld.statusCode = 500;
                ld.resFlag = false;
                ld.msg = "No session found in header!";
                return ld;
            }
            else
            {
                Sessions s = _context.Sessions.Where(x => (x.SessionGuid == SessionGuid)).FirstOrDefault();
                if (s != null)
                {
                    Users u = _context.Users.Where(x => x.Id == s.UserId).FirstOrDefault();
                    try
                    {
                        ld.statusCode = 200;
                        ld.resFlag = true;
                        ld.msg = "List of events!";
                        ld.data = _context.Events.Where(x => x.Id == id && x.UserId == u.Id).FirstOrDefault();
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
                else
                {
                    ld.statusCode = 500;
                    ld.resFlag = false;
                    ld.msg = "No session found with this guid!";
                    return ld;
                }

            }
        }

        // POST api/<Events>
        [HttpPost]
        public ResponseMessage Post([FromBody] EventsDto ev, [FromHeader] string SessionGuid)
        {
            ResponseMessage ld = new();
            if (SessionGuid == null)
            {
                ld.statusCode = 500;
                ld.resFlag = false;
                ld.msg = "No session found in header!";
                return ld;
            }
            else
            {
                Sessions s = _context.Sessions.Where(x => (x.SessionGuid == SessionGuid)).FirstOrDefault();
                if (s != null)
                {
                    Users u = _context.Users.Where(x => x.Id == s.UserId).FirstOrDefault();
                    try
                    {
                        EventBackEndApi.Models.Events e = new();
                        e.EventName = ev.EventName;
                        e.EventDescription = ev.EventDescription;
                        e.EventDate = ev.EventDate;
                        e.UserId = u.Id;
                        ld.statusCode = 200;
                        ld.resFlag = true;
                        ld.msg = "Event added successfully!";
                        _context.Add(e);
                        _context.SaveChanges();
                        ld.data = _context.Events.Where(x => x.UserId == u.Id);
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
                else
                {
                    ld.statusCode = 500;
                    ld.resFlag = false;
                    ld.msg = "No session found with this guid!";
                    return ld;
                }
            }
        }


        // PUT api/<Events>/5
        [HttpPut("{id}")]
        public ResponseMessage Put(int id, [FromBody] EventsDto ev, [FromHeader] string SessionGuid)
        {
            ResponseMessage ld = new();
            if (SessionGuid == null)
            {
                ld.statusCode = 500;
                ld.resFlag = false;
                ld.msg = "No session found in header!";
                return ld;
            }
            else
            {
                Sessions s = _context.Sessions.Where(x => (x.SessionGuid == SessionGuid)).FirstOrDefault();
                if (s != null)
                {
                    Users u = _context.Users.Where(x => x.Id == s.UserId).FirstOrDefault();
                    EventBackEndApi.Models.Events e = _context.Events.Where(x => x.Id == id).FirstOrDefault();
                    try
                    {
                        e.EventName = ev.EventName;
                        e.EventDescription = ev.EventDescription;
                        e.EventDate = ev.EventDate;
                        e.UserId = u.Id;
                        ld.statusCode = 200;
                        ld.resFlag = true;
                        ld.msg = "Event updated successfully!";
                        _context.Update(e);
                        _context.SaveChanges();
                        ld.data = _context.Events.Where(x => x.Id == id);
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
                else
                {
                    ld.statusCode = 500;
                    ld.resFlag = false;
                    ld.msg = "No session found with this guid!";
                    return ld;
                }
            }
        }

        // DELETE api/<Events>/5
        [HttpDelete("{id}")]
        public ResponseMessage Delete(int id, [FromHeader] string SessionGuid)
        {
            ResponseMessage ld = new();
            if (SessionGuid == null)
            {
                ld.statusCode = 500;
                ld.resFlag = false;
                ld.msg = "No session found in header!";
                return ld;
            }
            else
            {
                Sessions s = _context.Sessions.Where(x => (x.SessionGuid == SessionGuid)).FirstOrDefault();
                if (s != null)
                {
                    Users u = _context.Users.Where(x => x.Id == s.UserId).FirstOrDefault();
                    EventBackEndApi.Models.Events e = _context.Events.Where(x => x.Id == id && x.UserId == u.Id).FirstOrDefault();
                    try
                    {
                        ld.statusCode = 200;
                        ld.resFlag = true;
                        ld.msg = "Event deleted successfully!";
                        _context.Remove(e);
                        _context.SaveChanges();
                        ld.data = _context.Events.Where(x => x.UserId == u.Id);
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
                else
                {
                    ld.statusCode = 500;
                    ld.resFlag = false;
                    ld.msg = "No session found with this guid!";
                    return ld;
                }
            }
        }
    }
}
