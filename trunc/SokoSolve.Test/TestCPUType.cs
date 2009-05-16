using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using NUnit.Framework;
using SokoSolve.Common;

namespace SokoSolve.Test
{
    [TestFixture]
    public class TestCPUType
    {
        [Test]
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

        [Test]
        public void TestCPUFunction()
        {
            Console.WriteLine(DebugHelper.GetCPUDescription());
        }

    }
}
