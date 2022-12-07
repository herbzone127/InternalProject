using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

    public class AllUserData
    {
        public string message { get; set; }
        public bool status { get; set; }
        public List<UserGetdata> userData { get; set; }
    }

    public class UserGetdata
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int? LanguageID { get; set; }
        public string ContactNo { get; set; }
        public string OrganizationName { get; set; }
        public string DepartmentName { get; set; }
        public string City { get; set; }
        public DateTime Addedon { get; set; }
        public string Addedondate { get; set; }
        public string Addedontime { get; set; }
        public string Token { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public string Image { get; set; }
        public Visibility IsVisibleButton { get; set; }
        public Visibility ViewButtonVisible { get; set; }
        public int SRNO { get; set; }
    }

}