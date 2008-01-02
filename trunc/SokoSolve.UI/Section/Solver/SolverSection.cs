using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Structures;
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

            OnComplete = SolverCompelte;
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
                if (complete) return "Complete -" + solverEvalStatus.ToString();
                if (SolverActive) return "Working -" + solverEvalStatus.ToString();
                return "Inactive -" + solverEvalStatus.ToString();
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

        private Exception lastException;

        /// <summary>
        /// Invoked (but back on the UI thread) on completeion of the solver
        /// </summary>
        private void SolverCompelte()
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

           // if (SolverActive || complete)
            {
                if (solver != null && solver.Strategy != null && solver.Strategy.StaticAnalysis != null)
                {
                    if (!bitmapViewerStatic.HasBitmaps)
                    {
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.WallMap, new SolidBrush(Color.Gray));
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.FloorMap, new SolidBrush(Color.Green));
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.InitialCrateMap, new SolidBrush(Color.Blue));
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.DeadMap, new SolidBrush(Color.Brown));
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.BoundryMap, new SolidBrush(Color.LightGray));
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.GoalMap, new SolidBrush(Color.Yellow));
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.CornerMap, new SolidBrush(Color.Pink));
                        bitmapViewerStatic.Add(solver.Strategy.StaticAnalysis.RecessMap, new SolidBrush(Color.Cyan));
                        bitmapViewerStatic.Refresh();
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

            bitmapViewerNodeMaps.Clear();
            if (e.Node.Data.CrateMap != null)
            {
                SolverBitmap crate = new SolverBitmap("Crate Map", e.Node.Data.CrateMap);
                bitmapViewerNodeMaps.Add(crate, new SolidBrush(Color.Blue));
            }

            if (e.Node.Data.MoveMap != null)
            {
                SolverBitmap move = new SolverBitmap("MoveMap", e.Node.Data.MoveMap);
                bitmapViewerNodeMaps.Add(move, new SolidBrush(Color.Green));
            }

            if (e.Node.Data.DeadMap != null)
            {
                bitmapViewerNodeMaps.Add(e.Node.Data.DeadMap, new SolidBrush(Color.Black));
            }

            bitmapViewerNodeMaps.Add(solver.Strategy.StaticAnalysis.DeadMap, new SolidBrush(Color.DarkGray)); 


            SolverBitmap player = new SolverBitmap("Player", new SokoSolve.Common.Structures.Bitmap(e.Node.Data.CrateMap.Size));
            player[e.Node.Data.PlayerPosition] = true;
            bitmapViewerNodeMaps.Add(player, new SolidBrush(Color.White));

            // Build details
            SolverLabelList txt = e.Node.Data.GetDisplayData();
            labelNodeDetails.Text = txt.ToString();

            bitmapViewerNodeMaps.Refresh();
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

        /// <summary>
        /// Move to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPageStats_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(labelStats.Text);
        }
    }
}