using IdeaForge.Service.IGenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaForge.Core.Utilities;
using IdeaForge.Data;
using IdeaForge.Domain;
using Newtonsoft.Json;

namespace IdeaForge.Service.GenericServices
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponse> Login(Login login)
        {
            try
            {
                var url = UrlHelper.loginURL;
                
                var serializeJson = JsonConvert.SerializeObject(login);
                var resultString = await HTTPClientWrapper<Login>.PostRequest(url,login);
                var result = JsonConvert.DeserializeObject<LoginResponse>(resultString);
                //var url = UrlHelper.loginURL;
                //var result = await HTTPClientWrapper<Login>.Get(url);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
        public async Task<OTPResponse> OTP(PilotOTP pilotOPT) 
        {
            try 
            {
                var url =UrlHelper.pilotOTPURl;
                var serializeJson = JsonConvert.SerializeObject(pilotOPT);
                var resultString = await HTTPClientWrapper<PilotOTP>.PostRequest(url, pilotOPT);
                var result = JsonConvert.DeserializeObject<OTPResponse>(resultString);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
