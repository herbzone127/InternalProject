using IdeaForge.Core.Utilities;
using IdeaForge.Data;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IdeaForge.Domain.RideByPilot;

namespace IdeaForge.Service.GenericServices
{
    public class PilotRequest : IPilotRequestServices
    {
        public async Task<bool> GetStatusChangesResponse(bool isAccepted, int rideId,int userId)
        {
            try
            {
                var url = UrlHelper.StatusChangesURL + "/" + isAccepted + "/" + rideId+"/"+userId;
              
                var resultString = await HTTPClientWrapper<StatusChanges>.PostRequest(url, null);
                if (!resultString.ToLower().Contains("status"))
                {
                    var result = JsonConvert.DeserializeObject<bool>(resultString);
                    if (result == true)
                    {
                        return true;
                    }

                    return false;

                }
              

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
        public async Task<PilotRequestResponse> GetTodaysRequest(string status)
        {
            try
            {
                var url = UrlHelper.pilotRequestURL+"/"+status;
              
                var resultString = await HTTPClientWrapper<PilotRequestResponse>.Get(url);
           
                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<PilotRequestResponse> GetAllRequest(string status)
        {
            try
            {
                var url = UrlHelper.pilotAllRequestURL + "/" + status;

                var resultString = await HTTPClientWrapper<PilotRequestResponse>.Get(url);
                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<RideResponse> GetRideById(int rideId)
        {
            try
            {
                var url = UrlHelper.RidesByIdURL + "/" + rideId;

                var result = await HTTPClientWrapper<RideResponse>.Get(url);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<RideStatusResponse> GetAllStatuses()
        {
            try
            {
                var url = UrlHelper.getAllStatuses;

                var resultString = await HTTPClientWrapper<RideStatusResponse>.Get(url);
                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<PilotLocationResponse> GetPilotLocations(int userId)
        {
            try
            {
                var url = UrlHelper.getpilotloaction + "/"+userId;

                var resultString = await HTTPClientWrapper<PilotLocationResponse>.Get(url);
                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<ReasonsResponse> GetReasons()
        {
            try
            {
                var url = UrlHelper.getReasons ;

                var resultString = await HTTPClientWrapper<ReasonsResponse>.Get(url);
                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<PilotLocationResponse> AddUpdatePilotLocations(PilotLocation location)
        {
            try
            {
                var url = UrlHelper.updateloactionbyid;

                var resultString = await HTTPClientWrapper<PilotLocation>.PostRequest(url,location);
                var resultData = JsonConvert.DeserializeObject<PilotLocationResponse>(resultString);
                return resultData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<RideByPilotResponse> UpdateRideByPilot(Ride pilot)
        {
            try
            {
                var url = UrlHelper.ridebyPilot;

                var resultString = await HTTPClientWrapper<Ride>.PostRequest(url, pilot);
                var result = JsonConvert.DeserializeObject<RideByPilotResponse>(resultString);
               
                    return result;
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
        public async Task<FlightStatusResponse> AddUpdatePilotStatus(FlightStatus pilot)
        {
            try
            {
                var url = UrlHelper.addUpdatePilotStatus;
                var resultString = await HTTPClientWrapper<FlightStatus>.PostRequest(url, pilot);
                var result = JsonConvert.DeserializeObject<FlightStatusResponse>(resultString);

                return result;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
        public async Task<FlightFeedbackResponse> AddUpdatePilotFeedback(FlightFeedback pilot)
        {
            try
            {
                var url = UrlHelper.addUpdatePilotStatus;
                var resultString = await HTTPClientWrapper<FlightFeedback>.PostRequest(url, pilot);
                var result = JsonConvert.DeserializeObject<FlightFeedbackResponse>(resultString);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
        public async Task<adminLocationResponse> GetAdminLocations()
        {
            try
            {
                var url = UrlHelper.adminLocationUrl;
                dynamic a = null;
                var resultString = await HTTPClientWrapper<adminLocationResponse>.PostRequest(url, a);
                var result = JsonConvert.DeserializeObject<adminLocationResponse>(resultString);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<String> AddAdminLocations(addminPilotLocation adminPilot)
        {
            try
            {
                var url = UrlHelper.adminUpdateLocationUrl;
                var resultString = await HTTPClientWrapper<addminPilotLocation>.PostRequest(url, adminPilot);
                //var result = JsonConvert.DeserializeObject<adminLocationResponse>(resultString);
                return "true";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return "";
        }
        public async Task<UserFeedbackResponse> GetUserFeedbackByRideId(int rideId)
        {
            try
            {
                var url = UrlHelper.UserFeedbackByRideIdURL + "/" + rideId;

                var result = await HTTPClientWrapper<UserFeedbackResponse>.Get(url);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<FlightFeedbackResponse> GetFlightFeedbackByRideId(int rideId)
        {
            try
            {
                var url = UrlHelper.PilotFeedbackByRideIdURL + "/" + rideId;
                var result = await HTTPClientWrapper<FlightFeedbackResponse>.Get(url);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
    }
}
