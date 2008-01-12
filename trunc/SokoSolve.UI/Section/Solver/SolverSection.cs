using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.UI;
using SokoSolve.UI.Controls.Secondary;
using Path=SokoSolve.Common.Structures.Path;

namespace SokoSolve.UI.Section.Solver
{
    /// <summary>
    /// Master control for the Solver Block.
    /// </summary>
    public partial class SolverSection : UserControl
    {
        /// <summary>
        /// Is the solver completed?
        /// </summary>
        private bool complete;

        private Exception lastException;
        private PuzzleMap map;

        /// <summary>
        /// Target call-back for completion
        /// </summary>
        private OnCompleteDelegate OnComplete;

        private SolverController solver;
        private EvalStatus solverEvalStatus;
        private Thread worker;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SolverSection()
        {
            InitializeComponent();

            OnComplete = SolverComplete;
            solverEvalStatus = EvalStatus.NotStarted;
            treeViewer.OnVisualisationClick += new EventHandler<VisEventArgs>(OnVisualisationClick_TreeViewer);
            visualisationContainerLocalNodes.OnVisualisationClick += new EventHandler<VisEventArgs>(OnVisualisationClick_LocalNode);
            visualisationContainerReverseTree.OnVisualisationClick += new EventHandler<VisEventArgs>(OnVisualisationClick_ReverseTree);

            this.Disposed += new EventHandler(SolverSection_Disposed);

            tsDropDown.SelectedIndex = 0;

            inlineBrowserSolver.NavigateIncludedContent("$html\\Solver.html");
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
                solverEvalStatus = EvalStatus.InProgress;
                solver = new SolverController(map);

                solver.ExitConditions.StopOnSolution = exitConditions.cbStopOnSolution.Checked;
                solver.ExitConditions.MaxDepth = (int)exitConditions.upMaxDepth.Value;
                solver.ExitConditions.MaxNodes = (int)exitConditions.upMaxNodes.Value;
                solver.ExitConditions.MaxItterations = (int)exitConditions.upMaxItter.Value;
                solver.ExitConditions.MaxTimeSecs = (int)(exitConditions.upMaxTime.Value * 60);
                
                solverEvalStatus = solver.Solve();
                complete = true;
            }
            catch (Exception ex)
            {
                lastException = ex;
            }

            Invoke(OnComplete); // Set to SolverComplete
        }


        /// <summary>
        /// Invoked (but back on the UI thread) on completeion of the solver
        /// </summary>
        private void SolverComplete()
        {
            try
            {
                if (lastException != null)
                {
                    FormError error = new FormError();
                    error.Exception = lastException;
                    error.ShowDialog();
                    return;
                }

                if (solver == null) return; // Thread Abort.

                // Found a solutions?
                if (solverEvalStatus == EvalStatus.CompleteSolution)
                {
                    DialogResult result = MessageBox.Show(
                        "Solution found, do you want to save it? This will also include a report in the description.",
                        "Solution Found", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        

                        Solution sol = new Solution(Map, Map.Map.Player);
                        sol.Details = new GenericDescription();
                        sol.Details.Name = "SokoSolve Solution";
                        sol.Details.Author = new GenericDescriptionAuthor();
                        sol.Details.Author.Name = ProfileController.Current.UserName;
                        sol.Details.Author.Email = ProfileController.Current.UserEmail;
                        sol.Details.Author.Homepage = ProfileController.Current.UserHomepage;
                        sol.Details.License = ProfileController.Current.UserLicense;
                        sol.Details.Date = DateTime.Now;
                        sol.Details.DateSpecified = true;

                        // Build a description
                        SolverLabelList labels = solver.Stats.GetDisplayData();
                        labels.Add("Machine", string.Format("{0} Running {1}.", DebugHelper.GetCPUDescription(), Environment.OSVersion));
                        labels.Add("SokoSolve", Program.GetVersionString());
                        sol.Details.Description = labels.ToHTML(null, "tabledata");

                        Path path = solver.Strategy.BuildPath(solver.Evaluator.Solutions[0].Data);
                        sol.Set(path);
                        Map.Solutions.Add(sol);
                    }
                }
                else
                {
                    MessageBox.Show("Solver failed: " + solverEvalStatus.ToString(), "No Solution Found",
                                    MessageBoxButtons.OK);
                }

                // Write a solver log report
                BuildSolverLogReport();

                complete = true;
                timer.Enabled = false;
                worker = null;
                UpdateStatus();
            }
            catch (Exception ex)
            {
                lastException = ex;
                FormError error = new FormError();
                error.Exception = lastException;
                error.ShowDialog();
                return;
            }
        }

