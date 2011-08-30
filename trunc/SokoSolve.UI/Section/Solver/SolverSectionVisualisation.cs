using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.Reporting;
using SokoSolve.Core.UI;
using SokoSolve.UI.Controls.Secondary;
using Path=SokoSolve.Common.Structures.Path;
using NPlot;

namespace SokoSolve.UI.Section.Solver
{
    /// <summary>
    /// Master control for the Solver Block.
    /// </summary>
    public partial class SolverSectionVisualisation : UserControl
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SolverSectionVisualisation()
        {
            InitializeComponent();

            treeViewer.OnVisualisationClick += new EventHandler<VisEventArgs>(OnVisualisationClick_TreeViewer);
            visualisationContainerLocalNodes.OnVisualisationClick +=
                new EventHandler<VisEventArgs>(OnVisualisationClick_LocalNode);
            visualisationContainerReverseTree.OnVisualisationClick +=
                new EventHandler<VisEventArgs>(OnVisualisationClick_ReverseTree);       
        }


        /// <summary>
        /// The solver controller which will be visualised
        /// </summary>
        [Browsable(false)]
        public SolverController Controller
        {
            get { return controller; }
            set
            {
                controller = value;
                if (controller != null)
                {
                    bitmapViewerStatic.MapSize = Map.Map.Size;
                    bitmapViewerNodeMaps.MapSize = Map.Map.Size;

                    if (Map != null)
                    {
                        pictureBoxStaticImage.Image = DrawingHelper.DrawPuzzle(Map);
                    }
                }
                inlineBrowserSolver.NavigateIncludedContent("$html\\Solver.html");
            }
        }

        /// <summary>
        /// The current map for solving
        /// </summary>
        public PuzzleMap Map
        {
            get
            {
                if (controller == null) return null;
                return controller.PuzzleMap;
            }
        }

        /// <summary>
        /// Status Helper, is the solver thread running?
        /// </summary>
        private bool SolverActive
        {
            get { return controller.State == SolverController.States.Running; }
        }


        /// <summary>
        /// Write a log report to the $Content/Analysis directory
        /// </summary>
        private void BuildSolverLogReport()
        {
            if (!Directory.Exists(FileManager.GetContent("$Analysis"))) return;

            // Build report
            SolverReport report = new SolverReport();
            report.AppendHeadingTwo("Puzzle");
            report.AppendLabel("Library", controller.PuzzleMap.Puzzle.Library.Details.Name);
            report.AppendLabel("LibraryID", controller.PuzzleMap.Puzzle.Library.LibraryID);
            report.AppendLabel("Puzzle", controller.PuzzleMap.Puzzle.Details.Name);
            report.AppendLabel("PuzzleID", controller.PuzzleMap.Puzzle.PuzzleID);
            report.AppendHeadingTwo("Solver");
            report.Build(controller);

            // Save
            string filename = "SolverReport-" + controller.PuzzleMap.Puzzle.Library.LibraryID +
                              controller.PuzzleMap.Puzzle.PuzzleID + "-" + DateTime.Now.Ticks.ToString() + ".html";
            File.WriteAllText(FileManager.GetContent("$Analysis", filename), report.ToString());
        }

