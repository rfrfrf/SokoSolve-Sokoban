using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Common.Math;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.Reporting;
using SokoSolve.Core.UI;

namespace SokoSolve.Test.Core.Reporting
{
    [TestClass]
    public class TestReporting
    {
        [TestMethod]
        public void TestLibraryReport()
        {
            XmlProvider xmlHelper = new XmlProvider();
            Library lib = xmlHelper.Load("../../../SokoSolve.Test/Core/Reporting/Sasquatch.ssx");

            LibraryReport rpt = new LibraryReport(lib, new StaticImage(ResourceController.Singleton.GetInstance("default"), new VectorInt(16,16)),@"c:\junk\");
            rpt.BuildReport();
            rpt.Save(@"C:\junk\library.html");
        }


        [TestMethod]
        public void TestSolverReport()
        {
            XmlProvider xmlHelper = new XmlProvider();
            Library lib = xmlHelper.Load("../../../SokoSolve.Test/Core/Reporting/Sasquatch.ssx");

            List<SolverResult> results = new List<SolverResult>();

            SolverController one = new SolverController(lib.Puzzles[0].MasterMap);
            results.Add(one.Solve());

            SolverController two = new SolverController(lib.Puzzles[1].MasterMap);
            results.Add(two.Solve());

            SolverResultHTML rpt = new SolverResultHTML(results, null);
            rpt.BuildReport();
            rpt.Save(@"C:\junk\solver.html");
        }
    }
}
