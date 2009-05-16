using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Test.Core.Solver
{
    /// <summary>
    /// This test collection holds the failures and finding from manual inspection of the solver. 
    /// It collects future improvements and abilities that have not yet made it into the solver in stable form.
    /// </summary>
    [TestFixture]
    class TestSolverFailures : TestPuzzleBase
    {
        [Test]
        [Ignore]
        public void TestInvalidRecessHoverHint()
        {
            string[] puzzle = new string[]
                {
"#############",
"#O#.P#..#...#",
"#O#XX...#.X.#",
"#O#..#.X#...#",
"#O#.X#..#.X##",
"#O#..#.X#..#~",
"#O#.X#..#.X#~",
"#OO..#.X...#~",
"#OO..#..#..#~",
"############~"
                };

            Assert.IsTrue(SolveFromString(puzzle).HasSolution, "Must find a solution");
        }

        [Test]
        [Ignore]
        public void TestDead_InvalidCornerHoverHint()
        {
            string[] puzzle = new string[]
                {
"~####~~~~~~~~~~~",
"##..####~~~~~~~~",
"...OOO#~~~~~~~~",
"#...OOO#~~~~~~~~",
"#...#.##~~~~~~~~",
"#...#P.####~####",
"#####...X.###..#",
"~~~~#..##X.X...#",
"~~~###.....XX..#",
"~~~#.X..##...###",
"~~~#....######~~",
"~~~######~~~~~~~"
                    };

            SolverResult result = SolveFromString(puzzle);
            Assert.IsFalse(result.HasSolution, "Must be Dead, but has solutions");
            Assert.AreEqual(1, result.Info.TotalNodes, "Must be Dead, but has more than one eval node");
        }
    }
}
