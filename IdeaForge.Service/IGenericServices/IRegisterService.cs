using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Service.IGenericServices
{
    public interface IRegisterService
    {
        Task<RegisterResponse> Register(Register register);
        Task<CityData> GetCityList();
    }
}
