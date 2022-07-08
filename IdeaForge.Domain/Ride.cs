using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class Ride
    {
        public string Status { get; set; }

    }
    public class RideResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public RideById userData { get; set; }
    }

    public class RideById
    {

        public int id { get; set; }
        public int requestID { get; set; }
        public int statusID { get; set; }
        public string status { get; set; }
        public string location { get; set; }
        public int missionType { get; set; }
        public object comments { get; set; }
        public DateTime requestOn { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string videoLink { get; set; }
        public bool isActive { get; set; }
        public string addedBy { get; set; }
        public DateTime addedOn { get; set; }
        public string updateBy { get; set; }
        public DateTime updateOn { get; set; }
        public bool pushNotification { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public bool liveVideoStream { get; set; }
        public string missionName { get; set; }


    }

}
