using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Test.Common
{
    [TestFixture]
    public class MatrixTest
    {
        [Test]
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
