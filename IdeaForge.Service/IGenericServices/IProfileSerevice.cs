using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Service.IGenericServices
{
    public interface  IProfileSerevice
    {
         Task<ProfileModel> GetProfile(ProfilemodelID profilID);
        Task<ProfileModel> SaveProfile(UserDataProfile profiledata);
        
    }
}
