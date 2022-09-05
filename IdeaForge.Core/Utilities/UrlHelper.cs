using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Core.Utilities
{
    public class UrlHelper
    {
#if DEBUG
        public const string baseURL = "https://localhost:7121";
#else
//public const string baseURL = "http://ifapi.dweb.in";
#endif
        public const string loginURL = baseURL + "/api/login/pilotlogin";
        public const string getCityList = baseURL + "/api/login/getcitylist";
        public const string registerURL = baseURL + "/api/login/pilotsignup";
        public const string pilotOTPURl = baseURL + "/api/login/validatepilotOTP";
        public const string pilotProfileURl = baseURL + "/api/Pilot/getprofile";
        public const string adminProfileURl = baseURL + "/api/Admins/getprofile";
        public const string pilotSaveProfileURl = baseURL + "/api/Pilot/saveprofile";
        public const string adminSaveProfileURl = baseURL + "/api/Admins/saveprofile";
        public const string pilotRequestURL = baseURL + "/api/pilotRequests/getallridesByCurrentDate";
        public const string pilotAllRequestURL = baseURL + "/api/pilotRequests/getallrides";
        public const string adminAllRequestURL = baseURL + "/api/AdminRequest/getallrides";
        public const string RidesByIdURL = baseURL + "/api/pilotRequests/getridebyid";
        public const string StatusChangesURL = baseURL + "/api/pilotRequests/userupdateride";
        public const string pilotLocationUrl = baseURL + "/api/pilotRequests/getpilotlocation";
        public const string ridebyPilot = baseURL + "/api/pilotRequests/updateRideByPilot";
        public const string getAllStatuses = baseURL + "/api/pilotRequests/getAllStatus";
        public const string addUpdatePilotStatus = baseURL + "/api/pilotRequests/addUpdatePilotStatus";
        public const string getpilotloaction = baseURL + "/api/IFDock/getpilotlocation";
        public const string getReasons = baseURL + "/api/IFDock/getReasons";
        public const string updateloactionbyid = baseURL+ "/api/IFDock/AddUpdatePilotLocation";
        public const string adminLoginUrl = baseURL + "/api/login/adminlogin";
        public const string adminOTPUrl = baseURL + "/api/login/validateadminOTP";
        public const string adminSingUpUrl = baseURL + "/api/login/adminsignup";
        public const string adminLocationUrl = baseURL + "/api/Admins/GetLocation";
        public const string adminUpdateLocationUrl = baseURL + "/api/Admins/AddLocation";
        public const string UserFeedbackByRideIdURL = baseURL + "/api/pilotRequests/UserFeedbackByRideId";
        public const string PilotFeedbackByRideIdURL = baseURL + "/api/pilotRequests/PilotFeedbackByRideId";
    }
}
