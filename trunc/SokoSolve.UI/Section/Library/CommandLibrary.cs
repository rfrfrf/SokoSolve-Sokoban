using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.IO;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.Reporting;
using SokoSolve.Core.UI;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Section.Library
{
    abstract class CommandLibraryBase : AttachedCommand<ExplorerItem>
    {
        public CommandLibraryBase(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller)
        {
            InitUI(buttonControls);
        }

        public new LibraryController Controller { get { return base.Controller as LibraryController; } }
    }


    //#################################################################
    //#################################################################
    //#################################################################

    class LibraryNew : CommandLibraryBase
    {
        public LibraryNew(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("New Library", "Create a simple default library");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            Controller.Current = MakeDefaultLibrary();
        }

        /// <summary>
        /// Build
        /// </summary>
        /// <returns></returns>
        SokoSolve.Core.Model.Library MakeDefaultLibrary()
        {
            SokoSolve.Core.Model.Library lib = new SokoSolve.Core.Model.Library(Guid.NewGuid());
            lib.Details.Name = "New default library";
            lib.Details.Description = "Skeleton new library";
            lib.Details.Author = new GenericDescriptionAuthor();
            lib.Details.Author.Name = "Unknown author";
            lib.Details.Author.Email = "noreply@sokosolve.sourceforge.net";
            lib.Details.Author.Homepage = "http://sokosolve.sourceforge.net";
            lib.Details.Date = DateTime.Now;
            lib.Details.License = "Creative Commons";
            lib.Details.Comments = "This is a skeleton sokoban library. ALL RIGHTS RESERVED.";

            Category easy = new Category(lib);
            easy.CategoryID = lib.IdProvider.GetNextIDString("C{0}");
            easy.Details = new GenericDescription(lib.Details);
            easy.Details.Name = "Easy";
            easy.CategoryParentREF = lib.CategoryTree.Root.Data.CategoryID;
            lib.CategoryTree.Root.Add(easy);

            Category medium = new Category(lib);
            medium.CategoryID = lib.IdProvider.GetNextIDString("C{0}");
            medium.Details = new GenericDescription(lib.Details);
            medium.Details.Name = "Medium";
            medium.CategoryParentREF = lib.CategoryTree.Root.Data.CategoryID;
            lib.CategoryTree.Root.Add(medium);

            Category hard = new Category(lib);
            hard.CategoryID = lib.IdProvider.GetNextIDString("C{0}");
            hard.Details = new GenericDescription(lib.Details);
            hard.Details.Name = "Hard";
            hard.CategoryParentREF = lib.CategoryTree.Root.Data.CategoryID;
            lib.CategoryTree.Root.Add(hard);

            Puzzle newPuz = new Puzzle(lib);
            newPuz.PuzzleID = lib.IdProvider.GetNextIDString("P{0}");
            newPuz.Category = medium;
            newPuz.Details = new GenericDescription(lib.Details);
            newPuz.Details.Name = "Empty";
            newPuz.Details.Description = "Edit me...";
            newPuz.Order = 1;
            newPuz.Rating = "Normal";

            PuzzleMap map = new PuzzleMap(newPuz);
            map.MapID = lib.IdProvider.GetNextIDString("M{0}");
            map.Map = new SokobanMap();
            map.Map.SetFromStrings(new string[]
                                       {
                                           	"~~~###~~~~~",
					                        "~~##.#~####",
					                        "~##..###..#",
					                        "##.X......#",
					                        "#...PX.#..#",
					                        "###.X###..#",
					                        "~~#..#OO..#",
					                        "~##.##O#.##",
					                        "~#......##~",
					                        "~#.....##~~",
					                        "~#######~~~"
                                       });
            Solution sol = new Solution(map, new VectorInt(10,10));
            sol.Details = new GenericDescription();
            sol.Details.Name = "Invalid sample solution";
            sol.Steps = "UUUDUDUDUUUDUDULLLLLRRRRRR";
            map.Solutions.Add(sol);
            newPuz.Maps.Add(map);

            PuzzleMap mapAlt = new PuzzleMap(newPuz);
            mapAlt.Details = new GenericDescription();
            mapAlt.Details.Name = "Easier Alternative";
            mapAlt.MapID = lib.IdProvider.GetNextIDString("M{0}");
            mapAlt.Map = new SokobanMap();
            mapAlt.Map.SetFromStrings(new string[]
                                       {
                                           	"~~~###~~~~~",
					                        "~~#..#~####",
					                        "~#...###..#",
					                        "##.X......#",
					                        "#...PX.#..#",
					                        "###.X###..#",
					                        "~~#..#OO..#",
					                        "~##.##O#.##",
					                        "~#......##~",
					                        "~#.....##~~",
					                        "~#######~~~"
                                       });
            newPuz.Maps.Add(mapAlt);

            


            lib.Puzzles.Add(newPuz);

            return lib;
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################


    class LibraryExit : CommandLibraryBase
    {
        public LibraryExit(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Exit Library", "Exit the library");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            Controller.Logger.Add(this, "[TODO] Check if changes need to be saved, then confirm");

            // Close the app.
            Application.Exit();
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################


    class LibraryOpen : CommandLibraryBase
    {
        public LibraryOpen(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Open...", "Open an existing library");
            
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = ProfileController.Current.LibraryCurrentOpenDir;
            open.CheckFileExists = true;
            open.RestoreDirectory = true;
            open.Title = "Open SokoSolve .SSX XML file";
            open.DefaultExt = ".ssx";
            if (open.ShowDialog() == DialogResult.OK)
            {
                XmlProvider xmlLoad = new XmlProvider();
                Controller.Current = xmlLoad.Load(open.FileName);

                ProfileController.Current.LibraryLastFile = open.FileName;
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################


    class LibrarySave : CommandLibraryBase
    {
        public LibrarySave(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Save", "Save the current library");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            if (!string.IsNullOrEmpty(Controller.Current.FileName))
            {
                XmlProvider xmlSave = new XmlProvider();
                xmlSave.Save(Controller.Current, Controller.Current.FileName);
            }
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################


    class LibrarySaveAs : CommandLibraryBase
    {
        public LibrarySaveAs(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Save As...", "Save the current library");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = FileManager.getContent("Libraries/");
            saveFileDialog.DefaultExt = "SSX";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save SokoSolve .SSX XML file";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlProvider xmlSave = new XmlProvider();
                xmlSave.Save(Controller.Current, saveFileDialog.FileName);
            }
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class LibraryImport : CommandLibraryBase
    {
        public LibraryImport(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Import Library", "Import an external library");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormImport import = new FormImport();
            import.ucImport.Library = Controller.Current;
            if (import.ShowDialog() == DialogResult.OK)
            {
                Controller.Current = import.ucImport.Library;
            }
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################


    class LibraryEdit : CommandLibraryBase
    {
        public LibraryEdit(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Edit Library", "Edit library properties");
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

            if (instance.Status == ExecutionStatus.AwaitingCallback)
            {
                if (instance.Param != null && instance.Param.ToString() == "Ok")
                {
                    ucGenericDescription editControl = item.ShowDetail() as ucGenericDescription;
                    if (editControl == null) throw new InvalidCastException("Detail should be ucGenericDescription");
                    // Replace details
                    Core.Model.Library lib = (Core.Model.Library)item.DataUnTyped;
                    lib.Details = editControl.Data;

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
            // Enable only when there is exact 1 item of type ItemLibrary
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof (ItemLibrary));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class LibraryRefresh : CommandLibraryBase
    {
        public LibraryRefresh(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Refresh", "Import an external library");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            Controller.Explorer.Refresh();
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class LibraryReportHTML : CommandLibraryBase
    {
        public LibraryReportHTML(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Report (HTML)", "Generate and XHTML puzzle report");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormReport report = new FormReport();
            report.Library = Controller.Current;
            report.ShowDialog();
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            // Enable only when there is exact 1 item of type ItemLibrary
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemLibrary));
        }
    }

}
