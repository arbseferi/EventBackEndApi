using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackEndApi.Response
{
    public class ResponseMessage
    {
        public int statusCode { get; set; }
        public bool resFlag { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
}
