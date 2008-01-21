using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using SokoSolve.Common;
using SokoSolve.Core.Reporting;
using SokoSolve.UI.Controls.Secondary;

namespace SokoSolve.UI.Section.Solver
{
    /// <summary>
    /// Batched worker (without any visualisation) for the solver
    /// </summary>
    public partial class SolverSectionController : UserControl
    {
        #region Delegates

        public delegate void BindListViewItem<T>(ListViewItem item, T domainItem);

        #endregion

        #region States enum

        public enum States
        {
            Pending,
            Working,
            Complete
        }

        #endregion

        private bool attemptCancel = false;

        private DateTime complete;
        private WorkItem currentWorkItem = null;
        private SokoSolve.Core.Model.Library library;

        /// <summary>
        /// Then the user clicks Done.
        /// </summary>
        public event  EventHandler OnUserFinished;

        private Puzzle puzzle;
        private SolverController solver;
        private Thread solverThread;
        private DateTime started;
        private States status;
        private List<WorkItem> workerList;

        public SolverSectionController()
        {
            InitializeComponent();

            UpdateUI();

            Disposed += new EventHandler(SolverSectionController_Disposed);

            cbThreadPriority.Items.Clear();
            cbThreadPriority.Items.Add(ThreadPriority.Highest);
            cbThreadPriority.Items.Add(ThreadPriority.AboveNormal);
            cbThreadPriority.Items.Add(ThreadPriority.Normal);
            cbThreadPriority.Items.Add(ThreadPriority.BelowNormal);
            cbThreadPriority.Items.Add(ThreadPriority.Lowest);
            cbThreadPriority.SelectedIndex = 3;
        }

        void SolverSectionController_Disposed(object sender, EventArgs e)
        {
            if (solverThread != null && solverThread.IsAlive)
            {
                solverThread.Join(1000);

                if (solverThread.IsAlive) solverThread.Abort();
            }
        }

        /// <summary>
        /// Current solver
        /// </summary>
        [Browsable(false)]
        public SolverController Solver
        {
            get { return solver; }
        }

        /// <summary>
        /// Current solver thread
        /// </summary>
        [Browsable(false)]
        public Thread SolverThread
        {
            get { return solverThread; }
        }

        /// <summary>
        /// Current Library
        /// </summary>
        [Browsable(false)]
        public Core.Model.Library Library
        {
            get { return library; }
            set
            {
                library = value;
                ucPuzzleList1.Library = value;
                ucPuzzleList1.Bind();
                UpdateUI();
            }
        }

        /// <summary>
        /// Current (selected inbound) puzzle
        /// </summary>
        [Browsable(false)]
        public Puzzle Puzzle
        {
            get { return puzzle; }
            set
            {
                puzzle = value;
                ucPuzzleList1.Select(puzzle);
            }
        }

        /// <summary>
        /// States
        /// </summary>
        [Browsable(false)]
        public States Status
        {
            get { return status; }
        }

        /// <summary>
        /// Worker List complete percentage
        /// </summary>
        /// <returns></returns>
        private int CountPercentageComplete()
        {
            if (workerList == null || workerList.Count == 0) return 0;
            int count = 0;
            foreach (WorkItem item in workerList)
            {
                if (item.Result != null) count++;
            }
            return count*100/workerList.Count;
        }

