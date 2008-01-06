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
    }
}
