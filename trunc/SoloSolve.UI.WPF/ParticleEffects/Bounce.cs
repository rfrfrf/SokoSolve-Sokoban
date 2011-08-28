using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SokoSolve.Common.Math;
using SokoSolve.Core;


namespace SoloSolve.UI.WPF.ParticleEffects
{

    public interface IStep
    {
        void Step();
    }

    public class Bounce
    {
        private List<Particle> elements;
        private Canvas container;

        class Particle
        {
            public VectorDouble Speed { get; set; }
            public UIElement Target { get; set; }
            public VectorDouble Size { get; set; }
            public double SpinSpeed { get; set; }
            public double Spin { get; set; }
        }

        public Bounce(Canvas canvas, int count)
        {
            container = canvas;
            elements = new List<Particle>(count);

            var r = new Random();
            for (int i = 0; i < count; i++)
            {
                var n = new Rectangle()
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.LightBlue)
                };
               
                Canvas.SetLeft(n, r.Next(0, (int)(canvas.ActualWidth - n.Width)));
                Canvas.SetTop(n, r.Next(0, (int)(canvas.ActualHeight- n.Height)));
                container.Children.Add(n);

                elements.Add(new Particle()
                                 {
                                     Size = new VectorDouble(n.Width, n.Height),
                                     Speed = new VectorDouble(
                                         RandomHelper.NextDouble(-1, 1, 1),
                                         RandomHelper.NextDouble(-1, 1, 1)
                                         ),
                                     Target = n,
                                     SpinSpeed = RandomHelper.NextDouble(-1, 1, 1),
                                 });
                
            }
        }

        public void Step()
        {
            if (elements == null) return;

            foreach (var e in elements)
            {
                var p = new VectorDouble(Canvas.GetLeft(e.Target), Canvas.GetTop(e.Target));
                if (p.X + e.Speed.X > container.ActualWidth - e.Size.X) e.Speed.X *= -1;
                if (p.X + e.Speed.X < 0) e.Speed.X *= -1;

                if (p.Y + e.Speed.Y > container.ActualHeight - e.Size.Y) e.Speed.Y *= -1;
                if (p.Y + e.Speed.Y < 0) e.Speed.Y *= -1;

                Canvas.SetLeft(e.Target, p.X + e.Speed.X);
                Canvas.SetTop(e.Target, p.Y + e.Speed.Y);

                e.Spin += e.SpinSpeed;

                e.Target.RenderTransform = new RotateTransform(e.Spin);
            }
        }

        
    }
}
