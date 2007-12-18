using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Core.Model;
using SokoSolve.UI.Controls.Primary;
using Library=SokoSolve.UI.Controls.Primary.Library;

namespace SokoSolve.UI
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();

		    libControl = new Library();
		    gameControl = new Game();
		    profileControl = new Panel();
		    browserControl = new InlineBrowser();
		    welcomeControl = new Welcome();
		    Mode = Modes.Welcome;
		}

        public enum Modes
        {
            Library,
            Profile,
            Game,
            Browser,
            Welcome
        }

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
                }

	            ResumeLayout();
	        }
	    }

        public void StartGame(Puzzle puzzle, PuzzleMap map)
        {
            Mode = Modes.Game;
            if (gameControl != null)
            {
                gameControl.StartGame(puzzle, map);
            }
        }

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

	    private Library libControl;
	    private Game gameControl;
        private Panel profileControl;
        private InlineBrowser browserControl;
	    private Welcome welcomeControl;
        private Modes mode;
	}
}