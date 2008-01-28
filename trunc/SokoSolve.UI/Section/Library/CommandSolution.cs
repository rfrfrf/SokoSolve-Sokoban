using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Game;
using SokoSolve.Core.Model;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Section.Library
{
    //#################################################################
    //#################################################################
    //#################################################################

    class SolutionNew : CommandLibraryBase
    {
        public SolutionNew(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("New Solution", "Create a new blank solution");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemPuzzle itemPuzzle = instance.Context[0] as ItemPuzzle;
            if (itemPuzzle != null)
            {
                Solution newSol = new Solution(itemPuzzle.DomainData.MasterMap, itemPuzzle.DomainData.MasterMap.Map.Player);
                newSol.Details = ProfileController.Current.GenericDescription;
                newSol.Details.Name = string.Format("Manual solution for {0}", newSol.Map.Puzzle.Details.Name);
                newSol.Steps = "";

                itemPuzzle.DomainData.MasterMap.Solutions.Add(newSol);

                // Sync UI
                itemPuzzle.SyncDomain();

                // Refresh enture tree
                Controller.Explorer.SyncUI();
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemPuzzle));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class SolutionReplay : CommandLibraryBase
    {
        public SolutionReplay(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Replay", "Animate a replay of the solution");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemSolution itemSolution = instance.Context[0] as ItemSolution;
            if (itemSolution != null)
            {
                FormMain form = Controller.Explorer.TreeView.FindForm() as FormMain;
                if (form != null)
                {
                    Puzzle puz = itemSolution.DomainData.Map.Puzzle;
                    PuzzleMap puzMap = itemSolution.DomainData.Map;
                    form.StartGameSolution(puz, puzMap, itemSolution.DomainData, FormMain.Modes.Library);
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemSolution));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class SolutionTest : CommandLibraryBase
    {
        public SolutionTest(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Test Solution", "Check a solution against its puzzle to see if it is a valid solution.");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemSolution itemSolution = instance.Context[0] as ItemSolution;
            if (itemSolution != null)
            {
                FormMain form = Controller.Explorer.TreeView.FindForm() as FormMain;
                if (form != null)
                {
                    Puzzle puz = itemSolution.DomainData.Map.Puzzle;
                    PuzzleMap puzMap = itemSolution.DomainData.Map;
                    Game coreGame = new Game(puz, puzMap.Map);

                    string firstError = "";
                    if (coreGame.Test(itemSolution.DomainData, out firstError))
                    {
                        MessageBox.Show("Solution is valid");
                    }
                    else
                    {
                        MessageBox.Show("Solution is NOT valid. " + firstError);
                    }
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemSolution));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class SolutionDelete : CommandLibraryBase
    {
        public SolutionDelete(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Delete Solution", "Delete an existing solution and related content");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemSolution delMe = instance.Context[0] as ItemSolution;
            if (delMe != null)
            {
                // Find parent, then remove current
                TreeNode<ExplorerItem> parent = delMe.TreeNode.Parent;
                ItemPuzzle catParent = parent.Data as ItemPuzzle;
                if (catParent != null)
                {
                    catParent.DomainData.MasterMap.Solutions.Remove(delMe.DomainData);

                    // Refresh the UI model to updated domain data
                    catParent.SyncDomain();
                }

                // Set selection as parent
                Controller.UpdateSelectionSingle(catParent);

                // Refresh enture tree
                Controller.Explorer.SyncUI();
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemSolution));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class SolutionEdit : CommandLibraryBase
    {
        public SolutionEdit(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Edit Solution", "Edit solution properties");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            if (!instance.hasSelection) return;

            ExplorerItem item = instance.Context[0];

            if (instance.Status == ExecutionStatus.Working)
            {

                item.IsEditable = true;
                Control implUI = item.ShowDetail();

                // Wait for call-back
                instance.Status = ExecutionStatus.AwaitingCallback;

                // Register for the call-bak events
                PayloadSolutionEdit edit = implUI as PayloadSolutionEdit;
                if (edit != null)
                {
                    edit.tbSolutions.Text = (instance.Context[0].DataUnTyped as Solution).Steps;

                    ucGenericDescription details = edit.ucGenericDescription;
                    if (details != null)
                    {
                        details.Tag = instance;
                        details.ButtonOK.Tag = instance;
                        details.ButtonOK.Click += new EventHandler(ButtonOK_Click);
                        details.ButtonCancel.Tag = instance;
                        details.ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
                    }    
                }
                
                return;
            }

            if (instance.Status == ExecutionStatus.AwaitingCallback)
            {
                if (instance.Param != null && instance.Param.ToString() == "Ok")
                {
                    PayloadSolutionEdit editControl = item.ShowDetail() as PayloadSolutionEdit;
                    if (editControl == null) throw new InvalidCastException("Detail should be PayloadSolutionEdit");
                    
                    // Replace details
                    Core.Model.Solution solution = (Core.Model.Solution)item.DataUnTyped;
                    solution.Details = editControl.ucGenericDescription.Data;
                    solution.Steps = editControl.tbSolutions.Text;

                    editControl.ucGenericDescription.ButtonOK.Click -= new EventHandler(ButtonOK_Click);
                    editControl.ucGenericDescription.ButtonCancel.Click -= new EventHandler(ButtonOK_Click);

                    item.IsEditable = false;
                    item.ShowDetail();
                }

                if (instance.Param != null && instance.Param.ToString() == "Cancel")
                {
                    item.IsEditable = false;
                    item.ShowDetail();
                    return;
                }
            }
        }

        void ButtonCancel_Click(object sender, EventArgs e)
        {
            Control senderCtrl = sender as Control;
            if (senderCtrl != null && senderCtrl.Tag != null)
            {
                CommandInstance<ExplorerItem> cmdInst = senderCtrl.Tag as CommandInstance<ExplorerItem>;
                if (cmdInst != null)
                {
                    cmdInst.Param = "Cancel";

                    // Perform call-back
                    cmdInst.Command.Execute(cmdInst);
                }
            }
        }

        void ButtonOK_Click(object sender, EventArgs e)
        {
            Control senderCtrl = sender as Control;
            if (senderCtrl != null && senderCtrl.Tag != null)
            {
                CommandInstance<ExplorerItem> cmdInst = senderCtrl.Tag as CommandInstance<ExplorerItem>;
                if (cmdInst != null)
                {
                    cmdInst.Param = "Ok";

                    // Perform call-back
                    cmdInst.Command.Execute(cmdInst);
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemSolution));
        }


    }

    //#################################################################
    //#################################################################
    //#################################################################

}
