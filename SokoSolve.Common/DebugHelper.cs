using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace SokoSolve.Common
{
    public static class DebugHelper
    {

        /// <summary>
        /// Use WMI (System.Managment) to get the CPU type
        /// </summary>
        /// <returns></returns>
        public static string GetCPUDescription()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_Processor");
                ManagementObjectCollection results = searcher.Get();
                if (results != null)
                {
                    //http://msdn2.microsoft.com/en-us/library/aa394373(VS.85).aspx
                    foreach (ManagementBaseObject result in results)
                    {
                        sb.AppendFormat("{0} with {1} cores. ", result["Name"], result["NumberOfCores"]);
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
#if DEBUG
                return "Cannot get CPU Type, error:" + ex.Message;
#else
                return "";
#endif
            }
        }
    }
}
