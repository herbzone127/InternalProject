using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ideaForge.Helpers
{
    public static class LocationHelper
    {
        public static string GetCurentLocation()
        {

            string strAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            if (strAssemblyPath.Contains("bin"))
            {
                strAssemblyPath = strAssemblyPath.Substring(0, strAssemblyPath.IndexOf("bin"));
            }
            else
            {
                if (strAssemblyPath.EndsWith(".dll"))
                {
                    //string[] arr = strAssemblyPath.Split(new { @"\" }, );
                    //Array.Resize(ref arr, arr.Length - 1);
                    //strAssemblyPath = String.Join("\\", arr);
                }
            }
            return strAssemblyPath;
        }
    }
}