        /// <summary>
        /// Write a log report to the $Content/Analysis directory
        /// </summary>
        private void BuildSolverLogReport()
        {
            if (!Directory.Exists(FileManager.getContent("$Analysis"))) return;

             // Build report
            SolverReport report = new SolverReport();
            report.AppendHeadingTwo("Puzzle");
            report.AppendLabel("Library", solver.PuzzleMap.Puzzle.Library.Details.Name);
            report.AppendLabel("LibraryID", solver.PuzzleMap.Puzzle.Library.LibraryID);
            report.AppendLabel("Puzzle", solver.PuzzleMap.Puzzle.Details.Name);
            report.AppendLabel("PuzzleID", solver.PuzzleMap.Puzzle.PuzzleID);
            report.AppendHeadingTwo("Solver");
            report.Build(solver);

            // Save
            string filename = "SolverReport-" + solver.PuzzleMap.Puzzle.Library.LibraryID +
                              solver.PuzzleMap.Puzzle.PuzzleID + "-" + DateTime.Now.Ticks.ToString() + ".html";
            File.WriteAllText(FileManager.getContent("$Analysis", filename), report.ToString());
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
                webBrowserGlobalStats.DocumentText = "<code style=\"font-size: 10pt\"><pre>" +
                                                     StringHelper.Report(lastException) +
                                                     "</pre></code>";
                tslStatus.Text = lastException.Message;
                return;
            }

            tslStatus.Text = string.Format("{1} - {0}", Status, map.Puzzle.Details.Name);

