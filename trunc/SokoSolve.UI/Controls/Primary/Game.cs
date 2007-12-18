using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Controls.Primary
{
    /// <summary>
    /// Acts as a host and bridge to <see cref="GameUI"/>
    /// </summary>
	public partial class Game : UserControl
	{
		public Game()
		{
			InitializeComponent();

            // How to use double-buffering and reduce flickering
            // http://www.lowesoftware.com/modules.php?op=modload&name=News&file=article&sid=19

            // the control will be responsible for painting itself rather then
            // letting the OS do the work
            this.SetStyle(ControlStyles.UserPaint, true);

            // by default windows painting will paint the background to clear the
            // window before painting your controls, this informs the control to
            // ignore the WM_ERASEBKGND message to reduce flicker
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // this results in the framework handling double buffering for us, all
            // drawing calls will be made to a memory buffer which is then blitted
            // to the screen (the previous two styles are required for this to work)
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            // Set coord size
		    Game_Resize(null, null);
		}

        protected override bool ProcessDialogKey(Keys keyData)
        {
            ProcessImput(keyData);

            return base.ProcessDialogKey(keyData);
            
        }


        private void buttonDone_Click(object sender, EventArgs e)
        {
            Cursor.Show();

            timerAnimation.Enabled = false;
            gameUI = null;
            
            FormMain main = FindForm() as FormMain;
            if (main != null)
            {
                main.Mode = FormMain.Modes.Library;
            }
        }

        public void StartGame(Puzzle puzzle, PuzzleMap map)
        {
            gameUI = new GameUI(map.Map);
            gameUI.OnExit += new EventHandler(gameUI_OnExit);
            Game_Resize(null, null);
            timerAnimation.Enabled = true;

            Cursor.Hide();

            gameUI.Init();
        }

        void gameUI_OnExit(object sender, EventArgs e)
        {
            buttonDone_Click(sender, e);
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            Refresh();
        }        

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            if (gameUI != null)
            {
                gameUI.InitDisplay(e.Graphics);
                gameUI.PerformStep();
                gameUI.Render();
            }
        }

        private void ProcessImput(Keys inputKey)
        {
            if (gameUI == null) return;

            switch (inputKey)
            {
                case (Keys.Up):
                    gameUI.Player.doMove(Direction.Up);
                    break;

                case (Keys.Down):
                    gameUI.Player.doMove(Direction.Down);
                    break;

                case (Keys.Left):
                    gameUI.Player.doMove(Direction.Left);
                    break;

                case (Keys.Right):
                    gameUI.Player.doMove(Direction.Right);
                    break;

                case (Keys.Escape):

                case (Keys.Q):
                    buttonDone_Click(this, null);
                    break;

                case (Keys.R):
                    gameUI.Reset();
                    break;

                case (Keys.U):
                    gameUI.Undo();
                    break;

                case (Keys.Back):
                    gameUI.Undo();
                    break;
            }
        }

        private void Game_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (gameUI != null)
            {
                if (gameUI.SetCursor(e.X, e.Y, e.Clicks, (int)e.Button))
                {
                    // Does not seem to work.
                    //Cursor.Hide();
                }
                else
                {
                    //Cursor.Show();
                }
            }
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameUI != null)
            {
                if (gameUI.SetCursor(e.X, e.Y, e.Clicks, (int)e.Button))
                {
                    //Cursor.Hide();
                }
                else
                {
                    //Cursor.Show();
                }
            }
        }


        private GameUI gameUI;

        private void Game_Resize(object sender, EventArgs e)
        {
            if (gameUI != null)
            {
                gameUI.GameCoords.WindowRegion = new RectangleInt(new VectorInt(0, 0), new SizeInt(this.Width, this.Height));    
            }
            
        }
	}
}
