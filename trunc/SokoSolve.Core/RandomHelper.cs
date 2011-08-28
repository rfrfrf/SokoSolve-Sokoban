using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core
{
    /// <summary>
    /// Random Helper methods
    /// </summary>
    public static class RandomHelper
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

        /// <summary>
        /// Select randomly from a list, with null protection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aList"></param>
        /// <returns></returns>
        static public IEnumerable<T> Select<T>(IList<T> aList, int count)
        {
            if (aList == null || aList.Count == 0) return new T[0];

            if (count == 1)
            {
                return new T[] { Select(aList) };   
            }
            else
            {
                throw new NotImplementedException();
            }
            
        }

        public static double NextDouble(int from, int to, int fraction)
        {
            int d = 10 ^ fraction;
            return (double)random.Next(from * d, to * d) / (double)d;
        }

        private static Random random;
    }
}
