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
    public class AdminRequestService : IAdminRequestPage
    {
        public async Task<AllUserData> GetAllUser()
        {
            try
            {
                var url = UrlHelper.adminGetAllUserUrl;

                var resultString = await HTTPClientWrapper<AllUserData>.Get(url);

                return resultString;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return null;
        }

        public async Task<AllUserData> isActive(bool Isactive, int UserId)
        {
            var url = UrlHelper.adminUserIsactiveUrl + "/" + Isactive + "/" + UserId;
            dynamic data = null; 
            var resultString = await HTTPClientWrapper<AllUserData>.PostRequest(url,data);
            var result = JsonConvert.DeserializeObject<AllUserData>(resultString);
            if (result.status)
            {
                return result;
            }
            return result;
        }
    }
}
