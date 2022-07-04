using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class Login
    {
        public string email_PhoneNo { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class LoginResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public UserData userData { get; set; }
    }

    public class UserData
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public int? languageID { get; set; }
        public string contactNo { get; set; }
        public string organizationName { get; set; }
        public string departmentName { get; set; }
        public string city { get; set; }
        public string token { get; set; }
        public int roleID { get; set; }
        public bool isActive { get; set; }
        public bool isApproved { get; set; }
        public string image { get; set; }
    }


}