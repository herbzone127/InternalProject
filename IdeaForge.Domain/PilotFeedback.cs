﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class PilotFeedback
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int rideId { get; set; }
        public bool inFlightService { get; set; }
        public bool flightControl { get; set; }
        public bool communication { get; set; }
        public string feedbackComments { get; set; }
    }
}