            if (SolverActive || complete)
            {
                if (solver != null && solver.Strategy != null && solver.Strategy.StaticAnalysis != null)
                {
                    richTextBoxSolverReport.Text = solver.DebugReport.ToString(new DebugReportFormatter());

                    if (!bitmapViewerStatic.HasLayers)
                    {
                        BitmapViewer.Layer mapLayer = new BitmapViewer.Layer();
                        mapLayer.Order = 0;
                        mapLayer.IsVisible = true;
                        mapLayer.Map = solver.Map;
                        mapLayer.Name = "Puzzle";
                        bitmapViewerStatic.SetLayer(mapLayer);

                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.WallMap, new SolidBrush(Color.FromArgb(120, Color.Gray))).IsVisible = false;
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.FloorMap, new SolidBrush(Color.FromArgb(120, Color.Green))).IsVisible = false;
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.InitialCrateMap, new SolidBrush(Color.FromArgb(120, Color.Blue))).IsVisible = false;
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.DeadMap, new SolidBrush(Color.FromArgb(120, Color.Brown)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.BoundryMap, new SolidBrush(Color.FromArgb(120, Color.LightGray))).IsVisible = false;
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.GoalMap, new SolidBrush(Color.FromArgb(120, Color.Yellow))).IsVisible = false;
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.CornerMap, new SolidBrush(Color.FromArgb(120, Color.Pink)));
                        bitmapViewerStatic.SetLayer(solver.Strategy.StaticAnalysis.RecessMap, new SolidBrush(Color.FromArgb(120, Color.Cyan)));

                        BitmapViewer.Layer weightLayer = new BitmapViewer.Layer();
                        weightLayer.Order = 10;
                        weightLayer.IsVisible = true;
                        weightLayer.Matrix = solver.Strategy.StaticAnalysis.StaticForwardCrateWeighting;
                        weightLayer.Name = "Weightings";
                        weightLayer.Brush = new SolidBrush(Color.FromArgb(200, Color.Pink));
                        weightLayer.BrushAlt = new SolidBrush(Color.FromArgb(200, Color.Red));
                        weightLayer.Font = new Font("Arial Narrow", 7f);
                        bitmapViewerStatic.SetLayer(weightLayer);

                        bitmapViewerStatic.Render();
                    }


                    if (solver.Strategy.EvaluationTree != null)
                    {
                        treeViewer.Init(solver);
                        treeViewer.Render();
                    }

                    if (solver.ReverseStrategy != null && solver.ReverseStrategy.EvaluationTree != null)
                    {
                        TreeVisualisation tresVisRev = new TreeVisualisation(solver.ReverseStrategy.EvaluationTree, new RectangleInt(0, 0, 1800, 1800), new SizeInt(8,8));
                        visualisationContainerReverseTree.Visualisation = tresVisRev;
                        visualisationContainerReverseTree.Render();
                    }
                }

                // Stats
                if (solver != null && solver.Strategy != null)
                {
                    SolverLabelList txt = solver.Stats.GetDisplayData();
                    if (txt == null)
                    {
                        webBrowserGlobalStats.DocumentText = null;
                    }
                    else
                    {
                        webBrowserGlobalStats.DocumentText = txt.ToHTMLDocument();
                    }

                    UpdateEvalList();
                }
            }
        }

        private void UpdateEvalList()
        {
            return;

            List<INode<SolverNode>> evalList = solver.Strategy.EvaluationItterator.GetEvalList();
            if (evalList != null)
            {
                NodeListVisualisation vis = new NodeListVisualisation(
                    evalList.ConvertAll<SolverNode>(delegate(INode<SolverNode> item) { return item.Data; }),
                    new SizeInt(6, 6));
                visualisationContainerEvalList.Visualisation = vis;
                visualisationContainerEvalList.Render();
            }
        }

        /// <summary>
        /// Timer tick (update & monitor status)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            //UpdateStatus();
        }

        private void SolverSection_Disposed(object sender, EventArgs e)
        {
            if (worker != null)
            {
                StopWorker(worker);
            }
            worker = null;
            solver = null;
        }

        /// <summary>
        /// Exit click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e)
        {
            // Stop anything that has started
            tsbStop_Click(sender, e);

            if (worker != null)
            {
                StopWorker(worker);
            }

            solver = null;
            worker = null;

            // Set the library
            FormMain main = FindForm() as FormMain;
            main.Mode = FormMain.Modes.Library;
        }

        private void StopWorker(Thread thread)
        {
            if (solver != null) solver.State = SolverController.States.Cancelled;
            if (thread.IsAlive) thread.Join(1000);
            if (thread.IsAlive) thread.Abort();
        }

        /// <summary>
        /// Start solver 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStart_Click(object sender, EventArgs e)
        {
            bitmapViewerStatic.Clear();
            bitmapViewerStatic.MapSize = map.Map.Size;
            bitmapViewerNodeMaps.Clear();
            bitmapViewerNodeMaps.MapSize = map.Map.Size;

            if (!SolverActive)
            {
                worker = new Thread(new ThreadStart(Solve));
                timer.Enabled = true;
                worker.Start();
                tslStatus.Text = "Started";
            }
        }

        /// <summary>
        /// Stop/Abort the solver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStop_Click(object sender, EventArgs e)
        {
            if (solver != null) solver.State = SolverController.States.Cancelled;
            if (worker != null) worker.Join(5000);
            if (solver != null) UpdateStatus();
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


        private void OnVisualisationClick_LocalNode(object sender, VisEventArgs e)
        {
            OnVisualisationClick_TreeViewer(sender, e);
        }

        private void OnVisualisationClick_ReverseTree(object sender, VisEventArgs e)
        {
            OnVisualisationClick_TreeViewer(sender, e);
        }

        /// <summary>
        /// Select a node in the tree browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnVisualisationClick_TreeViewer(object sender, VisEventArgs e)
        {
            if (e.Element == null) return;

            SolverNode solverNode = null;

            TreeVisualisationElement element = e.Element as TreeVisualisationElement;
            if (element != null) solverNode = element.Data;

            RootPathElement rele = e.Element as RootPathElement;
            if (rele != null) solverNode = rele.Node;

            if (solverNode == null) return;


            SokobanMap build = BuildCurrentMap(solverNode);
            if (build != null)
            {
                BitmapViewer.Layer puzzleLayer = new BitmapViewer.Layer();
                puzzleLayer.Name = "Puzzle";
                puzzleLayer.Map = build;
                puzzleLayer.Order = 0;
                puzzleLayer.IsVisible = true;
                bitmapViewerNodeMaps.SetLayer(puzzleLayer);
            }


            if (solverNode.MoveMap != null)
            {
                SolverBitmap move = new SolverBitmap("MoveMap", solverNode.MoveMap);
                bitmapViewerNodeMaps.SetLayer(move, new SolidBrush(Color.FromArgb(120, Color.Green)));
            }

            if (solverNode.DeadMap != null)
            {
                bitmapViewerNodeMaps.SetLayer(solverNode.DeadMap, new SolidBrush(Color.FromArgb(120, Color.Black)));
            }


            // Build details
            SolverLabelList txt = solverNode.GetDisplayData();
           
            webBrowserNodeCurrent.DocumentText = txt.ToHTMLDocument();

            bitmapViewerNodeMaps.Render();

            if (sender != visualisationContainerLocalNodes)
            {
                RootPathVisualisation localVis = new RootPathVisualisation(new SizeInt(10, 10), solver);
                localVis.RenderCanvas =
                    new RectangleInt(0, 0, visualisationContainerLocalNodes.Width - 30,
                                     visualisationContainerLocalNodes.Height - 30);
                localVis.Init(solverNode);
                visualisationContainerLocalNodes.ClearImage();
                visualisationContainerLocalNodes.Visualisation = localVis;
                visualisationContainerLocalNodes.Render();
            }
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
                    if (solver.Strategy.StaticAnalysis.GoalMap[cx, cy])
                        result.setState(new VectorInt(cx, cy), Cell.Goal);
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
        }


        private void tsbRefreshVis_Click(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        #region Nested type: OnCompleteDelegate

        /// <summary>
        /// Target call-back for completion
        /// </summary>
        private delegate void OnCompleteDelegate();

        #endregion
    }
}