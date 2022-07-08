using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class PilotRideUpdate
    {
        public class PilotUpdateRequest
        {
            public int ID { get; set; }
            public int RequestID { get; set; }
            public int StatusID { get; set; }
            public string Status { get; set; }
            public string Location { get; set; }
            public int MissionType { get; set; }
            public string Comments { get; set; }
            public DateTime RequestOn { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string VideoLink { get; set; }
            public bool IsActive { get; set; }
            public string AddedBy { get; set; }
            public DateTime AddedOn { get; set; }
            public string UpdateBy { get; set; }
            public DateTime UpdateOn { get; set; }
            public bool PushNotification { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public bool LiveVideoStream { get; set; }
            public string MissionName { get; set; }
        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class PilotUpdateRespone
        {
            public string message { get; set; }
            public bool status { get; set; }
            public UserData userData { get; set; }
        }

        public class UserData
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
}
