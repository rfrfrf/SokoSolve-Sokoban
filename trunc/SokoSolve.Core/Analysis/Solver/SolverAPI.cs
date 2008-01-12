using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Facade Pattern to simplify the <see cref="SolverController"/> use be downstream users
    /// </summary>
    public class SolverAPI
    {
        /// <summary>
        /// Attempt to solve a sokoban puzzle.
        /// </summary>
        /// <param name="Map">Puzzle to solve</param>
        /// <returns>null implies no solution, or failed attempt</returns>
        public SolverResult Solve(PuzzleMap Map)
        {
            SolverController ctrl = new SolverController(Map);
            ctrl.Solve();

            throw new NotImplementedException();

            // Convert solution 
            return null;
        }
    }

   
}
