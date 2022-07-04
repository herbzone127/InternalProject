using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class ProfileModel
    {
        public string message { get; set; }
        public bool status { get; set; }
        public UserDataProfile userData { get; set; }
    }
    public class UserDataProfile
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public int languageID { get; set; }
        public string contactNo { get; set; }
        public string organizationName { get; set; }
        public string departmentName { get; set; }
        public string city { get; set; }
        public object token { get; set; }
        public int roleID { get; set; }
        public bool isActive { get; set; }
        public bool isApproved { get; set; }
        public string image { get; set; }
        public DateTime addedon { get; set; }
        public string addedondat { get; set; }

    }

    public class ProfilemodelID
    {
        public int id { get; set; }
        public string token { get; set; }
    }
}
