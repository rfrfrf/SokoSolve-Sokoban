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
        }

        private Bounce b;
        private DispatcherTimer t;


        private void MainLoaded(object sender, RoutedEventArgs e)
        {
            t = new DispatcherTimer();
            t.Interval = TimeSpan.FromSeconds(0.033);
            t.Tick += new EventHandler(CoreTimer_Click);
            t.Start();


            b = new Bounce(background, 20);
            
        }

        private void CoreTimer_Click(object sender, EventArgs e)
        {
            if (b != null) b.Step();
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

        

    }
}
