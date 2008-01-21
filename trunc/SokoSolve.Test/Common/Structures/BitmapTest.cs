using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Analysis;
using SokoSolve.Core.Model;

namespace SokoSolve.Test.Common.Structures
{
	[TestClass]
	public class BitmapTest
	{
		[TestMethod]
		public void FloodFill()
		{
            Bitmap map = new Bitmap(new string[]
			                        	{
			                        		"111111111111111111",
			                        		"100000000000001001",
			                        		"100000000000001001",
			                        		"100001111111001001",
											"100001010001001001",
			                        		"100001000101001001",
			                        		"100001111111000001",
											"111011000000001111",
			                        		"100000001000010001",
			                        		"100000001000010001",
			                        		"111111111111111111"
			                        	});

			Debug.WriteLine(map.ToString());

			Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
			FloodFillStrategy strategy = new FloodFillStrategy(map, new VectorInt(2, 2));
			eval.Evaluate(strategy);

			Debug.WriteLine(strategy.Result.ToString());

			List<LocationNode> path = strategy.GetShortestPath(new VectorInt(9, 9));
		    Assert.IsNotNull(path);

			
			Debug.WriteLine(StringHelper.Join(path, delegate(LocationNode node) { return node.Location.ToString(); }, ", "));

			Debug.WriteLine(map.ToString());

            // Expected value
		    Assert.AreEqual(new VectorInt(9, 9), path[path.Count - 1].Location);
		}

        [TestMethod]
        public void FloodCrateMoveMap()
        {
         

             SokobanMap map = new SokobanMap();
		    map.SetFromStrings(new string[]
		                           {
"~~~###~~~~~",
"~~##.#~####",
"~##..###..#",
"##.X......#",
"#...PX.#..#",
"###.X###..#",
"~~#..#OO..#",
"~##.##O#.##",
"~#......##~",
"~#.....##~~",
"~#######~~~"
		                           });

            Bitmap result = CrateAnalysis.BuildCrateMoveMap(map, new VectorInt(3,3));
            Assert.IsNotNull(result);

            Debug.WriteLine(map.ToString());
            Debug.WriteLine(result.ToString());
            Debug.WriteLine("done.");
        }

        [TestMethod]
        public void TestFindCratePath()
        {


            SokobanMap map = new SokobanMap();
            map.SetFromStrings(new string[]
		                           {
"~~~###~~~~~",
"~~##.#~####",
"~##..###..#",
"##.X......#",
"#...PX.#..#",
"###.X###..#",
"~~#..#OO..#",
"~##.##O#.##",
"~#......##~",
"~#.....##~~",
"~#######~~~"
		                           });

            CrateAnalysis.ShortestCratePath result = CrateAnalysis.FindCratePath(map, new VectorInt(3, 3), new VectorInt(9, 5));
            Assert.IsNotNull(result);

            Debug.WriteLine(map.ToString());
            Debug.WriteLine(result.CratePath.ToString());
            Debug.WriteLine(result.PlayerPath.ToString());
            Debug.WriteLine("done.");
        }

        [TestMethod]
        public void TestFindCratePath_DeeperTest()
        {
            string mapString =
@"~~~~~~~~~~~#####
~~~~~~~~~~##...#
~~~~~~~~~~#....#
~~~~####~~#.X.##
~~~~#..####X.X#~
~~~~#.....X.X.#~
~~~##.##.X.X.X#~
~~~#..O#..X.X.#~
~~~#..O#......#~
#####.#########~
#OOOO.P..#~~~~~~
#OOOO....#~~~~~~
##..######~~~~~~
~####~~~~~~~~~~~";

            SokobanMap map = new SokobanMap();
            map.SetFromString(mapString);

            CrateAnalysis.ShortestCratePath result = CrateAnalysis.FindCratePath(map, new VectorInt(10, 7), new VectorInt(7, 5));
            Assert.IsNull(result, "There is not cratemovepath to from position 10,7 to 7,5");

        }
	}
}