        /// <summary>
        /// Update (replot) the graphs
        /// </summary>
        public void PlotGraphs()
        {
            if (surface == null)
            {
                surface = new NPlot.Windows.PlotSurface2D();
                tabPageGraphs.Controls.Add(surface);
                surface.Dock = DockStyle.Fill;

                surface2 = new NPlot.Windows.PlotSurface2D();
                surface2.Height = 150;
                tabPageGraphs.Controls.Add(surface2);
                surface2.Dock = DockStyle.Bottom;                
            }

            surface.Clear();
            
            //Add a background grid for better chart readability.
            Grid grid = new Grid();
            grid.VerticalGridType = Grid.GridType.Coarse;
            grid.HorizontalGridType = Grid.GridType.Coarse;
            grid.MajorGridPen = new Pen(Color.LightGray, 1.0f);
            surface.Add(grid);

            LinePlot lp1 = new LinePlot();
            lp1.DataSource = controller.Stats.EvaluationItterations.History;
            lp1.Color = Color.Black;
            lp1.Label = "Itterations";
            surface.Add(lp1);

            LinePlot lp2 = new LinePlot();
            lp2.DataSource = controller.Stats.Nodes.History;
            lp2.Pen = new Pen(Color.Green, 2.0f);
            lp2.Label = "Nodes";
            surface.Add(lp2);

            LinePlot lp3 = new LinePlot();
            lp3.DataSource = controller.Stats.Duplicates.History;
            lp3.Pen = new Pen(Color.Orange, 1.0f);
            lp3.Label = "Duplicates";
            surface.Add(lp3);

            LinePlot lp4 = new LinePlot();
            lp4.DataSource = controller.Stats.DeadNodes.History;
            lp4.Pen = new Pen(Color.Red, 1.5f);
            lp4.Label = "Dead";
            surface.Add(lp4);

            LinePlot lp6 = new LinePlot();
            lp6.DataSource = controller.Stats.AvgEvalList.History;
            lp6.Pen = new Pen(Color.Brown, 1.3f);
            lp6.Label = "Eval List";
            surface.Add(lp6);

            LinePlot lp7 = new LinePlot();
            lp7.DataSource = controller.Stats.NodesFwd.History;
            lp7.Pen = new Pen(Color.Brown, 1.0f);
            lp7.Pen.DashStyle = DashStyle.Dash;
            lp7.Label = "Nodes Fwd";
            surface.Add(lp7);

            LinePlot lp8 = new LinePlot();
            lp8.DataSource = controller.Stats.NodesRev.History;
            lp8.Pen = new Pen(Color.Cyan, 1.0f);
            lp8.Pen.DashStyle = DashStyle.Dash;
            lp8.Label = "Nodes Rev";
            surface.Add(lp8);

            surface.XAxis1.Label = "Elapsed Time (sec)";
            surface.YAxis1.Label = "Total";            
            surface.Legend = new Legend();

            surface.SmoothingMode = SmoothingMode.HighQuality;
            surface.Title = "Solver | Node Information";
            
            surface.Refresh();

            surface2.Clear();
            StepPlot lp5 = new StepPlot();
            lp5.Pen = new Pen(Color.Fuchsia, 2f);
            lp5.DataSource = controller.Stats.NodesPerSecond.History;
            lp5.Label = "Nodes/s";
            surface2.Add(lp5);

            surface2.XAxis1.Label = "Elapsed Time (sec)";
            surface2.YAxis1.Label = "Rate (node/sec)";
            surface2.Legend = new Legend();
            surface2.SmoothingMode = SmoothingMode.HighQuality;
            surface2.Refresh();
        }

