using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common
{
    public static class GeneralHelper
    {
        /// <summary>
        /// A safe equality checking helper. Handles null checking, object reference checking, by value checking
        /// </summary>
        /// <param name="lhs">LHS</param>
        /// <param name="rhs">RHS</param>
        /// <returns>true is same</returns>
        public static bool StaticEqualityCheckHelper(object lhs, object rhs)
        {
            bool isNullA = object.ReferenceEquals(lhs, null);
            bool isNullB = object.ReferenceEquals(rhs, null);

            // Both null?
            if (isNullA && isNullB) return true;

            // One or the other null?
            if (isNullA && !isNullB) return false;
            if (!isNullA && isNullB) return false;

            // Same reference?
            if (object.ReferenceEquals(lhs, rhs)) return true;

            // Neither are null, and they are not the same memory reference...Do a value comparison
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// A safe equality checking helper. Handles null checking, object reference checking, by value checking.
        /// This method us useful for IEqualityComparer<T> implementations
        /// </summary>
        /// <param name="lhs">LHS</param>
        /// <param name="rhs">RHS</param>
        /// <returns>null implies lhs, rhs are not null, are are not the same reference</returns>
        public static bool? StaticReferenceCheckHelper(object lhs, object rhs)
        {
            bool isNullA = object.ReferenceEquals(lhs, null);
            bool isNullB = object.ReferenceEquals(rhs, null);

            // Both null?
            if (isNullA && isNullB) return true;

            // One or the other null?
            if (isNullA && !isNullB) return false;
            if (!isNullA && isNullB) return false;

            // Same reference?
            if (object.ReferenceEquals(lhs, rhs)) return true;

            // Neither are null, and they are not the same memory reference...Do a value comparison
            return null;
        }

        /// <summary>
        /// Rotate an array left-to-right by 90deg
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[,] Rotate<T>(T[,] source)
        {
            int sizeX = source.GetLength(0);
            int sizeY = source.GetLength(1);
            T[,] result = new T[sizeY, sizeX]; // swap dimensions

            for (int cx = 0; cx < sizeX; cx++)
            {
                for (int cy = 0; cy < sizeY; cy++)
                {
                    result[sizeY - cy - 1, cx] = source[cx, cy];
                }
            }

            return result;
        }
    }
}
