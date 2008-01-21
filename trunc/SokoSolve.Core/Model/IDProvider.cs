using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Model
{
    /// <summary>
    /// Utility class to create unique IDs
    /// </summary>
    public class IDProvider
    {
        private object locker = new object();
        private int maxID = 0;

        /// <summary>
        /// Strong Constructor
        /// </summary>
        /// <param name="maxID">Current Max (next avail ID)</param>
        public IDProvider(int maxID)
        {
            this.maxID = maxID;
        }

        /// <summary>
        /// Rather user GetNextID for assigning values
        /// </summary>
        /// <returns></returns>
        public int GetCurrentID()
        {
            return maxID;
        }

        /// <summary>
        /// Get the next valid ID
        /// </summary>
        /// <returns></returns>
        public int GetNextID()
        {
            lock (locker)
            {
                return maxID++;
            }
        }

        /// <summary>
        /// The the next ID and use a string.format layout. Ex "XX{0}" will give an XX prefix to the number
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public string GetNextIDString(string prefix)
        {
            return prefix == null ? GetNextID().ToString() : string.Format(prefix, GetNextID());
        }
    }
}