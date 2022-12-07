using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Service.IGenericServices
{
    public interface IAdminRequestPage
    {
        Task<AllUserData> GetAllUser();
        Task<AllUserData> isActive(bool isActive, Int32 userId);
       
    }
}
