using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ideaForge.Models
{
    public class ReportBySelectedUserToExcel
    {
        public int missionType { get; set; }
        public int totalrequestedTime { get; set; }
        public DateTime requestOn { get; set; }
        public DateTime flightTime { get; set; }
        public bool pushNotification { get; set; }
        public bool liveVideoStream { get; set; }
        public string statusForUser { get; set; }
        public string currentFlightStatus { get; set; }
        public string originLatitude { get; set; }
        public string originLongitude { get; set; }
        public string ControlKey { get; set; }
        public string SecretKey { get; set; }
        public string UAVID { get; set; }
        public int userRating { get; set; }
        public string userString { get; set; }
        public int pilotRating { get; set; }
        public string pilotFeedback { get; set; }
    }
}
