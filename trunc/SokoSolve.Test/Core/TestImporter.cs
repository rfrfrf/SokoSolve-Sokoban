using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SokoSolve.Core.IO;

namespace SokoSolve.Test.Core
{
    [TestFixture]
    public class TestImporter
    {
        [Test]
        public void TestTXT()
        {
            ImportTXT importer = new ImportTXT();
            importer.Import("../../../SokoSolve.Test/Core/Mas Microban.txt");

            Assert.IsNull(importer.LastError);
        }
    }
}
