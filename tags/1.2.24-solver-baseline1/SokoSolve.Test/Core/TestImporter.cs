using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Core.IO;

namespace SokoSolve.Test.Core
{
    [TestClass]
    public class TestImporter
    {
        [TestMethod]
        public void TestTXT()
        {
            ImportTXT importer = new ImportTXT();
            importer.Import("../../../SokoSolve.Test/Core/Mas Microban.txt");

            Assert.IsNull(importer.LastError);
        }
    }
}
