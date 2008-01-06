using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core
{
    /// <summary>
    /// Random Helper methods
    /// </summary>
    static class RandomHelper
    {
        /// <summary>
        /// Basic static constructor
        /// </summary>
        static RandomHelper()
        {
            random = new Random((int) DateTime.Now.Ticks);
        }

        /// <summary>
        /// Get the default random instance
        /// </summary>
        static public Random Random
        {
            get { return random; }
        }

        /// <summary>
        /// Get a random boolean
        /// </summary>
        /// <returns></returns>
        static public bool RandomBool()
        {
            return random.Next() >= int.MaxValue/2;
        }


        /// <summary>
        /// Get a random boolean
        /// </summary>
        /// <returns></returns>
        static public bool RandomBool(int Percentage)
        {
            return random.Next(0, 100) <= Percentage;
        }

        /// <summary>
        /// Select randomly from a list, with null protection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aList"></param>
        /// <returns></returns>
        static public T Select<T>(IList<T> aList)
        {
            if (aList == null || aList.Count == 0) return default(T);
            return aList[random.Next(0, aList.Count)];
        }

        private static Random random;
    }
}
