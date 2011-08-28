using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SokoSolve.Common.Math;
using SokoSolve.Core;

namespace SoloSolve.UI.WPF.Game
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        private readonly List<Crate> crates;
        private SokoSolve.Core.Game.Game logic;
        private Image player;
        private int step;
        private Game game;

        public Map()
        {
            InitializeComponent();

            crates = new List<Crate>();
        }

        public void SetGame(Game g)
        {
            game = g;
        }

        public SokoSolve.Core.Game.Game Logic
        {
            get { return logic; }
            set
            {
                logic = value;
                Init();
            }
        }

        public void Step()
        {
            step++;

            if (step%123 == 0)
            {
                foreach (Crate crate in RandomHelper.Select(crates, 1))
                {
                    crate.spinspeed = RandomHelper.NextDouble(-1, 1, 1);
                }
            }

            foreach (Crate crate in crates)
            {
                crate.spin += crate.spinspeed;
                crate.Element.RenderTransform = new RotateTransform(crate.spin, crate.Element.ActualWidth/2,
                                                                    crate.Element.ActualHeight/2);
            }
        }

        

        private void Init()
        {
            grid.Children.Clear();
            crates.Clear();

            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            SizeInt size = Logic.Current.Size;
            for (int r = 0; r < size.Height; r++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    MinHeight = 16,
                });
            }

            for (int c = 0; c < size.Width; c++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    MinWidth = 16,
                });
            }

            for (int r = 0; r < size.Height; r++)
                for (int c = 0; c < size.Width; c++)
                {
                    var cells = CellFactory(c, r);
                    if (cells != null)
                    {
                        foreach (var cell in cells)
                        {
                            Grid.SetColumn(cell, c);
                            Grid.SetRow(cell, r);
                            grid.Children.Add(cell);    
                        }
                        
                    }
                }


            // So that the player has the largest z-Order...
            for (int r = 0; r < size.Height; r++)
                for (int c = 0; c < size.Width; c++)
                {
                    CellStates tile = Logic.Current[c, r];

                    if (tile == CellStates.FloorCrate || tile == CellStates.FloorCrate || tile == CellStates.FloorGoalCrate)
                    {
                        var crate = new Crate
                        {
                            Element = new Image
                            {
                                Stretch = Stretch.Fill,
                                Source = Resources["Crate"] as BitmapImage
                            }
                        };
                        Grid.SetColumn(crate.Element, c);
                        Grid.SetRow(crate.Element, r);
                        grid.Children.Add(crate.Element);
                        crates.Add(crate);
                    }
                }

            for (int r = 0; r < size.Height; r++)
                for (int c = 0; c < size.Width; c++)
                {
                    CellStates tile = Logic.Current[c, r];

                    if (tile == CellStates.FloorPlayer || tile == CellStates.FloorGoalPlayer)
                    {
                        player = new Image
                        {
                            Stretch = Stretch.Fill,
                            Source = Resources["Player"] as BitmapImage
                        };
                        Grid.SetColumn(player, c);
                        Grid.SetRow(player, r);
                        grid.Children.Add(player);
                    }
                }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Logic != null) Init();
        }

        private IEnumerable<UIElement> CellFactory(int x, int y)
        {
            CellStates cell = Logic.Current[x, y];

            if (cell == CellStates.Void) return null;

            if (cell == CellStates.Wall)
            {
                return new UIElement[]
                {
                    new Image
                        {
                            Stretch = Stretch.Fill,
                            Source = Resources["Wall"] as BitmapImage
                        }
                };
            }

            if (cell == CellStates.FloorGoal || cell == CellStates.FloorGoalCrate)
            {
                return new UIElement[]
                {
                    new Image
                    {
                        Stretch = Stretch.Fill,
                        Source = Resources["Floor"] as BitmapImage
                    },
                    new Image
                    {
                        Stretch = Stretch.Fill,
                        Source = Resources["Goal"] as BitmapImage
                    }
                };
            }

            return new UIElement[]
            {
                new Image
                    {
                        Stretch = Stretch.Fill,
                        Source = Resources["Floor"] as BitmapImage
                    }
            };
        }

        private VectorInt GetPosition(UIElement e)
        {
            return new VectorInt(
                Grid.GetColumn(e),
                Grid.GetRow(e)
                );
        }

        private void SetPosition(UIElement e, VectorInt pos)
        {
            Grid.SetColumn(e, pos.X);
            Grid.SetRow(e, pos.Y);
        }

        public void SyncPositions(SokoSolve.Core.Game.Game.MoveResult r, Direction push)
        {
            if (r == SokoSolve.Core.Game.Game.MoveResult.Undo)
            {
                Init();
                return;
            }

            if (r == SokoSolve.Core.Game.Game.MoveResult.ValidPush ||
                r == SokoSolve.Core.Game.Game.MoveResult.ValidPushGoal ||
                r == SokoSolve.Core.Game.Game.MoveResult.ValidPushWin)
            {
                // Change the pushed crate location
                VectorInt oldCratePos = Logic.Current.Player; // Note: The crate is now in the wrong place
                Crate crate = crates.First(x => GetPosition(x.Element) == oldCratePos);
                SetPosition(crate.Element, oldCratePos.Offset(push));
            }

            Grid.SetColumn(player, Logic.Current.Player.X);
            Grid.SetRow(player, Logic.Current.Player.Y);
        }

        private class Crate
        {
            public int Index { get; set; }
            public Image Element { get; set; }

            public double spin { get; set; }
            public double spinspeed { get; set; }
        }


        public void HandleClick(MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;

            int c = Grid.GetColumn(element);
            int r = Grid.GetRow(element);

            var p = e.GetPosition(grid);

            game.SetStatus(string.Format("m:{0} c:{1} r:{2} = {3}", p, c, r, Logic.Current[c, r]));
        }

        private VectorInt start;

        private void FireLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;
            int c = Grid.GetColumn(element);
            int r = Grid.GetRow(element);

            start = new VectorInt(c, r);

            game.SetStatus(string.Format("Start {0}", start));
        }

        private void FireLeftMouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;
            int c = Grid.GetColumn(element);
            int r = Grid.GetRow(element);

            var end = new VectorInt(c, r);

            game.SetStatus(string.Format("Start {0} to {1}", start, end));
           
            start = VectorInt.Null;
        }
    }
}