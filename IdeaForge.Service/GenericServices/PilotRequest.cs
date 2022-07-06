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
    public class PilotRequest : IPilotRequestServices
    {
        public async Task<PilotRequestResponse> GetTodaysRequest(string status)
        {
            try
            {
                var url = UrlHelper.pilotRequestURL+"/"+status;
              
                var resultString = await HTTPClientWrapper<PilotRequestResponse>.PostRequest(url,null);
                var resultData = JsonConvert.DeserializeObject<PilotRequestResponse>(resultString);
                return resultData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
   

        async Task<PilotRequestResponse> IPilotRequestServices.GetAllRequest(string status)
        {
            try
            {
                var url = UrlHelper.pilotAllRequestURL + "/" + status;

                var resultString = await HTTPClientWrapper<PilotRequestResponse>.PostRequest(url,null);
                var resultData = JsonConvert.DeserializeObject<PilotRequestResponse>(resultString); 
                return resultData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }
    }
}
