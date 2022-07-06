using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class PilotLocation
    {
        public class Root
        {
            public int id { get; set; }
            public string locationName { get; set; }
            public int userId { get; set; }
            public User user { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string email { get; set; }
            public string name { get; set; }
            public int languageID { get; set; }
            public string contactNo { get; set; }
            public string organizationName { get; set; }
            public string departmentName { get; set; }
            public string city { get; set; }
            public DateTime addedon { get; set; }
            public DateTime lastEditon { get; set; }
            public int lastEditby { get; set; }
            public int addedBy { get; set; }
            public int roleID { get; set; }
            public bool isActive { get; set; }
            public bool isApproved { get; set; }
            public string image { get; set; }
        }
    }
}
