using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IdeaForge.Domain.RideByPilot;

namespace IdeaForge.Service.IGenericServices
{
    public interface IAdminRequestServices
    {
        
        Task<PilotRequestResponse> GetAllRequest(string status);
        Task<RideResponse> GetRideById(int rideId);

        Task<PilotLocationResponse> GetPilotLocations(int userId);
   
        Task<ReasonsResponse> GetReasons();
        Task<adminLocationResponse> GetAdminLocations();
        Task<String> AddAdminLocations(addminPilotLocation adminPilot);
        Task<PilotRequestResponse> GetAllRidesByPilotStatus(string status);
        Task<Repotresponse> GetAllridereport(string city, string date);
    }
}
