using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class CompleteRideModel
    {
        public string message { get; set; }
        public bool status { get; set; }
        public CompleteRideModelUserData userData { get; set; }
    }

    public class CompleteRideModelUserData
    {
        public int id { get; set; }
        public int requestID { get; set; }
        public int statusID { get; set; }
        public string status { get; set; }
        public string location { get; set; }
        public DateTime requestOn { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string videoLink { get; set; }
        public bool isActive { get; set; }
        public string addedBy { get; set; }
        public DateTime addedOn { get; set; }
        public string updateBy { get; set; }
        public DateTime updateOn { get; set; }
        public object contactNo { get; set; }
        public int feedbackID { get; set; }
        public object comments { get; set; }
        public int rating { get; set; }
        public int userID { get; set; }
        public int rideID { get; set; }

        public string flit_time { get; set; }
        public string Total_request_time { get; set; }
    }

    public class CompleteRideModelRideId
    {
        public int id { get; set; }
        public string token { get; set; }
    }

    public class stardynamic
    {
        public string ImageUser_Rating1 { get; set; }

    }
}
