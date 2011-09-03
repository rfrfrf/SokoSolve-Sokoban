using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Analysis;

namespace SoloSolve.UI.WPF.Game
{
    

    public class CursorMovement
    {
        private Map map;
        private VectorInt start;
        private VectorInt end;
        private Path pathMoves;
        private Path pathCrateMoves;
        private Modes mode;
        readonly Color cHover = Colors.Black;
        private readonly Color cCrateHover = Colors.BlueViolet;

        public enum Modes
        {
            None,
            Player,
            Crate
        }

        public CursorMovement(Map map)
        {
            this.map = map;
        }

        public void Start(VectorInt s)
        {
            start = s;
            if (map.Logic.Current.Check(Cell.Crate, start))
            {
                mode = Modes.Crate;
            }
            else if (map.Logic.Current.Check(Cell.Floor, start))
            {
                mode = Modes.Player;
            }
        }

        public void End(VectorInt e)
        {
            end = e;
            var moves = UpdatePath(end, false);
            if (moves != null)
            {
                map.AddMoves(moves);
                start = end = VectorInt.Null;
                mode = Modes.None;
            }
        }

        public void Hover(VectorInt v)
        {
            UpdatePath(v, false);
        }

        

        private List<VectorInt> UpdatePath(VectorInt dest, bool hover)
        {
            var canvus = map.GetOverlayCanvus();

            if (mode == Modes.Player  || mode == Modes.None)
            {
                var steps = MoveAnalysis.FindPlayerPath(map.Logic.Current, dest);
                if (steps != null)
                {
                    DrawPathFromLogicalPostiions(hover, steps, canvus);
                }
                else
                {
                    canvus.Children.Remove(pathMoves);
                    pathMoves = null;
                }

                return steps;
            }

            if (mode == Modes.Crate)
            {
                var path = CrateAnalysis.FindCratePath(map.Logic.Current, start, dest);
                if (path != null)
                {
                    DrawPathFromLogicalPostiions(hover, path.PlayerPath.MovesAsPosition.ToList(), canvus);
                    return path.PlayerPath.MovesAsPosition.ToList();
                }
            }

            return null;
        }

        private void DrawPathFromLogicalPostiions(bool hover, List<VectorInt> steps, Canvas canvus)
        {
            if (pathMoves == null)
            {
                pathMoves = new Path()
                {
                    StrokeThickness = 4
                };
                canvus.Children.Add(pathMoves);
            }

            

            if (mode == Modes.Player || mode == Modes.None)
            {
                pathMoves.StrokeDashCap = PenLineCap.Square;
                pathMoves.StrokeStartLineCap = PenLineCap.Triangle;
                pathMoves.StrokeDashArray = new DoubleCollection(new double[] { 0, 2 });
                if (hover)
                {
                    pathMoves.Stroke = new SolidColorBrush(Color.FromArgb(128, cHover.R, cHover.G, cHover.B));
                }
                else
                {
                    pathMoves.Stroke = new SolidColorBrush(Colors.RosyBrown);
                }
            }
            else 
            {
                pathMoves.StrokeDashCap = PenLineCap.Square;
                pathMoves.StrokeStartLineCap = PenLineCap.Round;
                pathMoves.StrokeDashArray = null;
                if (hover)
                {
                    pathMoves.Stroke = new SolidColorBrush(Color.FromArgb(128, cCrateHover.R, cCrateHover.G, cCrateHover.B));
                }
                else
                {
                    pathMoves.Stroke = new LinearGradientBrush(Colors.DarkRed, Colors.PaleTurquoise, 45);
                }
            }
            

            // Paint trail
            var segs = new List<PathSegment>();
            foreach (var step in steps.Skip(1))
            {
                var np = map.GetPhysFromLogical(step).ToWindowsPoint();
                segs.Add(new LineSegment(np, true));
            }

            var p = new PathGeometry(new List<PathFigure>()
            {
                new PathFigure(map.GetPhysFromLogical(steps.First()).ToWindowsPoint(), segs, false)
            });

            pathMoves.Data = p;
        }
    }
}