        /// <summary>
        /// Bind to the UI controls
        /// </summary>
        private void UpdateUI()
        {
            switch (Status)
            {
                case (States.Working):
                    tsbStart.Enabled = false;
                    tsbStop.Enabled = true;
                    tsbSaveHTML.Enabled = false;

                    tsbSaveXML.Enabled = false;
                    listViewResults.Enabled = true;
                    ucPuzzleList1.Enabled = false;

                    tsgProgress.Visible = true;

                    tslStatus.Text = "Working";
                    tslTime.Text = StringHelper.ToString(DateTime.Now.Subtract(started));
                    tsgProgress.Enabled = true;
                    tsgProgress.Value = CountPercentageComplete();


                    BindResults();
                    return;

                case (States.Complete):
                    tsbStart.Enabled = true;
                    tsbStop.Enabled = false;
                    tsbSaveHTML.Enabled = true;

                    //tsbSaveXML.Enabled = true;
                    listViewResults.Enabled = true;
                    ucPuzzleList1.Enabled = true;

                    tsgProgress.Visible = false;

                    tslStatus.Text = "Completed";
                    tslTime.Text = StringHelper.ToString(complete.Subtract(started));
                    tsgProgress.Enabled = true;
                    tsgProgress.Value = CountPercentageComplete();

                    BindResults();
                    return;

                case (States.Pending):

                default:
                    tsbStart.Enabled = true;
                    tsbStop.Enabled = false;
                    tsbSaveHTML.Enabled = false;
                    tsbSaveXML.Enabled = false;
                    listViewResults.Enabled = false;
                    ucPuzzleList1.Enabled = true;
                    tslStatus.Text = "Pending";
                    tsgProgress.Enabled = false;
                    return;
            }
        }

        public static void BindListView<T>(ListView target, List<T> source, BindListViewItem<T> binder)
        {
            target.BeginUpdate();

            if (source == null || source.Count == 0)
            {
                target.Items.Clear();
            }
            else
            {
                // Remove deleted items
                List<ListViewItem> removeList = new List<ListViewItem>();
                foreach (ListViewItem targetItem in target.Items)
                {
                    bool isFound = false;
                    foreach (T domainItem in source)
                    {
                        if (domainItem.Equals(targetItem.Tag))
                        {
                            isFound = true;
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        // Delete
                        removeList.Add(targetItem);
                    }
                }

                foreach (ListViewItem remove in removeList)
                {
                    target.Items.Remove(remove);
                }


                // Add New
                foreach (T domainItem in source)
                {
                    bool isFound = false;
                    foreach (ListViewItem targetItem in target.Items)
                    {
                        if (domainItem.Equals(targetItem.Tag))
                        {
                            isFound = true;
                            // Update
                            binder(targetItem, domainItem);
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        // New
                        ListViewItem item = new ListViewItem();
                        item.Tag = domainItem;

                        // All all columns
                        for (int cc = 0; cc < target.Columns.Count - 1; cc++)
                        {
                            item.SubItems.Add("");
                        }

                        // Bind
                        binder(item, domainItem);

                        target.Items.Add(item);
                    }
                }
            }

            foreach (ColumnHeader col in target.Columns)
            {
                col.Width = -2;
            }


            target.EndUpdate();
        }

        private void BindResultsItem(ListViewItem target, WorkItem workItem)
        {
            if (workItem.Map != null && workItem.Map.Puzzle != null && workItem.Map.Puzzle.Details != null)
            {
                target.Text = workItem.Map.Puzzle.Details.Name;
            }
            else
            {
                target.Text = "NoName";
            }

            target.Group = listViewResults.Groups[3];
            if (workItem.Result != null)
            {
                target.SubItems[1].Text = workItem.Result.StatusString;
                target.SubItems[2].Text = workItem.Result.Summary;

                switch(workItem.Result.Status)
                {
                    case(SolverResult.CalculationResult.Cancelled):
                    case (SolverResult.CalculationResult.Error):
                        target.ImageIndex = 3; // red cross icon
                        target.Group = listViewResults.Groups[2]; // Failed
                        break;

                    case (SolverResult.CalculationResult.GaveUp):
                        target.ImageIndex = 3; // red cross icon
                        target.Group = listViewResults.Groups[1]; // Failed
                        break;

                    case (SolverResult.CalculationResult.SolutionFound):
                    case (SolverResult.CalculationResult.NoSolution):
                        target.ImageIndex = 4; // Tick icon 
                        target.Group = listViewResults.Groups[0]; // Solved
                        break;

                    default:
                        target.Group = listViewResults.Groups[3];
                        break;
                }
            }
            else
            {
                target.Group = listViewResults.Groups[3]; // Pending

                if (currentWorkItem == workItem)
                {
                    // Current
                    target.ImageIndex = 5; // play icon

                    target.SubItems[1].Text = "Working";
                    target.SubItems[2].Text = BuildWorkingSummary(currentWorkItem.Controller.Stats);
                }
                else
                {
                    // Pending
                }
            }
        }

        private string BuildWorkingSummary(SolverStats stats)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0:0} sec", stats.CurrentEvalSecs.ValueTotal);
            sb.AppendFormat(", {0:0} nodes", stats.Nodes.ValueTotal);
            sb.AppendFormat(" at {0:0.0} nodes/sec", stats.NodesPerSecond.ValuePerSec);
            return sb.ToString();
        }

