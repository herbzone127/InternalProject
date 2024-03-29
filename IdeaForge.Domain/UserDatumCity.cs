﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapControl;
namespace IdeaForge.Domain
{
    public class CityData
    {
        public string message { get; set; }
        public bool status { get; set; }
        public List<UserDatum> userData { get; set; }
    }

    public class UserDatum
    {
        public int id { get; set; }
        public string city_Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string state { get; set; }
        public bool inactive { get; set; }
        public Location position { get; set; }
    }
   
}
