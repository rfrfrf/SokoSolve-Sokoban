using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SokoSolve.Core.Analysis.Progress;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Test.Core.Solver
{
    [TestFixture]
    public class TestProgressComponent : TestPuzzleBase
    {
        [Test]
        public void TestProgressCreate()
        {
            ProgressComponent comp = new ProgressComponent();

            XmlProvider xml = new XmlProvider();
            Library lib = xml.Load(MakePathUIContent(@"Libraries\SolverDevelopment.ssx"));

            comp.Add(lib);

            comp.Save("temp_test_progress.xml");

            ProgressComponent load = new ProgressComponent();
            load.Load("temp_test_progress.xml");

            Assert.AreEqual(comp.Count, load.Count);
        }
    }
}
