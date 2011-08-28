using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SokoSolve.Common.Math;

namespace SoloSolve.UI.WPF
{
    public static class Extensions
    {
        public static System.Windows.Point ToWindowsPoint(this VectorInt p)
        {
            return new Point(p.X, p.Y);
        }
    }
}
