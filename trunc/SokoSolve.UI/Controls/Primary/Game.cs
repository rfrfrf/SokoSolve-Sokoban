using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.UI;
using SokoSolve.Core.UI.Nodes;
using SokoSolve.Core.UI.Nodes.Effects;
using SokoSolve.Core.UI.Paths;
using SokoSolve.UI.Controls.Secondary;

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
            gameUI.Active = false;

            Cursor.Show();

            timerAnimation.Enabled = false;
            gameUI = null;
            
            FormMain main = FindForm() as FormMain;
            if (main != null)
            {
                main.Mode = returnMode;
            }
        }

        public void HandleGameException(string Context, Exception ex)
        {
            FormError error = new FormError();
            error.Exception = ex;
            error.ShowDialog();
        }

        #region Core Game Functions

        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="puzzle"></param>
        /// <param name="map"></param>
        public void StartGame(Puzzle puzzle, PuzzleMap map)
        {
            try
            {
                puzzleMap = map;
                gameUI = new GameUI(puzzle, map.Map, new WindowsSoundSubSystem());
                gameUI.OnExit += new EventHandler(gameUI_OnExit);
                gameUI.OnGameWin += new EventHandler<NotificationEvent>(gameUI_OnGameWin);
                Game_Resize(null, null);
                timerAnimation.Enabled = true;

                Cursor.Hide();

                gameUI.Start();
            }
            catch (Exception ex)
            {
                HandleGameException("During Game Startup", ex);
            }
        }

        

        /// <summary>
        /// Perform game step and drawing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (gameUI != null)
                {
                    
                    gameUI.StartRender(e.Graphics);
                    gameUI.PerformStep();
                    gameUI.Render();
                    gameUI.EndRender();
                }
            }
            catch(Exception ex)
            {
                HandleGameException("Drawing Frame", ex);
            }
            
        }

        #endregion

        
        void gameUI_OnExit(object sender, EventArgs e)
        {
            buttonDone_Click(sender, e);
        }

        void gameUI_OnGameWin(object sender, NotificationEvent e)
        {
            switch(e.Command)
            {
                case ("Next"): PuzzleCompletedNext(); return;
                case ("Cancel"): PuzzleCompletedCancel(); return;
                case ("Save"): PuzzleCompletedSave(); return;
                case ("Home"): PuzzleCompletedExit(); return;
            }

            throw new NotImplementedException();
            
        }

        /// <summary>
        /// Show a basic message in the center of the puzzle (with a background)
        /// </summary>
        /// <param name="message"></param>
        private void ShowText(string message)
        {
            NodeEffectText node = new NodeEffectText(gameUI, 100000, message, gameUI.GameCoords.PuzzleRegion.Center);
            node.BrushBackGround = new SolidBrush(Color.DarkSlateBlue);
            node.Path = new StaticPath(node.CurrentAbsolute, 20);
            gameUI.Add(node);
        }

        private void PuzzleCompletedExit()
        {
            buttonDone_Click(null, null);
        }

        private void PuzzleCompletedSave()
        {
            // Save
            Solution sol = new Solution();
            sol.FromGame(gameUI.Moves);
            sol.Details = new GenericDescription();
            sol.Details.Name = string.Format("'{0}' Solution", gameUI.Puzzle.Details.Name);
            sol.Details.Date = DateTime.Now;
            sol.Details.DateSpecified = true;
            sol.Details.Comments = "Solution found from game player.";
            gameUI.Puzzle.MasterMap.Solutions.Add(sol);   

            // TODO: Save Way points

            // Give feedback
            ShowText("Solution Saved.");
        }

        private void PuzzleCompletedCancel()
        {
            StartGame(gameUI.Puzzle, puzzleMap);   
        }

        private void PuzzleCompletedNext()
        {
            SokoSolve.Core.Model.Library lib = gameUI.Puzzle.Library;
            Puzzle next = lib.Next(gameUI.Puzzle);
            if (next != null)
            {
                StartGame(next, next.MasterMap);    
            }
            else
            {
                // Give feedback
                ShowText("There are no more puzzles");

                // TODO: Find the next library from the profile
            }
        }


        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            Refresh();
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

        static GameUI.MouseButtons Convert(MouseButtons item)
        {
            if (item == MouseButtons.Left) return GameUI.MouseButtons.Left;
            if (item == MouseButtons.Right) return GameUI.MouseButtons.Right;

            return GameUI.MouseButtons.None;
        }

        private void Game_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (gameUI != null)
            {
                if (gameUI.SetCursor(e.X, e.Y, e.Clicks, Convert(e.Button), GameUI.MouseClicks.Down))
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

        private void Game_MouseUp(object sender, MouseEventArgs e)
        {
            if (gameUI != null)
            {
                if (gameUI.SetCursor(e.X, e.Y, e.Clicks, Convert(e.Button), GameUI.MouseClicks.Up))
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
                if (gameUI.SetCursor(e.X, e.Y, e.Clicks, Convert(e.Button), GameUI.MouseClicks.None))
                {
                    //Cursor.Hide();
                }
                else
                {
                    //Cursor.Show();
                }
            }
        }

        /// <summary>
        /// Which UI state to return from a game to.
        /// </summary>
        public FormMain.Modes ReturnMode
        {
            get { return returnMode; }
            set { returnMode = value; }
        }

        private void Game_Resize(object sender, EventArgs e)
        {
            if (gameUI != null)
            {
                gameUI.GameCoords.WindowRegion = new RectangleInt(new VectorInt(0, 0), new SizeInt(this.Width, this.Height));    
            }
        }

        private GameUI gameUI;
        private FormMain.Modes returnMode = FormMain.Modes.Library;
        private PuzzleMap puzzleMap;

        
	}
}
