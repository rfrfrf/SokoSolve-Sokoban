using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SokoSolve.Common.Math;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.Reporting;
using SokoSolve.Core.UI;

namespace SokoSolve.Test.Core.Reporting
{
    [TestFixture]
    public class TestReporting : TestPuzzleBase
    {
        [Test]
        public void TestLibraryReport()
        {
            XmlProvider xmlHelper = new XmlProvider();
            Library lib = xmlHelper.Load(MakePathUIContent("Libraries\\Sasquatch.ssx"));

            LibraryReport rpt = new LibraryReport(lib, new StaticImage(ResourceController.Singleton.GetInstance("default"), new VectorInt(16,16)),@".");
            rpt.BuildReport();
            rpt.Save(@"C:\junk\library.html");
        }


        [Test]
        public void TestSolverReport()
        {
            XmlProvider xmlHelper = new XmlProvider();
            Library lib = xmlHelper.Load(MakePathUIContent("Libraries\\Sasquatch.ssx"));

            List<SolverResult> results = new List<SolverResult>();

            SolverController one = new SolverController(lib.Puzzles[0].MasterMap);
            results.Add(one.Solve());

            SolverController two = new SolverController(lib.Puzzles[1].MasterMap);
            results.Add(two.Solve());

            SolverResultHTML rpt = new SolverResultHTML(results, null);
            rpt.BuildReport();
            rpt.Save(@"solver.html");
        }
    }
}
