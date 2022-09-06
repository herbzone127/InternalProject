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
    public class AdminRequest : IAdminRequestServices
    {


    

    
   

       public async Task<PilotRequestResponse> GetAllRequest(string status)
        {
            try
            {
                var url = UrlHelper.adminAllRequestURL + "/" + status;

                var resultString = await HTTPClientWrapper<PilotRequestResponse>.Get(url);
                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
        public async Task<PilotRequestResponse> GetAllRidesByPilotStatus(string status)
        {
            try
            {
                var url = UrlHelper.adminGetAllRidesByPilotStatus + "/" + status;

                var resultString = await HTTPClientWrapper<PilotRequestResponse>.Get(url);
                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }

        async Task<RideResponse> IAdminRequestServices.GetRideById(int rideId)
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
    }
}
