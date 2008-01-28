using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
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
                CreateNewPuzzle(category, category.DomainData);
            }

            ItemLibrary library = instance.Context[0] as ItemLibrary;
            if (library != null)
            {
                CreateNewPuzzle(library, library.DomainData.CategoryTree.Root.Data);
            }
        }

        private void CreateNewPuzzle(ExplorerItem current, Category category)
        {
            Puzzle newPuz = new Puzzle(Controller.Current);
            newPuz.PuzzleID = Controller.Current.IdProvider.GetNextIDString("P{0}");
            newPuz.Details = ProfileController.Current.GenericDescription;
            newPuz.Details.Name = string.Format("New Puzzle #{0}", Controller.Current.Puzzles.Count+1);
            newPuz.Category = category;
            newPuz.MasterMap = new PuzzleMap(newPuz);
            newPuz.MasterMap.MapID = Controller.Current.IdProvider.GetNextIDString("M{0}");
            newPuz.Order = Controller.Current.Puzzles.Count + 1;

            Controller.Current.Puzzles.Add(newPuz);

            // Refresh the UI model to updated domain data
            current.SyncDomain();

            // Refresh enture tree
            Controller.Explorer.SyncUI();
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemCategory)) ||
                ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemLibrary));
        }
    }
    //#################################################################
    //#################################################################
    //#################################################################


    class PuzzleNewQuickStart : CommandLibraryBase
    {
        public PuzzleNewQuickStart(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("New (Clipboard Quickstart)", "Create a new puzzle map, using the current clipboard contents");
        }


        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemCategory category = instance.Context[0] as ItemCategory;
            if (category != null)
            {
                // Create a new puzzle with a valid number etc.
                CreateNewPuzzle(category, category.DomainData);
            }

            ItemLibrary library = instance.Context[0] as ItemLibrary;
            if (library != null)
            {
                CreateNewPuzzle(library, library.DomainData.CategoryTree.Root.Data);
            }
        }

        private void CreateNewPuzzle(ExplorerItem current, Category category)
        {
            Puzzle newPuz = new Puzzle(Controller.Current);
            newPuz.PuzzleID = Controller.Current.IdProvider.GetNextIDString("P{0}");
            newPuz.Details = ProfileController.Current.GenericDescription;
            newPuz.Details.Name = string.Format("New Puzzle #{0}", Controller.Current.Puzzles.Count + 1);
            newPuz.Category = category;
            newPuz.MasterMap = new PuzzleMap(newPuz);
            newPuz.MasterMap.MapID = Controller.Current.IdProvider.GetNextIDString("M{0}");
            newPuz.Order = Controller.Current.Puzzles.Count + 1;

            newPuz.MasterMap.Map.SetFromStrings(StringHelper.Split(Clipboard.GetText(), "\n"), SokobanMap.InternetChars);

            Controller.Current.Puzzles.Add(newPuz);

            // Refresh the UI model to updated domain data
            current.SyncDomain();

            // Refresh enture tree
            Controller.Explorer.SyncUI();

            Controller.Explorer.UpdateSelection(current);
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemCategory)) ||
                ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemLibrary));
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
                        editor.PuzzleMap = new PuzzleMap((item.DataUnTyped as Puzzle).MasterMap);
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

                    puz.MasterMap = editor.PuzzleMap;

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

                

                if (instance.Param != null && instance.Param.ToString() == "Details.Cancel")
                {
                    ItemPuzzle iPuz = item as ItemPuzzle;
                
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
                
                    iPuz.ShowDetail();
                    return;
                }

                if (instance.Param != null && instance.Param.ToString() == "Editor.Copy")
                {
                    Puzzle puzMap = (Puzzle)item.DataUnTyped;

                    Clipboard.SetText(puzMap.MasterMap.ToString(), TextDataFormat.Text);
                    return;
                }

                if (instance.Param != null && instance.Param.ToString() == "Editor.Paste")
                {
                    Puzzle puzMap = (Puzzle)item.DataUnTyped;

                    Clipboard.SetText(puzMap.MasterMap.ToString(), TextDataFormat.Text);
                    return;
                }

                if (instance.Param != null && instance.Param.ToString() == "Editor.Rotate")
                {
                    Puzzle puzMap = (Puzzle)item.DataUnTyped;

                    puzMap.MasterMap.Map.Rotate();
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

                Controller.Current.Puzzles.Remove(delMe.DomainData);

                parent.SyncDomain(); 
                parent.SyncUI();

                // Set selection as parent
                Controller.UpdateSelectionSingle(parent);

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


    class PuzzleClone : CommandLibraryBase
    {
        public PuzzleClone(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Clone Puzzle", "Clone an existing puzzle");

        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemPuzzle puz = instance.Context[0] as ItemPuzzle;
            if (puz != null)
            {
                Puzzle cloned = new Puzzle(puz.DomainData.Library);
                cloned.PuzzleID = puz.DomainData.Library.IdProvider.GetNextIDString("P{0}");
                cloned.Category = puz.DomainData.Category;
                cloned.Details = new GenericDescription(puz.DomainData.Details);
                cloned.MasterMap = new PuzzleMap(cloned);
                cloned.MasterMap.Details = new GenericDescription(puz.DomainData.MasterMap.Details);
                cloned.MasterMap.MapID = puz.DomainData.Library.IdProvider.GetNextIDString("M{0}");
                cloned.MasterMap.Map = new SokobanMap(puz.DomainData.MasterMap.Map);
                puz.DomainData.Library.Puzzles.Add(cloned);

                Controller.Explorer.Refresh();
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
                    form.StartGame(puz.DomainData, puz.DomainData.MasterMap, FormMain.Modes.Library);
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


    class PuzzleSolve : CommandLibraryBase
    {
        public PuzzleSolve(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Solve Puzzle", "Solve puzzle");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemPuzzle puz = instance.Context[0] as ItemPuzzle;
            if (puz != null)
            {
                FormMain form = Controller.Explorer.TreeView.FindForm() as FormMain;
                if (form != null)
                {
                    form.Solve(puz.DomainData.MasterMap);
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemPuzzle));
        }

    }

}
