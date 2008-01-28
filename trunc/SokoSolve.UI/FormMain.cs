using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.UI.Controls.Primary;
using SokoSolve.UI.Section.Solver;
using Library=SokoSolve.UI.Controls.Primary.Library;

namespace SokoSolve.UI
{
    /// <summary>
    /// Master Form for the entire application
    /// </summary>
	public partial class FormMain : Form
	{
        /// <summary>
        /// Default contructor
        /// </summary>
		public FormMain()
		{
			InitializeComponent();

		    libControl = new Library();
		    gameControl = new Game();
		    profileControl = new Panel();
		    browserControl = new InlineBrowser();
		    welcomeControl = new Welcome();
            solverControl = new SolverSectionController();

            libControl.Size = Size;
            gameControl.Size = Size;
            profileControl.Size = Size;
            browserControl.Size = Size;
            welcomeControl.Size = Size;
            solverControl.Size = Size;

            libControl.Dock = DockStyle.Fill;
            gameControl.Dock = DockStyle.Fill;
            profileControl.Dock = DockStyle.Fill;
            browserControl.Dock = DockStyle.Fill;
            welcomeControl.Dock = DockStyle.Fill;
            solverControl.Dock = DockStyle.Fill;

            SetNotVisible();

            Controls.Add(libControl);
            Controls.Add(gameControl);
            Controls.Add(profileControl);
            Controls.Add(browserControl);
            Controls.Add(welcomeControl);
            Controls.Add(solverControl);


		    Mode = Modes.Welcome;

		}

        private void SetNotVisible()
        {
            libControl.Visible = false;
            gameControl.Visible = false;
            profileControl.Visible = false;
            browserControl.Visible = false;
            welcomeControl.Visible = false;
            solverControl.Visible = false;
        }

        /// <summary>
        /// List of payload modes
        /// </summary>
        public enum Modes
        {
            Library,
            Profile,
            Game,
            Browser,
            Welcome,
            Solver
        }

        /// <summary>
        /// Current application mode
        /// </summary>
	    public Modes Mode
	    {
	        get { return mode; }
	        set
	        {
	            SuspendLayout();
	            mode = value;

	            SetNotVisible();
                switch(mode)
                {
                    case (Modes.Library):
                        libControl.Visible = true;
                        libControl.Refresh();
                        break;
                    case (Modes.Game):
                 
                        gameControl.Visible = true;
                        gameControl.Focus();
                        break;

                    case (Modes.Browser):
                        browserControl.Visible = true;
                        break;

                    case (Modes.Welcome):
                        welcomeControl.Visible = true;
                        break;

                    case (Modes.Solver):
                        solverControl.Visible = true;
                        break;
                }

	            ResumeLayout();
	        }
	    }

        /// <summary>
        /// Start a new Game
        /// </summary>
        /// <param name="puzzle"></param>
        /// <param name="map"></param>
        /// <param name="returnMode"></param>
        public void StartGame(Puzzle puzzle, PuzzleMap map, Modes returnMode)
        {
            Mode = Modes.Game;
            if (gameControl != null)
            {
                gameControl.ReturnMode = returnMode;

                ProfileController.Current.LibraryLastPuzzle = puzzle.PuzzleID;
                gameControl.StartGame(puzzle, map);
            }
        }

        /// <summary>
        /// Playback a solution in the game client
        /// </summary>
        /// <param name="puzzle"></param>
        /// <param name="map"></param>
        /// <param name="solution"></param>
        /// <param name="returnMode"></param>
        public void StartGameSolution(Puzzle puzzle, PuzzleMap map, Solution solution, Modes returnMode)
        {
            Mode = Modes.Game;
            if (gameControl != null)
            {
                gameControl.ReturnMode = returnMode;

                ProfileController.Current.LibraryLastPuzzle = puzzle.PuzzleID;
                gameControl.StartGameSolution(puzzle, map, solution);
            }
        }

        /// <summary>
        /// Init the library, but do not navigate to it
        /// </summary>
        /// <param name="Current"></param>
        public void InitLibrary(SokoSolve.Core.Model.Library Current)
        {
            ProfileController.Current.LibraryLastPuzzle = Current.FileName;
            libControl.InitLibrary(Current);
        }


        /// <summary>
        /// Load and display a file in Library mode
        /// </summary>
        /// <param name="libraryFile"></param>
        public void LoadLibrary(string libraryFile)
        {
            try
            {
                XmlProvider xml = new XmlProvider();
                InitLibrary(xml.Load(libraryFile));
                Mode = Modes.Library;
            }
            catch(Exception ex)
            {
                // Nothing
            }
        }

        /// <summary>
        /// Show a URL in the in-applicaiton html browser
        /// </summary>
        /// <param name="Url"></param>
        public void ShowInBrowser(string Url)
        {
            Mode = Modes.Browser;
            if (Url.StartsWith("$"))
            {
                browserControl.NavigateIncludedContent(Url);   
            }
            else
            {
                browserControl.Navigate(Url);
            }
        }

        /// <summary>
        /// Start the solver controller on a map
        /// </summary>
        /// <param name="map"></param>
        public void Solve(PuzzleMap map)
        {
            Mode = Modes.Solver;
            solverControl.Library = map.Puzzle.Library;
            solverControl.Puzzle = map.Puzzle;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (solverControl != null)
            {
                solverControl.Dispose();
                solverControl = null;
            }
        }

	    private Library libControl;
	    private Game gameControl;
        private Panel profileControl;
        private InlineBrowser browserControl;
	    private Welcome welcomeControl;
        private Modes mode;
        private SolverSectionController solverControl;


	}
}