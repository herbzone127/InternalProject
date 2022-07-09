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
        public async Task<RideByPilotResponse> UpdateRideByPilot(RideByPilot pilot)
        {
            try
            {
                var url = UrlHelper.ridebyPilot;

                var resultString = await HTTPClientWrapper<RideByPilot>.PostRequest(url, pilot);
                var result = JsonConvert.DeserializeObject<RideByPilotResponse>(resultString);
               
                    return result;
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
        public async Task<PilotStatus> AddUpdatePilotStatus(PilotStatus pilot)
        {
            try
            {
                var url = UrlHelper.addUpdatePilotStatus;
                var resultString = await HTTPClientWrapper<PilotStatus>.PostRequest(url, pilot);
                var result = JsonConvert.DeserializeObject<PilotStatus>(resultString);

                return result;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<PilotFeedback> AddUpdatePilotFeedback(PilotFeedback pilot)
        {
            try
            {
                var url = UrlHelper.addUpdatePilotStatus;
                var resultString = await HTTPClientWrapper<PilotFeedback>.PostRequest(url, pilot);
                var result = JsonConvert.DeserializeObject<PilotFeedback>(resultString);

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
