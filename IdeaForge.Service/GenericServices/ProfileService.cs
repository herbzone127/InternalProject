using IdeaForge.Core.Utilities;
using IdeaForge.Data;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdeaForge.Service.GenericServices
{
    public class ProfileService : IProfileSerevice
    {
        public async Task<ProfileModel> GetProfile(ProfilemodelID profilID)
        {
           
            try
            {
                string url = string.Empty;
                if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
                {



                    var dataProfile = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                   
                    if (dataProfile.roleID == 2)
                        url = UrlHelper.adminProfileURl;
                    if (dataProfile.roleID == 3)
                        url = UrlHelper.adminProfileURl;
                }
                  


                var serializeJson = JsonConvert.SerializeObject(profilID);
                var resultString = await HTTPClientWrapper<ProfilemodelID>.PostRequestToken(url, profilID, Global.Token);
                if(resultString != null)
                {
                    var result = JsonConvert.DeserializeObject<ProfileModel>(resultString);
                    //var url = UrlHelper.loginURL;
                    //var result = await HTTPClientWrapper<Login>.Get(url);
                    return result;
                }
                return null; 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<ProfileModel> SaveProfile(UserDataProfile dataProfile)
        {
            string token = dataProfile.token.ToString();
            try
            {
                string url = string.Empty;
                if(dataProfile.roleID==2)
                 url = UrlHelper.pilotSaveProfileURl;
               if(dataProfile.roleID==3)
                    url = UrlHelper.adminSaveProfileURl;
                var result = await HTTPClientWrapper<UserDataProfile>.PostRequestToken(url, dataProfile,token);
                var resultModel = JsonConvert.DeserializeObject<ProfileModel>(result);
                return resultModel;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
