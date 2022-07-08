using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RideUpdateRequest
    {
        public int id { get; set; }
        public int requestID { get; set; }
        public int statusID { get; set; }
        public string status { get; set; }
        public string location { get; set; }
        public int missionType { get; set; }
        public string comments { get; set; }
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


    public class RideUpdateResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public RideUpdate userData { get; set; }
    }

    public class RideUpdate
    {
        public int id { get; set; }
        public int requestID { get; set; }
        public int statusID { get; set; }
        public string status { get; set; }
        public string location { get; set; }
        public int missionType { get; set; }
        public string comments { get; set; }
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
