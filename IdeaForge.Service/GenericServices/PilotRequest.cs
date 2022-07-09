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
        public async Task<bool> GetStatusChangesResponse(bool isAccepted, int rideId)
        {
            try
            {
                var url = UrlHelper.StatusChangesURL + "/" + isAccepted + "/" + rideId;
              
                var resultString = await HTTPClientWrapper<StatusChanges>.PostRequest(url, null);
                var result = JsonConvert.DeserializeObject<bool>(resultString);
                if(result==true)
                {
                    return true;
                }

                return false;

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
       
        async Task<RideResponse> IPilotRequestServices.GetRideById(int rideId)
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
        public async Task<List<RideStatus>> GetAllStatuses()
        {
            try
            {
                var url = UrlHelper.getAllStatuses;

                var resultString = await HTTPClientWrapper<List<RideStatus>>.Get(url);
                return resultString;

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
    }
}
