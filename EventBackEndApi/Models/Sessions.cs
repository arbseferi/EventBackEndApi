using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackEndApi.Models
{
    public class Sessions
    {
        public int Id { get; set; }
        public string SessionGuid { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
