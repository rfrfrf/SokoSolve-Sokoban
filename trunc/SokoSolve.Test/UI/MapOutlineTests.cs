using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Analysis;
using SokoSolve.Core.Model;

namespace SokoSolve.Test.UI
{

    public class MapOutline
    {
        private SokobanMap map;
        private VectorInt start;
        private VectorInt end;
        private Mode mode;

        public enum Mode
        {
            None,
            PlayerMove,
            CrateMove,
            Error
        }

        public void Start(VectorInt s)
        {
            if (map.Check(Cell.Crate, s))
            {
                mode = Mode.CrateMove;
                start = s;
                return;
            }

            if (map.Check(Cell.Floor, s))
            {
                mode = Mode.PlayerMove;
                start = s;
                return;
            }

            start = VectorInt.Null;
            mode = Mode.Error;
        }

        public IEnumerable<Direction> End(VectorInt e)
        {
            if (mode == Mode.PlayerMove)
            {
                CrateAnalysis.ShortestCratePath path = CrateAnalysis.FindCratePath(map, start, end);
                //if (path != null)
                //{
                //    var p = map.Player;
                //    foreach (Direction step in path.PlayerPath.Moves)
                //    {
                //        p.Add(new VectorInt(step));
                //        stack.Add(p);
                //    }
                //}
            }

            return null;
        }
    }


    class MapOutlineTests
    {
    }
}
