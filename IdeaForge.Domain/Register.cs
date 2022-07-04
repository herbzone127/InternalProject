using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class Register
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string languageID { get; set; }
        public string contactNo { get; set; }
        public string organizationName { get; set; }
        public string departmentName { get; set; }
        public string city { get; set; }
        public string token { get; set; }
        public int roleID { get; set; }
        public bool isActive { get; set; }
        public bool isApproved { get; set; }
    }
    public class RegisterResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public Register userData { get; set; }
    }
}