        /// <summary>
        /// Bind results to UI
        /// </summary>
        private void BindResults()
        {
            BindListView<WorkItem>(listViewResults, workerList, BindResultsItem);
        }

        /// <summary>
        /// Done button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDone_Click(object sender, EventArgs e)
        {
            if (OnUserFinished != null)
            {
                OnUserFinished(sender, e);
            }
            else
            {
                // If no event is set, try the main form and return
                FormMain main = FindForm() as FormMain;
                if (main != null)
                {
                    main.Mode = FormMain.Modes.Library;
                }
            }
        }

        /// <summary>
        /// Start Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStart_Click(object sender, EventArgs e)
        {
            started = DateTime.Now;
            status = States.Working;
            timerUpdater.Enabled = true;
            UpdateUI();
            tabControl1.SelectedTab = tabPageResults;

            workerList = new List<WorkItem>();
            foreach (PuzzleMap map in ucPuzzleList1.SelectedPuzzleMaps)
            {
                WorkItem item = new WorkItem();
                item.Map = map;
                workerList.Add(item);
            }

            solverThread = new Thread(new ThreadStart(ProcessWorkerList));
            solverThread.Name = "SolverBatcher";
            solverThread.Priority = (ThreadPriority)cbThreadPriority.SelectedItem;
            solverThread.Start();
        }

        /// <summary>
        /// Worker Method. Do the actual work.
        /// </summary>
        private void ProcessWorkerList()
        {
            foreach (WorkItem workItem in workerList)
            {
                currentWorkItem = workItem;

                if (attemptCancel) break;

                try
                {
                    workItem.Controller = new SolverController(workItem.Map);
                    workItem.Controller.ExitConditions.StopOnSolution = exitConditions1.cbStopOnSolution.Checked;
                    workItem.Controller.ExitConditions.MaxDepth = (int) exitConditions1.upMaxDepth.Value;
                    workItem.Controller.ExitConditions.MaxNodes = (int) exitConditions1.upMaxNodes.Value;
                    workItem.Controller.ExitConditions.MaxItterations = (int) exitConditions1.upMaxItter.Value;
                    workItem.Controller.ExitConditions.MaxTimeSecs = (int) (exitConditions1.upMaxTime.Value*60);
                    this.Invoke(new SimpleDelegate(ProcessWorkerListItemUpdate));
                    workItem.Result = workItem.Controller.Solve();
                    this.Invoke(new SimpleDelegate(ProcessWorkerListItemUpdate));
                }
                catch (Exception ex)
                {
                    // Failed, record it and move to the next item.
                    workItem.Exception = ex;
                }
            }

            // Move any solutions into library
            AddSolutions();

            currentWorkItem = null;
            complete = DateTime.Now;

            // Complete, call back
            this.Invoke(new SimpleDelegate(ProcessWorkerListComplete));
        }