        /// <summary>
        /// Update or Bind to the UI
        /// </summary>
        private void UpdateStatus()
        {
            // Clear existing report text
            richTextBoxSolverReport.Clear();

            if (controller != null && controller.Strategy != null && controller.StaticAnalysis != null)
            {
                // Draw the graphs
                PlotGraphs();

                // Show debug text
                richTextBoxSolverReport.Text = controller.DebugReport.ToString(new DebugReportFormatter());

                // Static bitmaps
                if (!bitmapViewerStatic.HasLayers)
                {
                    SokobanMap build = BuildCurrentMap(controller.Strategy.EvaluationTree.Root.Data);
                    BitmapViewer.Layer mapLayer = new BitmapViewer.Layer();
                    mapLayer.Order = 0;
                    mapLayer.Map = build;
                    mapLayer.Name = "Puzzle";
                    bitmapViewerStatic.SetLayer(mapLayer);
                    mapLayer.IsVisible = true;

                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.WallMap, new SolidBrush(Color.FromArgb(120, Color.Gray)));
                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.FloorMap, new SolidBrush(Color.FromArgb(120, Color.Green)));
                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.InitialCrateMap, new SolidBrush(Color.FromArgb(120, Color.Blue)));
                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.DeadMap, new SolidBrush(Color.FromArgb(120, Color.Brown))).IsVisible = true;
                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.BoundryMap, new SolidBrush(Color.FromArgb(120, Color.LightGray)));
                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.GoalMap, new SolidBrush(Color.FromArgb(120, Color.Yellow)));
                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.CornerMap, new SolidBrush(Color.FromArgb(120, Color.Pink)));
                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.RecessMap, new SolidBrush(Color.FromArgb(120, Color.Cyan)));

                    bitmapViewerStatic.SetLayer(controller.StaticAnalysis.RoomAnalysis.DoorBitmap, new SolidBrush(Color.FromArgb(120, Color.Khaki)));
                    foreach (Room room in controller.StaticAnalysis.RoomAnalysis.Rooms)
                    {
                        bitmapViewerStatic.SetLayer(room, new SolidBrush(Color.FromArgb(120, Color.LightCyan)));    
                    }

                    BitmapViewer.Layer weightLayer = new BitmapViewer.Layer();
                    weightLayer.Order = 10;
                    weightLayer.Matrix = controller.StaticAnalysis.StaticForwardCrateWeighting;
                    weightLayer.Name = "Weightings";
                    weightLayer.Brush = new SolidBrush(Color.FromArgb(200, Color.Pink));
                    weightLayer.BrushAlt = new SolidBrush(Color.FromArgb(200, Color.Red));
                    weightLayer.Font = new Font("Arial Narrow", 7f);
                    bitmapViewerStatic.SetLayer(weightLayer);


                    bitmapViewerStatic.Render();
                }

                // Forward Tree
                if (controller.Strategy.EvaluationTree != null)
                {
                    treeViewer.Init(controller);
                    treeViewer.Controller = controller;
                    treeViewer.Render();
                }

                // Reverse Tree
                if (controller.ReverseStrategy != null && controller.ReverseStrategy.EvaluationTree != null)
                {
                    TreeVisualisation tresVisRev = new TreeVisualisation(controller.ReverseStrategy.EvaluationTree,
                                              new RectangleInt(0, 0, 1800, 1800), new SizeInt(8, 8));
                    tresVisRev.Controller = controller;
                    visualisationContainerReverseTree.Visualisation = tresVisRev;
                    visualisationContainerReverseTree.Render();
                }

                // Stats
                SolverLabelList txt = controller.Stats.GetDisplayData();
                if (txt != null)
                {
                    ReportXHTML reportCurrent = new ReportXHTML("Node Details");
                    reportCurrent.SetCSSInline(FileManager.GetContent("$html\\style.css"));
                    reportCurrent.Add(txt.ToHTML());
                    htmlViewStats.SetHTML(reportCurrent.ToString());
                }

                UpdateEvalList();
            }
        }

        /// <summary>
        /// Update the evaluation list
        /// </summary>
        private void UpdateEvalList()
        {
            return;

            List<INode<SolverNode>> evalList = controller.Strategy.EvaluationItterator.GetEvalList();

            List<SolverNode> deepClone = new List<SolverNode>(evalList.Count);
            foreach (INode<SolverNode> node in evalList)
            {
                deepClone.Add(new SolverNode(node.Data));
            }

            if (deepClone.Count > 0)
            {
                NodeListVisualisation vis = new NodeListVisualisation(controller, deepClone, new SizeInt(6, 6));
                visualisationContainerEvalList.Visualisation = vis;
                visualisationContainerEvalList.Render();
            }
        }


        private void OnVisualisationClick_LocalNode(object sender, VisEventArgs e)
        {
            if (e.Mouse.Clicks > 1)
            {
                OnVisualisationClick_TreeViewer(sender, e);
            }
            else
            {
                OnVisualisationClick_TreeViewer(sender, e);
            }
        }

        private void OnVisualisationClick_ReverseTree(object sender, VisEventArgs e)
        {
            OnVisualisationClick_TreeViewer(sender, e);
        }

        /// <summary>
        /// Select a SPECIFIC node and display its details
        /// </summary>
        /// <param name="solverNode"></param>
        private void BindNode(SolverNode solverNode)
        {
            if (solverNode == null) return;

            SokobanMap build = BuildCurrentMap(solverNode);
            if (build != null)
            {
                BitmapViewer.Layer puzzleLayer = new BitmapViewer.Layer();
                puzzleLayer.Name = "Puzzle";
                puzzleLayer.Map = build;
                puzzleLayer.Order = 0;
                puzzleLayer.IsVisible = true;
                bitmapViewerNodeMaps.SetLayer(puzzleLayer).IsVisible = true;
            }


            if (solverNode.MoveMap != null)
            {
                SolverBitmap move = new SolverBitmap("MoveMap", solverNode.MoveMap);
                bitmapViewerNodeMaps.SetLayer(move, new SolidBrush(Color.FromArgb(120, Color.Green))).IsVisible = true;
            }

            if (solverNode.DeadMap != null)
            {
                bitmapViewerNodeMaps.SetLayer(solverNode.DeadMap, new SolidBrush(Color.FromArgb(120, Color.Black))).IsVisible = true;
            }


            // Build details
            SolverLabelList txt = solverNode.GetDisplayData();

            ReportXHTML reportCurrent = new ReportXHTML("Node Details");
            reportCurrent.SetCSSInline(FileManager.GetContent("$html\\style.css"));
            reportCurrent.Add(txt.ToHTML());
            if (build != null)
            {
                reportCurrent.Add(string.Format("<code><pre>{0}</pre></code>", build.ToString()));
            }
            webBrowserNodeCurrent.DocumentText = reportCurrent.ToString();

            bitmapViewerNodeMaps.Render();

            RootPathVisualisation localVis = new RootPathVisualisation(new SizeInt(16, 16), controller);
            localVis.RenderCanvas =
                new RectangleInt(0, 0, visualisationContainerLocalNodes.Width - 30,
                                 visualisationContainerLocalNodes.Height - 30);
            localVis.Init(solverNode);
            visualisationContainerLocalNodes.ClearImage();
            visualisationContainerLocalNodes.Visualisation = localVis;
            visualisationContainerLocalNodes.Render();
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

            BindNode(solverNode);
        }

        private SokobanMap BuildCurrentMap(SolverNode node)
        {
            SokobanMap result = new SokobanMap();
            result.Init(node.CrateMap.Size);

            for (int cx = 0; cx < result.Size.Width; cx++)
                for (int cy = 0; cy < result.Size.Height; cy++)
                {
                    if (controller.StaticAnalysis.WallMap[cx, cy]) result[cx, cy] = CellStates.Wall;
                    if (controller.StaticAnalysis.FloorMap[cx, cy]) result[cx, cy] = CellStates.Floor;
                    if (controller.StaticAnalysis.GoalMap[cx, cy])
                        result.SetState(new VectorInt(cx, cy), Cell.Goal);
                    if (node.CrateMap[cx, cy]) result.SetState(new VectorInt(cx, cy), Cell.Crate);
                    result.SetState(node.PlayerPosition, Cell.Player);
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


       

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            UpdateStatus();
        }


        private void htmlViewStats_OnCommand(object sender, SokoSolve.UI.Controls.Web.UIBrowserEvent e)
        {
            string NodeID = e.Command.ToString().Remove(0, "app://node/".Length);
            e.Completed = true;
            SelectNode(NodeID);
        }

        private void SelectNode(string nodeID)
        {
            SolverNode node =  controller.FindNode(nodeID);
            if (node != null)
            {
                BindNode(node);
            }

        }

        private NPlot.Windows.PlotSurface2D surface;
        private NPlot.Windows.PlotSurface2D surface2;
        private SolverController controller;
    }
}