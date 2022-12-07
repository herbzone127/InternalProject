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

namespace IdeaForge.Service.GenericServices
{
    public class CompleteRideServices : ICompleteRideServices
    {
        public async Task<CompleteRideModel> GetCompletedRide(CompleteRideModelRideId compRideId)
        {
            string token = compRideId.token;
            try
            {
                var url = UrlHelper.pilotSaveProfileURl;//pilotCompletedRideURl;

                var serializeJson = JsonConvert.SerializeObject(compRideId);
                var resultString = await HTTPClientWrapper<CompleteRideModelRideId>.PostRequestToken(url, compRideId, token);
                var result = JsonConvert.DeserializeObject<CompleteRideModel>(resultString);
                //var url = UrlHelper.loginURL;
                //var result = await HTTPClientWrapper<Login>.Get(url);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
