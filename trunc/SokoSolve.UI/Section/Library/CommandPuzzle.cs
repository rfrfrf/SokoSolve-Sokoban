using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Section.Library
{
    //#################################################################
    //#################################################################
    //#################################################################


    class PuzzleNew : CommandLibraryBase
    {
        public PuzzleNew(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("New Puzzle", "Create a new blank puzzle map");
        }


        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemCategory category = instance.Context[0] as ItemCategory;
            if (category != null)
            {
                // Create a new puzzle with a valid number etc.
                Puzzle newPuz = new Puzzle(Controller.Current);
                newPuz.PuzzleID = Controller.Current.IdProvider.GetNextIDString("P{0}");
                newPuz.Details = new GenericDescription();
                newPuz.Details.Name = string.Format("New Puzzle #{0}", Controller.Current.Puzzles.Count+1);
                newPuz.Category = category.Data;
                newPuz.MasterMap = new PuzzleMap(newPuz);
                newPuz.MasterMap.MapID = Controller.Current.IdProvider.GetNextIDString("M{0}");

                Controller.Current.Puzzles.Add(newPuz);

                // Refresh the UI model to updated domain data
                category.SyncWithData();

                // Refresh enture tree
                Controller.Explorer.BindUI();
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemCategory));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################


    class PuzzleEdit : CommandLibraryBase
    {
        public PuzzleEdit(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Edit Puzzle", "Edit a puzzle map");
        }


        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            if (!instance.hasSelection) return;

            ExplorerItem item = instance.Context[0];

            if (instance.Status == ExecutionStatus.Working)
            {
                item.IsEditable = !item.IsEditable;
                Control implUI = item.ShowDetail();

                if (item.IsEditable)
                {
                    // Wait for call-back
                    instance.Status = ExecutionStatus.AwaitingCallback;

                    // Register for the call-bak events
                    Editor editor = implUI as Editor;
                    if (editor != null)
                    {
                        editor.Tag = instance;
                        editor.ClearCommandsEvents();
                        editor.Commands += new Editor.SimpleCommand(editor_Commands);
                        editor.Map = new SokobanMap((item.DataUnTyped as Puzzle).MasterMap.Map);
                    }
                }
                return;
            }

            if (instance.Status == ExecutionStatus.AwaitingCallback)
            {
                if (instance.Param != null && instance.Param.ToString() == "Editor.Save")
                {
                    Editor editor = item.ShowDetail() as Editor;
                    Puzzle puz = item.DataUnTyped as Puzzle;

                    puz.MasterMap.Map = editor.Map;

                    item.IsEditable = false;
                    item.ShowDetail();
                    return;
                }

                if (instance.Param != null && instance.Param.ToString() == "Editor.Cancel")
                {
                    item.IsEditable = false;
                    item.ShowDetail();
                    return;
                }

                if (instance.Param != null && instance.Param.ToString() == "Editor.Props")
                {
                    ItemPuzzle iPuz = item as ItemPuzzle;
                    iPuz.ShowProps = true;
                    item.IsEditable = true;
                    Control implUI = item.ShowDetail();
                    ucGenericDescription details = implUI as ucGenericDescription;
                    if (details != null)
                    {
                        details.Tag = instance;
                        details.ButtonOK.Tag = instance;
                        details.ButtonOK.Click += new EventHandler(ButtonOK_Click);
                        details.ButtonCancel.Tag = instance;
                        details.ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
                    }
                    return;
                }

                if (instance.Param != null && instance.Param.ToString() == "Details.Cancel")
                {
                    ItemPuzzle iPuz = item as ItemPuzzle;
                    iPuz.ShowProps = false;
                    item.ShowDetail();
                    return;
                }

                if (instance.Param != null && instance.Param.ToString() == "Details.Ok")
                {
                    ucGenericDescription editControl = item.ShowDetail() as ucGenericDescription;
                    if (editControl == null) throw new InvalidCastException("Detail should be ucGenericDescription");

                    // Replace details
                    Puzzle puzMap = (Puzzle)item.DataUnTyped;
                    puzMap.Details = editControl.Data;

                    ItemPuzzle iPuz = item as ItemPuzzle;
                    iPuz.ShowProps = false;
                    iPuz.ShowDetail();
                    return;
                }
            }
        }

        void ButtonCancel_Click(object sender, EventArgs e)
        {
            Control senderControl = sender as Control;
            if (senderControl != null)
            {
                CommandInstance<ExplorerItem> cmdInst = senderControl.Tag as CommandInstance<ExplorerItem>;
                if (cmdInst != null)
                {
                    cmdInst.Param = "Details.Cancel";
                    cmdInst.Command.Execute(cmdInst);
                }    
            }
            
        }

        void ButtonOK_Click(object sender, EventArgs e)
        {
            Control senderControl = sender as Control;
            if (senderControl != null)
            {
                CommandInstance<ExplorerItem> cmdInst = senderControl.Tag as CommandInstance<ExplorerItem>;
                if (cmdInst != null)
                {
                    cmdInst.Param = "Details.Ok";
                    cmdInst.Command.Execute(cmdInst);
                }
            }
        }

        void editor_Commands(Editor sender, string command)
        {
            if (sender != null && sender.Tag != null)
            {
                CommandInstance<ExplorerItem> cmdInst = sender.Tag as CommandInstance<ExplorerItem>;
                if (cmdInst != null)
                {
                    cmdInst.Param = command;

                    // Perform call-back
                    cmdInst.Command.Execute(cmdInst);
                }
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


    class PuzzleDelete : CommandLibraryBase
    {
        public PuzzleDelete(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Delete Puzzle", "Delete an existing puzzle");
            
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemPuzzle delMe = instance.Context[0] as ItemPuzzle;
            if (delMe != null)
            {
                ExplorerItem parent = delMe.TreeNode.Parent.Data;

                Controller.Current.Puzzles.Remove(delMe.Data);

                parent.SyncWithData(); 
                parent.BindToUI();

                // Set selection as parent
                Controller.UpdateSelectionSingle(parent);

                // Refresh enture tree
                Controller.Explorer.BindUI();
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


    class PuzzleClone : CommandLibraryBase
    {
        public PuzzleClone(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Clone Puzzle", "Clone an existing puzzle");

        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            throw new NotImplementedException();
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemPuzzle));
        }

    }


    //#################################################################
    //#################################################################
    //#################################################################


    class PuzzlePlay : CommandLibraryBase
    {
        public PuzzlePlay(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Play Puzzle", "Play a puzzle");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemPuzzle puz = instance.Context[0] as ItemPuzzle;
            if (puz != null)
            {
                FormMain form = Controller.Explorer.TreeView.FindForm() as FormMain;
                if (form != null)
                {
                    form.StartGame(puz.Data, puz.Data.MasterMap);
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemPuzzle));
        }

    }

}
