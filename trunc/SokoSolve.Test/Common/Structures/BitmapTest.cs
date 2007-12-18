using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;

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

			
			Debug.WriteLine(StringHelper.Join(path, delegate(LocationNode node) { return node.Location.ToString(); }, ", "));

			Debug.WriteLine(map.ToString());
		}
	}
}