        /// <summary>
        /// Add any solutions
        /// </summary>
        private void AddSolutions()
        {
            if (rbDontAdd.Checked) return;

            foreach (WorkItem item in workerList)
            {
                if (item == null) continue;
                if (item.Result == null) continue; 

                if (item.Result.HasSolution)
                {
                    if (rbAddBetter.Checked)
                    {
                        if (item.Map.HasSolution)
                        {
                            Solution bestNew = Solution.FindBest(item.Result.Solutions);
                            Solution bestExisting = Solution.FindBest(item.Map.Solutions);
                            if (bestNew != null && bestExisting != null && bestExisting.CompareTo(bestNew) < 0)
                            {
                                item.Map.Solutions.Add(bestNew);
                            }    
                        }
                        else
                        {
                            Solution bestNew = Solution.FindBest(item.Result.Solutions);
                            item.Map.Solutions.Add(bestNew);
                        }
                    }
                    else if (rbAlwaysAdd.Checked)
                    {
                        foreach (Solution sol in item.Result.Solutions)
                        {
                            item.Map.Solutions.Add(sol);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when an item in the worklist if compelted
        /// </summary>
        private void ProcessWorkerListItemUpdate()
        {
            try
            {
                UpdateUI();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// When the worker is complete
        /// </summary>
        private void ProcessWorkerListComplete()
        {
            status = States.Complete;
            solverThread = null;
            timerUpdater.Enabled = false;
            try
            {
                UpdateUI();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Stop button was clicked stop and cleanup threads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStop_Click(object sender, EventArgs e)
        {
            tslStatus.Text = "Stopping...";
            if (solverThread != null && solverThread.IsAlive)
            {
                attemptCancel = true;
                if (currentWorkItem != null)
                {
                    currentWorkItem.Controller.Cancel();
                }

                // Join, waiting for 1 sec
                solverThread.Join(2000);
                if (solverThread.IsAlive) solverThread.Abort();
                solverThread = null;
            }

            attemptCancel = false;
            status = States.Complete;
            timerUpdater.Enabled = false;
            UpdateUI();
        }

        private void timerUpdater_Tick(object sender, EventArgs e)
        {
            UpdateUI();

            if (status == States.Working)
            {
                BindResults();
            }
        }

        #region Nested type: SimpleDelegate

        private delegate void SimpleDelegate();

        #endregion

        #region Nested type: WorkItem

        private class WorkItem
        {
            private SolverController controller;
            private Exception exception;
            private PuzzleMap map;
            private SolverResult result;

            public PuzzleMap Map
            {
                get { return map; }
                set { map = value; }
            }

            public SolverController Controller
            {
                get { return controller; }
                set { controller = value; }
            }

            public SolverResult Result
            {
                get { return result; }
                set { result = value; }
            }

            public Exception Exception
            {
                get { return exception; }
                set { exception = value; }
            }
        }

        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (currentWorkItem != null)
            {
                FormSolverVisualisation formVis = new FormSolverVisualisation();
                formVis.Controller = currentWorkItem.Controller;
                formVis.Show();    
            }
            
        }

        private void tsbSaveHTML_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                List<SolverResult> results = workerList.ConvertAll<SolverResult>(delegate(WorkItem item) { return item.Result; });
                SolverResultHTML report = new SolverResultHTML(results);
                report.BuildReport();
                report.Save(save.FileName);

                Process.Start(save.FileName);
            }
        }

        private void tsbSelectAll_Click(object sender, EventArgs e)
        {
            ucPuzzleList1.SelectAll();
        }

        private void listViewResults_DoubleClick(object sender, EventArgs e)
        {
            if (listViewResults.SelectedItems.Count > 0)
            {
                WorkItem item = listViewResults.SelectedItems[0].Tag as WorkItem;
                if (item != null)
                {
                    if (item.Exception != null)
                    {
                        FormError error = new FormError();
                        error.Exception = item.Exception;
                        error.ShowDialog();
                        return;
                    }

                    if (item.Result != null)
                    {
                        List<SolverResult> results = new List<SolverResult>();
                        results.Add(item.Result);
                        SolverResultHTML report = new SolverResultHTML(results);
                        report.BuildReport();

                        FormBrowser browser = new FormBrowser();
                        browser.SetHTML(report.ToString());
                        browser.ShowDialog();
                    }
                }
            }
        }
    }
}
