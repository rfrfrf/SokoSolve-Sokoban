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
        /// <summary>
        /// Strong Constructor
        /// </summary>
        /// <param name="maxID">Current Max (next avail ID)</param>
        public IDProvider(int maxID)
        {
            this.maxID = maxID;
        }

        private int maxID = 0;
        private object locker = new object();

        public int GetNextID()
        {
            lock(locker)
            {
                return maxID++;    
            }
        }

        public string GetNextIDString(string prefix)
        {
            return prefix == null ? GetNextID().ToString() : string.Format(prefix, GetNextID());
        }
    }
}
