using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Model;
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
            solverControl = new SolverSection();
		    Mode = Modes.Welcome;
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

	            Controls.Clear();
                switch(mode)
                {
                    case (Modes.Library):
                        libControl.Dock = DockStyle.Fill;
                        Controls.Add(libControl);
                        libControl.Dock = DockStyle.Fill;
                        break;
                    case (Modes.Game):
                        Controls.Add(gameControl);
                        gameControl.Dock = DockStyle.Fill;
                        gameControl.Focus();
                        break;

                    case (Modes.Browser):
                        Controls.Add(browserControl);
                        browserControl.Dock = DockStyle.Fill;
                        break;

                    case (Modes.Welcome):
                        Controls.Add(welcomeControl);
                        welcomeControl.Dock = DockStyle.Fill;
                        break;

                    case (Modes.Solver):
                        Controls.Add(solverControl);
                        solverControl.Dock = DockStyle.Fill;
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

        public void Solve(PuzzleMap map)
        {
            Mode = Modes.Solver;
            solverControl.Map = map;
        }

	    private Library libControl;
	    private Game gameControl;
        private Panel profileControl;
        private InlineBrowser browserControl;
	    private Welcome welcomeControl;
        private Modes mode;
        private SolverSection solverControl;
	}
}