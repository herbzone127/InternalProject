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
    public class RegisterService : IRegisterService
    {
        public async Task<CityData> GetCityList()
        {
            try
            {
                var url = UrlHelper.getCityList;
                var result = await HTTPClientWrapper<CityData>.Get(url);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<RegisterResponse> Register(Register register)
        {
            try
            {
                var url = UrlHelper.registerURL;
                var result = await HTTPClientWrapper<Register>.PostRequest(url,register);
                var resultModel= JsonConvert.DeserializeObject<RegisterResponse>(result);
                return resultModel;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
    }
}
