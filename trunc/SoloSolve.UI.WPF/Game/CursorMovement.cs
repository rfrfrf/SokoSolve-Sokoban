using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Path path;
        private Modes mode;

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
            }
        }

        public void Hover(VectorInt v)
        {
            UpdatePath(v, false);
        }

        private List<VectorInt> UpdatePath(VectorInt end, bool hover)
        {
            var canvus = map.GetOverlayCanvus();

            if (mode == Modes.Player)
            {
                var steps = MoveAnalysis.FindPlayerPath(map.Logic.Current, end);
                if (steps != null)
                {
                    if (path == null)
                    {
                        path = new Path()
                        {
                            StrokeThickness = 4
                        };
                        canvus.Children.Add(path);
                    }

                    var cHover = Colors.Black;

                    path.StrokeDashCap = PenLineCap.Round;
                    path.StrokeStartLineCap = PenLineCap.Triangle;
                    path.StrokeDashArray = new DoubleCollection(new double[] { 0, 2 });
                    path.Stroke = hover
                                      ? new SolidColorBrush(Color.FromArgb(128, cHover.R, cHover.G, cHover.B))
                                      : new SolidColorBrush(Colors.Orange);

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

                    path.Data = p;
                }
                else
                {
                    canvus.Children.Remove(path);
                    path = null;
                }

                return steps;
            }

            if (mode == Modes.Crate)
            {

                var steps = MoveAnalysis.FindPlayerPath(map.Logic.Current, end);
                if (steps != null)
                {
                    if (path == null)
                    {
                        path = new Path()
                        {
                            StrokeThickness = 4
                        };
                        canvus.Children.Add(path);
                    }

                    var cHover = Colors.Black;

                    path.StrokeDashCap = PenLineCap.Round;
                    path.StrokeStartLineCap = PenLineCap.Triangle;
                    path.StrokeDashArray = new DoubleCollection(new double[] { 0, 2 });
                    path.Stroke = hover
                                      ? new SolidColorBrush(Color.FromArgb(128, cHover.R, cHover.G, cHover.B))
                                      : new SolidColorBrush(Colors.Orange);

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

                    path.Data = p;
                }
                else
                {
                    canvus.Children.Remove(path);
                    path = null;
                }

                return steps;
            }

            return null;
        }



    }

        ///// <summary>
        ///// Move the crate
        ///// </summary>
        ///// <param name="startCrateLocation"></param>
        ///// <param name="targetCrateLocation"></param>
        //private void PerformCrateMovement(VectorInt startCrateLocation, VectorInt targetCrateLocation)
        //{
        //    CrateAnalysis.ShortestCratePath path = CrateAnalysis.FindCratePath(Logic.Current, startCrateLocation, targetCrateLocation);
        //    if (path != null)
        //    {
        //        var p = Logic.Current.Player;
        //        foreach (Direction step in path.PlayerPath.Moves)
        //        {
        //            p.Add(new VectorInt(step));
        //            stack.Add(p);
        //        }
        //    }
        //}
}
