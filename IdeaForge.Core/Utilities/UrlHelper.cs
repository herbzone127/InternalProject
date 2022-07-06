using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Core.Utilities
{
    public class UrlHelper
    {
        public const string baseURL = "http://ifapi.dweb.in";
        public const string loginURL = baseURL + "/api/login/pilotlogin";
        public const string getCityList = baseURL + "/api/login/getcitylist";
        public const string registerURL = baseURL + "/api/login/pilotsignup";
        public const string pilotOTPURl = baseURL + "/api/login/validatepilotOTP";
        public const string pilotProfileURl = baseURL + "/api/Pilot/getprofile";
        public const string pilotSaveProfileURl = baseURL + "/api/Pilot/saveprofile";
        public const string pilotRequestURL = baseURL + "/api/pilotRequests/getallridesByCurrentDate";
        public const string pilotAllRequestURL = baseURL + "/api/pilotRequests/getallrides";
        public const string RidesByIdURL = baseURL + "/api/pilotRequests/getridebyid/{rideId}";
        public const string StatusChangesURL = baseURL + "/api/pilotRequests/userupdateride/{isAccepted}/{rideId}";



        public const string pilotLocationUrl = baseURL + "/api/pilotRequests/getpilotloactionbyuserid/{userId}";
    }
}
