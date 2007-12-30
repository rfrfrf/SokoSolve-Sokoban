using System;
using System.Collections.Generic;
using System.Drawing;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI;
using SokoSolve.UI.Section;

namespace SokoSolve.UI.Section.Library
{
    /// <summary>
    /// Concrete implementation of the Library controller.
    /// See <see cref="LibraryExplorer"/>
    /// </summary>
	public class LibraryController : Controller<ExplorerItem>
	{
        /// <summary>
        /// Strong Constructor. Primarily responsible for registering all available commands and attaching them to the library UI (menu, toolbar)
        /// </summary>
        /// <param name="myView"></param>
        public LibraryController(SokoSolve.UI.Controls.Primary.Library myView)
		{
            
			view = myView;
			selection = new List<ExplorerItem>();

            // Library Commands
            Register(new LibraryExit(this, new object[] { view.tsbLibraryExit }));
			Register(new LibraryNew(this, new object[] { view.tsbLibraryNew, view.mbLibraryNew } ));
			Register(new LibraryOpen(this, new object[] { view.tbsLibraryOpen, view.mbLibraryOpen}));
            Register(new LibrarySave(this, new object[] { view.tsbLibrarySave, view.mbSave }));
		    Register(new LibraryEdit(this, new object[] {view.mbEdit, view.tsbLibraryProperties}));
            Register(new LibraryImport(this, new object[] { view.tsbLibraryImport }));
            Register(new LibraryRefresh(this, new object[] { view.tsbLibraryRefresh }));

            // Category Commands
            Register(new CategoryNew(this, new object[] { view.tsbCategoryNew }));
            Register(new CategoryDelete(this, new object[] { view.tsbCategoryDelete }));
            Register(new CategoryEdit(this, new object[] { view.tsbCategoryProperties }));

            // Puzzle Commands
            Register(new PuzzleNew(this, new object[] { view.tsbPuzzleNew }));
            Register(new PuzzleEdit(this, new object[] { view.tsbPuzzleEdit, view.mbPuzzleEdit }));
            Register(new PuzzleDelete(this, new object[] { view.tsbPuzzleDelete, view.mbDelete }));
            Register(new PuzzleClone(this, new object[] { view.tsbPuzzleClone }));
            Register(new PuzzlePlay(this, new object[] { view.tsbPuzzlePlay, view.mbPuzzlePlay }));

            // Solution Commands
            Register(new SolutionReplay(this, new object[] { view.tsbSolutionReplay }));
            Register(new SolutionTest(this, new object[] { view.tsbSolutionTest }));

            // Extra Commands
            Register(new HelpAbout(this, new object[] { view.tsbHelpAbout }));
            Register(new HelpHowToPlay(this, new object[] { view.tsbHowToPlay }));
            Register(new HelpLicense(this, new object[] { view.tsbHelpSoftwareLicense }));
            Register(new HelpRelease(this, new object[] { view.tsbHelpReleaseNotes }));
            Register(new HelpCheckVersion(this, new object[] { view.tsbCheckVersion }));
            Register(new HelpWebSite(this, new object[] { view.tsbHelpWebSite }));
            Register(new HelpReturn(this, new object[] { view.tsbReturn }));

			UpdateUI("Controller.Init");
		}

        /// <summary>
        /// Allow this controller to know it's Explorer
        /// </summary>
        public LibraryExplorer Explorer
        {
            get { return explorer; }
            set { explorer = value; }
        }

        /// <summary>
        /// Currently Library. This is not the current selection, for this <see cref="Selection"/>
        /// </summary>
        public Core.Model.Library Current
		{
			get { return currentLibrary; }
			set 
			{ 
				currentLibrary = value;

                // LibraryExplorer will pick up this change and rebuild the ExplorerItem tree
				if (OnCurrentChanged != null) OnCurrentChanged(this, new EventArgs());

                // Now change the selection to the Library
			    UpdateSelectionSingle(Explorer.Root.Data);
			    Explorer.UpdateSelection(Explorer.Root.Data);
			}
		}
        /// <summary>
        /// Has the library been changed
        /// </summary>
		public event EventHandler OnCurrentChanged;

        /// <summary>
        /// Update the UI, informs the commands and allows them to bind to the UI.
        /// </summary>
        /// <param name="context"></param>
		public override void UpdateUI(string context)
		{
            
		    Logger.Add(this, "UpdateUI - {0}.", context);
		    Logger.StartSection();

			foreach (Command<ExplorerItem> cmd in commands)
			{
				AttachedCommand<ExplorerItem> attached = cmd as AttachedCommand<ExplorerItem>;
				if (attached != null)
				{
					attached.UpdateUI(context);
				}
			}

            Logger.EndSection();
		}

        /// <summary>
        /// Change the current selection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
		public override bool UpdateSelection(List<ExplorerItem> item)
		{
			bool updated = base.UpdateSelection(item);
			if (updated)
			{
				foreach(Command<ExplorerItem> command in commands)
				{
					command.UpdateForSelection(item);
				}

				UpdateUI("Controller.Selection.Changed");
			}

			return updated;
		}

        /// <summary>
        /// Update the status and status text, this is shown on the status bar.
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="text"></param>
		public override void SetStatus(ExecutionStatus newStatus, string text)
		{
			base.SetStatus(newStatus, text);
			view.tsLabelStatus.Text = string.Format("{0} - {1}", newStatus, text);

            switch(newStatus)
            {
                case(ExecutionStatus.Error): 
                    view.tsLabelStatus.ForeColor = Color.Red;
                    break;
                case (ExecutionStatus.Complete):
                    view.tsLabelStatus.ForeColor = Color.Green;
                    break;
                case (ExecutionStatus.Incomplete):
                    view.tsLabelStatus.ForeColor = Color.DarkOrange;
                    break;
                default:
                    view.tsLabelStatus.ForeColor = Color.Black;
                    break;
            }
		}

        /// <summary>
        /// Perform a URI command (passed in from the browser controls)
        /// </summary>
        /// <param name="appURI"></param>
        /// <returns></returns>
        public override bool PerformCommandURI(Uri appURI)
        {
            if (appURI == null) return false;

            if (appURI.ToString().StartsWith("app://puzzle/"))
            {
                // Select the top item
                string findID = appURI.ToString().Remove(0, "app://puzzle/".Length);
                TreeNode<ExplorerItem> find = explorer.Root.Find(delegate(TreeNode<ExplorerItem> item)
                          {
                              Puzzle puz = item.Data.DataUnTyped as Puzzle;
                              if (puz == null) return false;
                              return puz.PuzzleID == findID;
                          }, int.MaxValue);

                if (find != null)
                {
                    UpdateSelectionSingle(find.Data);

                    // Update UI
                    Explorer.UpdateSelection();
                }
            }
            return true;
        }

      

        private SokoSolve.Core.Model.Library currentLibrary;
        private SokoSolve.UI.Controls.Primary.Library view;
        private List<ExplorerItem> selection;
        private LibraryExplorer explorer;

	}
}
