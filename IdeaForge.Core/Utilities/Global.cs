using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IdeaForge.Core.Utilities
{
    public static class Global
    {
        public static string email_PhoneNo { get; set; }
        public static int loginUserId { get; set; }
        public static string Token { get; set; }
        public static WindowState LoginState { get; set; }
        public static string contactNo { get; set; }
        public static int SignupCounter { get; set; }
        public static string PilotLoggedLocation { get; set; }
        public static string GoogleMapsApiKey { get; set; }
        public static bool IsIFDockStatus { get; set; }
        public static UserDatum SelectedLocation { get; set; }
        public static bool isStoped { get; set; }
        public static int RoleID { get; set; }
        public static bool inactive { get; set; }
        public static int isPopupShown { get; set; }
    }
}
