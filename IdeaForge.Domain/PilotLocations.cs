using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IdeaForge.Domain
{
      // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class PilotLocationResponse
        {
            public string message { get; set; }
            public bool status { get; set; }
            public List<PilotLocation> userData { get; set; }
        }

        public class PilotLocation
        {
            public int id { get; set; }
            public string locationName { get; set; }
            public int userId { get; set; }
            public int cityId { get; set; }
            public int reasonId { get; set; }
            public string comments { get; set; }
            public string city_Name { get; set; }
            public string reasonDescription { get; set; }
            public bool isActive { get; set; }
        public Visibility IsVisible { get; set; }
        public int SRNO { get; set; }
        public string StringStatus { get; set; }
    }



}
