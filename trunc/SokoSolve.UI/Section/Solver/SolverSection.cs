using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Section.Solver
{
    /// <summary>
    /// Master control for the Solver Block.
    /// </summary>
    public partial class SolverSection : UserControl
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SolverSection()
        {
            InitializeComponent();

            OnComplete = SolverComplete;
            solverEvalStatus = EvalStatus.NotStarted;
        }

        /// <summary>
        /// The current map for solving
        /// </summary>
        public PuzzleMap Map
        {
            get { return map; }
            set
            {
                map = value;
                tsbStart.Enabled = map != null;
                complete = false;
                solver = null;
                solverEvalStatus = EvalStatus.NotStarted;

                  // This only happend once
                bitmapViewerStatic.MapSize = map.Map.Size;
                bitmapViewerNodeMaps.MapSize = map.Map.Size;
            

                if (map != null)
                {
                    pictureBoxStaticImage.Image = DrawingHelper.DrawPuzzle(map);
                }
            }
        }

        /// <summary>
        /// Status Helper, is the solver thread running?
        /// </summary>
        private bool SolverActive
        {
            get { return (worker != null && worker.IsAlive); }
        }

        /// <summary>
        /// Status Text Helper
        /// </summary>
        private string Status
        {
            get
            {
                if (complete) return "Complete " + solverEvalStatus.ToString();
                if (SolverActive) return "Working " + solverEvalStatus.ToString();
                return "Inactive " + solverEvalStatus.ToString();
            }
        }

        /// <summary>
        /// Start the solver (this will always to run with a non UI worker thread)
        /// </summary>
        private void Solve()
        {
            try
            {
                complete = false;
                solver = new SolverController(map);
                solverEvalStatus = solver.Solve();
                complete = true;    
            }
            catch(Exception ex)
            {
                lastException = ex;
                return;
            }
            
            Invoke(OnComplete);
        }

        

        /// <summary>
        /// Invoked (but back on the UI thread) on completeion of the solver
        /// </summary>
        private void SolverComplete()
        {
            try
            {
                complete = true;
                timer.Enabled = false;
                worker = null;
                UpdateStatus();
            }
            catch(Exception ex)
            {
                lastException = ex;
                return;
            }
        }



        /// <summary>
        /// Update or Bind to the UI
        /// </summary>
        private void UpdateStatus()
        {
            
            tsbStop.Enabled = SolverActive;
            tsbPause.Enabled = SolverActive;
            tsbStart.Enabled = !SolverActive;

            if (lastException != null)
            {
                labelStats.Text = StringHelper.Report(lastException);
                tslStatus.Text = lastException.Message;
                return;
            }

            tslStatus.Text = string.Format("{1} - {0}", Status, map.Puzzle.Details.Name);

            if (SolverActive || complete)
            {
                if (solver != null && solver.Strategy != null && solver.Strategy.StaticAnalysis != null)
                {

                    if (!bitmapViewerStatic.HasLayers)
                    {
                        BitmapViewer.Layer mapLayer = new BitmapViewer.Layer();
                        mapLayer.Order = 0;
                        mapLayer.IsVisible = true;
                        mapLayer.Map = solver.Map;
                        mapLayer.Name = "Puzzle";
                        bitmapViewerStatic.SetLayer(mapLayer);

                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.WallMap, new SolidBrush(Color.FromArgb(120, Color.Gray)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.FloorMap, new SolidBrush(Color.FromArgb(120,Color.Green)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.InitialCrateMap, new SolidBrush(Color.FromArgb(120,Color.Blue)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.DeadMap, new SolidBrush(Color.FromArgb(120,Color.Brown)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.BoundryMap, new SolidBrush(Color.FromArgb(120,Color.LightGray)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.GoalMap, new SolidBrush(Color.FromArgb(120,Color.Yellow)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.CornerMap, new SolidBrush(Color.FromArgb(120,Color.Pink)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.RecessMap, new SolidBrush(Color.FromArgb(120,Color.Cyan)));
                        bitmapViewerStatic.Render();
                    }
                    

                    if (solver.Strategy.EvaluationTree != null)
                    {
                        treeViewer.Init(solver.Strategy.EvaluationTree);
                        treeViewer.Render();
                    }
                }

                // Stats
                if (solver != null && solver.Strategy != null)
                {
                    SolverLabelList txt = solver.Stats.GetDisplayData();
                    labelStats.Text = txt == null ? "" : txt.ToString();
                }
            }
        }

        /// <summary>
        /// Timer tick (update & monitor status)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }


        /// <summary>
        /// Exit click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e)
        {
            FormMain main = FindForm() as FormMain;
            main.Mode = FormMain.Modes.Library;
        }

        /// <summary>
        /// Start solver 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStart_Click(object sender, EventArgs e)
        {
            bitmapViewerStatic.Clear();
            bitmapViewerNodeMaps.Clear();

            if (!SolverActive)
            {
                worker = new Thread(new ThreadStart(Solve));
                timer.Enabled = true;
                worker.Start();
                UpdateStatus();
            }
            else
            {
                worker.Resume();
            }
        }

        /// <summary>
        /// Stop/Abort the solver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStop_Click(object sender, EventArgs e)
        {
            if (SolverActive)
            {
                worker.Abort();
                worker = null;
                UpdateStatus();
            }
        }

        /// <summary>
        /// Pause the solver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPause_Click(object sender, EventArgs e)
        {
            if (SolverActive)
            {
                worker.Suspend();
                UpdateStatus();
            }
        }

        /// <summary>
        /// Select a node in the tree browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewer_OnClickNode(object sender, SolverNodeEventArgs e)
        {
            if (e.Node == null) return;

            SokobanMap build = BuildCurrentMap(e.Node.Data);
            if (build != null)
            {
                BitmapViewer.Layer puzzleLayer = new BitmapViewer.Layer();
                puzzleLayer.Name = "Puzzle";
                puzzleLayer.Map = build;
                puzzleLayer.Order = 0;
                puzzleLayer.IsVisible = true;
                bitmapViewerNodeMaps.SetLayer(puzzleLayer);
            }
            

            if (e.Node.Data.MoveMap != null)
            {
                SolverBitmap move = new SolverBitmap("MoveMap", e.Node.Data.MoveMap);
                bitmapViewerNodeMaps.SetLayer(move, new SolidBrush(Color.FromArgb(120, Color.Green)));
            }

            if (e.Node.Data.DeadMap != null)
            {
                bitmapViewerNodeMaps.SetLayer(e.Node.Data.DeadMap, new SolidBrush(Color.FromArgb(120, Color.Black)));
            }


            // Build details
            SolverLabelList txt = e.Node.Data.GetDisplayData();
            labelNodeDetails.Text = txt.ToString();

            bitmapViewerNodeMaps.Render();
        }

        private SokobanMap BuildCurrentMap(SolverNode node)
        {
            SokobanMap result = new SokobanMap();
            result.Init(node.CrateMap.Size);

            for (int cx = 0; cx < result.Size.Width; cx++)
                for (int cy = 0; cy < result.Size.Height; cy++)
                {
                    if (solver.Strategy.StaticAnalysis.WallMap[cx, cy]) result[cx, cy] = CellStates.Wall;
                    if (solver.Strategy.StaticAnalysis.FloorMap[cx, cy]) result[cx, cy] = CellStates.Floor;
                    if (solver.Strategy.StaticAnalysis.GoalMap[cx, cy]) result.setState(new VectorInt(cx, cy), Cell.Goal);
                    if (node.CrateMap[cx, cy]) result.setState(new VectorInt(cx, cy), Cell.Crate);
                    result.setState(node.PlayerPosition, Cell.Player);
                }

                return result;
        }

        /// <summary>
        /// Move to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPageStats_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(labelStats.Text);
        }

       

        #region Nested type: OnCompleteDelegate

        /// <summary>
        /// Target call-back for completion
        /// </summary>
        private delegate void OnCompleteDelegate();

        #endregion

        /// <summary>
        /// Is the solver completed?
        /// </summary>
        private bool complete;

        private PuzzleMap map;

        /// <summary>
        /// Target call-back for completion
        /// </summary>
        private OnCompleteDelegate OnComplete;

        private SolverController solver;
        private EvalStatus solverEvalStatus;
        private Thread worker;
        private Exception lastException;

       
    }
}