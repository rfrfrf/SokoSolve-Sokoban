using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model.DataModel;
using SoloSolve.UI.WPF.ParticleEffects;

namespace SoloSolve.UI.WPF.Game
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        public Game()
        {
            InitializeComponent();
            var xml = new XmlProvider();

            Library = xml.Load(@"C:\Projects\SokoSolve\SokoSolve.UI\Content\Libraries\Sasquatch.ssx");
            logic = new SokoSolve.Core.Game.Game(Library.Puzzles.First(), Library.Puzzles[3].MasterMap.Map);
        }

        protected SokoSolve.Core.Model.Library Library { get; set; }

        private Bounce b;
        private DispatcherTimer t;
        private SokoSolve.Core.Game.Game logic;


        private void MainLoaded(object sender, RoutedEventArgs e)
        {
            Title.Text = logic.Puzzle.Details.Name;
            Description.Text = string.Format("{0} {1} {2}", logic.Puzzle.Details.Description, logic.Puzzle.Details.Author, Library.Details.Name);
            map.Logic = logic;
            map.SetGame(this);

            t = new DispatcherTimer();
            t.Interval = TimeSpan.FromSeconds(0.033);
            t.Tick += new EventHandler(CoreTimer_Click);
            t.Start();

            b = new Bounce(background, 20);

            Focus();

        }

        private void CoreTimer_Click(object sender, EventArgs e)
        {
            if (b != null) b.Step();
            map.Step();
        }

        private void MainReSize(object sender, SizeChangedEventArgs e)
        {
            background.Width = this.ActualWidth;
            background.Height = this.ActualHeight;

            game.Width = this.ActualWidth;
            game.Height = this.ActualHeight;

            foreground.Width = this.ActualWidth;
            foreground.Height = this.ActualHeight;

        }


        private void FireKeyDown(object sender, KeyEventArgs e)
        {
            Status.Text = e.Key.ToString();

            if (e.Key == Key.Up)
            {
                PerformMove(Direction.Up);
            }

            if (e.Key == Key.Down)
            {
                PerformMove(Direction.Down);
            }

            if (e.Key == Key.Left)
            {
                PerformMove(Direction.Left);
            }

            if (e.Key == Key.Right)
            {
                PerformMove(Direction.Right);
            }

            if (e.Key == Key.Back || e.Key == Key.U)
            {
                PerformUndo();
            }

            if (e.Key == Key.Escape)
            {
                Application.Current.MainWindow.Close();
            }

        }

        private void PerformUndo()
        {
            logic.Undo();
            

            map.SyncPositions(SokoSolve.Core.Game.Game.MoveResult.Undo, Direction.None);
        }

        private void FireKeyUp(object sender, KeyEventArgs e)
        {
           
            
        }

        private void PerformMove(Direction dir)
        {
            try
            {
                var res = logic.Move(dir);
                Status.Text = res.ToString();

                map.SyncPositions(res, dir);
            }
            catch (Exception e)
            {

                Status.Text = e.Message;
                Status.Tag = e;
            }
           

            
        }

        private void FirePreviewDown(object sender, KeyEventArgs e)
        {
            var i = 0;
        }

        private void ViewStatus(object sender, MouseButtonEventArgs e)
        {
            if (Status.Tag != null)
            {
                MessageBox.Show(Status.Tag.ToString(), Status.Text);
            }
            
        }

        public void SetStatus(string s)
        {
            Status.Text = s;
        }
    }
}
