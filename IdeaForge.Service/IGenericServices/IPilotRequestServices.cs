﻿using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IdeaForge.Service.IGenericServices
{
    public interface IPilotRequestServices
    {
        Task<PilotRequestResponse> GetTodaysRequest(string status);
        Task<PilotRequestResponse> GetAllRequest(string status);
        Task<RideResponse> GetRideById(int rideId);
        Task<bool> GetStatusChangesResponse(bool isAccepted , int rideId);


        Task<RideUpdateResponse> UpdateRideByPilot(RideUpdateRequest rideUpdateRequest);
        Task<List<RideStatus>> GetAllStatuses();



    }
}
