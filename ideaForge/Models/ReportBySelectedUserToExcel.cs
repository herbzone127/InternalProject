using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ideaForge.Models
{
    public class ReportBySelectedUserToExcel
    {
        public string missionType { get; set; }
        public double totalrequestedTime { get; set; }
        public string flightDtae { get; set; }
        public string flightTime { get; set; }
        public string pushNotification { get; set; }
        public string liveVideoStream { get; set; }
        public string statusForUser { get; set; }
        public string currentFlightStatus { get; set; }
        public string originLatitude { get; set; }
        public string originLongitude { get; set; }
        public string ControlKey { get; set; }
        public string SecretKey { get; set; }
        public string UAVID { get; set; }
        public int userRating { get; set; }
        public string userFeedback { get; set; }
        public int pilotRating { get; set; }
        public string pilotFeedback { get; set; }
    }
}
