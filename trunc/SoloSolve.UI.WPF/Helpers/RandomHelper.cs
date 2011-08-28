using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoloSolve.UI.WPF.Helpers
{
    public static class RandomHelper
    {
        static Random random = new Random();

        public static double NextDouble(int from, int to, int fraction)
        {
            int d = 10 ^ fraction;
            return (double)random.Next( from*d, to*d) / (double)d;
        }
    }
}
