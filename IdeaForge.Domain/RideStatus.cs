using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{

    public class RideStatusResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public List<RideStatus> userData { get; set; }
    }
    public class RideStatus
    {
        public int statusId { get; set; }
        public string name { get; set; }
    }
}
