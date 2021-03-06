using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IdeaForge.Domain.RideByPilot;

namespace IdeaForge.Service.IGenericServices
{
    public interface IPilotRequestServices
    {
        Task<PilotRequestResponse> GetTodaysRequest(string status);
        Task<PilotRequestResponse> GetAllRequest(string status);
        Task<RideResponse> GetRideById(int rideId);
        Task<bool> GetStatusChangesResponse(bool isAccepted , int rideId);
        Task<RideByPilotResponse> UpdateRideByPilot(Ride pilot);
        Task<List<RideStatus>> GetAllStatuses();
        Task<FlightStatusResponse> AddUpdatePilotStatus(FlightStatus pilot);
        Task<FlightFeedbackResponse> AddUpdatePilotFeedback(FlightFeedback pilot);
        Task<PilotLocationResponse> GetPilotLocations(int userId);
        Task<PilotLocationResponse> AddUpdatePilotLocations(PilotLocation location);


    }
}
