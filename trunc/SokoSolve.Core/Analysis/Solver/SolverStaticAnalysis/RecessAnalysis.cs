using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis
{
    /// <summary>
    /// Create a list of puzzle recesses <see cref="Recess"/>
    /// </summary>
    public class RecessAnalysis
    {

        /// <summary>
        /// Find all puzzle corner cells
        /// </summary>
        /// <param name="staticAnalysis"></param>
        /// <returns></returns>
        public  SolverBitmap FindCorners(StaticAnalysis staticAnalysis)
        {
            SolverBitmap cornerMap = new SolverBitmap("Corner Map", staticAnalysis.WallMap.Size);

            // No need to check the first and last lines
            for (int cx = 1; cx < cornerMap.Size.Width - 1; cx++)
                for (int cy = 1; cy < cornerMap.Size.Height - 1; cy++)
                {
                    // Check TopLeft
                    if (CheckCorner(staticAnalysis, new VectorInt(cx, cy)))
                    {
                        // Corner found
                        cornerMap[cx, cy] = true;
                    }
                }

            return cornerMap;
        }

        /// <summary>
        /// Find all recesses (goal and non-goal) in puzzle map
        /// </summary>
        /// <param name="staticAnalysis"></param>
        /// <param name="outRecesses"></param>
        /// <param name="outRecessMap"></param>
        public void FindRecesses(StaticAnalysis staticAnalysis, out List<Recess> outRecesses, out Bitmap outRecessMap)
        {
            outRecessMap = new Bitmap(staticAnalysis.WallMap.Size);
            outRecesses = new List<Recess>();

            foreach (VectorInt corner in staticAnalysis.CornerMap.TruePositions)
            {
                CheckRecessFromCorner(staticAnalysis, outRecesses, outRecessMap, corner, Direction.Up);
                CheckRecessFromCorner(staticAnalysis, outRecesses, outRecessMap, corner, Direction.Down);
                CheckRecessFromCorner(staticAnalysis, outRecesses, outRecessMap, corner, Direction.Left);
                CheckRecessFromCorner(staticAnalysis, outRecesses, outRecessMap, corner, Direction.Right);
            }
        }

        /// <summary>
        /// Check an individual node
        /// </summary>
        /// <param name="context"></param>
        /// <param name="checkCell"></param>
        /// <returns></returns>
        private bool CheckCorner(StaticAnalysis context, VectorInt checkCell)
        {
            // Check to see if this if a floor. As we do not have an explicit floor map use the wall map implicitly
            if (context.WallMap[checkCell]) return false;

            // Check TopRight
            if (context.WallMap[checkCell.Add(0, -1)] && context.WallMap[checkCell.Add(1, 0)]) return true;

            // Check TopLeft
            if (context.WallMap[checkCell.Add(0, -1)] && context.WallMap[checkCell.Add(-1, 0)]) return true;

            // Check BottomRight
            if (context.WallMap[checkCell.Add(0, 1)] && context.WallMap[checkCell.Add(1, 0)]) return true;

            // Check BottomLeft
            if (context.WallMap[checkCell.Add(-1, 0)] && context.WallMap[checkCell.Add(0, 1)]) return true;

            return false;
        }

        /// <summary>
        /// Moving in each direction try to find another non-goal corner
        /// </summary>
        private void CheckRecessFromCorner(StaticAnalysis staticAnalysis,  List<Recess> outRecesses, Bitmap outRecessMap, VectorInt CheckPos, Direction CheckDirection)
        {
            RectangleInt region = new RectangleInt(new VectorInt(0, 0), outRecessMap.Size.Subtract(1, 1));
            VectorInt pos = CheckPos;

            Direction sideA;
            Direction sideB;
            // Check Recess wall side
            if (CheckDirection == Direction.Up || CheckDirection == Direction.Down)
            {
                sideA = Direction.Left;
                sideB = Direction.Right;
            }
            else
            {
                sideA = Direction.Up;
                sideB = Direction.Down;
            }

            bool hasSideA = true;
            bool hasSideB = true;

            // Try to find another corner with SideA or SideB
            while (region.Contains(pos))
            {
                // Check Fail
               
                if (staticAnalysis.WallMap[pos]) return; // Wall in way- Fail

                if (hasSideA)
                {
                    if (!staticAnalysis.WallMap[pos.Offset(sideA)]) hasSideA = false;
                }

                if (hasSideB)
                {
                    if (!staticAnalysis.WallMap[pos.Offset(sideB)]) hasSideB = false;
                }

                if (!hasSideA && !hasSideB) return;

                // Check success
                // Don't check the first time
                if (pos != CheckPos)
                {
                    if (staticAnalysis.CornerMap[pos])
                    {
                        Recess newRecess = new Recess("Recess #"+(outRecesses.Count+1).ToString(), outRecessMap.Size);
                        outRecesses.Add(newRecess);
                        // Winner
                        // Mark all in path as corner
                        VectorInt setTrue = CheckPos;
                        while (setTrue != pos)
                        {
                            // Individual
                            newRecess[setTrue] = true;
                            // Master (all recesses)
                            outRecessMap[setTrue] = true;

                            // Next
                            setTrue = setTrue.Offset(CheckDirection);
                        }

                        // Do the final node
                        newRecess[pos] = true;
                        outRecessMap[pos] = true;

                        newRecess.GoalCount = newRecess.BitwiseAND(staticAnalysis.GoalMap).Count;
                        return;
                    }
                }

                // Next
                pos = pos.Offset(CheckDirection);
            }
        }
    }
}
