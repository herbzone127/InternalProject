﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Core.Utilities
{
    public class UrlHelper
    {
//#if DEBUG
//        public const string baseURL = "https://localhost:44340";
//#else
public const string baseURL = "http://ifapi.dweb.in";
//#endif
        public const string loginURL = baseURL + "/api/login/pilotlogin";
        public const string getCityList = baseURL + "/api/login/getcitylist";
        public const string registerURL = baseURL + "/api/login/pilotsignup";
        public const string pilotOTPURl = baseURL + "/api/login/validatepilotOTP";
        public const string pilotProfileURl = baseURL + "/api/Pilot/getprofile";
        public const string pilotSaveProfileURl = baseURL + "/api/Pilot/saveprofile";
        public const string pilotRequestURL = baseURL + "/api/pilotRequests/getallridesByCurrentDate";
        public const string pilotAllRequestURL = baseURL + "/api/pilotRequests/getallrides";
        public const string RidesByIdURL = baseURL + "/api/pilotRequests/getridebyid";
        public const string StatusChangesURL = baseURL + "/api/pilotRequests/userupdateride";
        public const string pilotLocationUrl = baseURL + "/api/pilotRequests/getpilotlocation";
        public const string ridebyPilot = baseURL + "/api/pilotRequests/updateRideByPilot";
        public const string getAllStatuses = baseURL + "/api/pilotRequests/getAllStatus";
        public const string addUpdatePilotStatus = baseURL + "/api/pilotRequests/addUpdatePilotStatus";
        public const string getpilotloaction = baseURL + "/api/IFDock/getpilotlocation";
        public const string getReasons = baseURL + "/api/IFDock/getReasons";
        public const string updateloactionbyid = baseURL+ "/api/IFDock/AddUpdatePilotLocation";
    }
}
