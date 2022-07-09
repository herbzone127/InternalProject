using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class FlightStatusResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public FlightStatus FlightStatus { get; set; }
    }
    public class FlightStatus
    {
        public int id { get; set; }
        public bool cameraError { get; set; }
        public bool technicalError { get; set; }
        public bool communicationLoss { get; set; }
        public bool badWeather { get; set; }
        public int rideId { get; set; }
        public int userId { get; set; }
    }
}
