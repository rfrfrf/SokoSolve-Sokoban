using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Common;

namespace SokoSolve.Test
{
    [TestClass]
    public class TestCPUType
    {
        [TestMethod]
        public void TestCPU()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_Processor");
            ManagementObjectCollection results = searcher.Get();
            if (results != null)
            {
                //http://msdn2.microsoft.com/en-us/library/aa394373(VS.85).aspx
                foreach (ManagementBaseObject result in results)
                {
                    Console.WriteLine(result["Name"]);
                    Console.WriteLine(result["CurrentClockSpeed"]);
                    Console.WriteLine(result["NumberOfCores"]);
                    Console.WriteLine(result["NumberOfLogicalProcessors"]);
                }
            }

            Console.WriteLine("DOne");
        }

        [TestMethod]
        public void TestCPUFunction()
        {
            Console.WriteLine(DebugHelper.GetCPUDescription());
        }

    }
}
