using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace IdeaForge.Domain
{
    public class PilotRequest
    {
        public string Status { get; set; }
    }
    public class PilotRequestResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public List<RequestData> userData { get; set; }
    }

    public class RequestData
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
        public string contactNo { get; set; }
        public Brush color { get; set; }
        public Brush TextColor { get; set; }
        public string StatusImage { get; set; }
        public string userName { get; set; }
        public string originLatitude { get; set; }
        public string originLongitude { get; set; }
        public string destionLatitude { get; set; }
        public string destionLongitude { get; set; }
        public Visibility IsVisibleButton { get; set; }
        public Visibility ViewButtonVisible { get; set; }
        public string city { get; set; }
        public int TotalAcceptedRidesByUser { get; set; }
        public int TotalRejectedRidesByUser { get; set; }
        public int TotalServiceByUser { get; set; }
        public int TotalRequestedByUser { get; set; }
    }
}
