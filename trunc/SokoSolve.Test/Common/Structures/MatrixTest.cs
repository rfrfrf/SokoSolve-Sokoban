using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Test.Common
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void TestMatrixAverage()
        {
            Matrix mm = new Matrix(new SizeInt(5, 5));
            mm[2, 2] = 16f;
            mm[3, 4] = 32f;

            Console.WriteLine(mm.ToString());

            mm = mm.Average(new Bitmap(5,5));

            Console.WriteLine(mm.ToString());

            Console.WriteLine("Done");
        }
    }
}
