using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.DataModel
{
    public class Identity
    {
        public Identity(int aID)
        {
            pID = aID;
        }
        private int pID;

        public int ID
        {
            get { return pID; }
            set { pID = value; }
        }

        public override bool Equals(object obj)
        {
            Identity i = obj as Identity;
            if (i == null) return false;
            return pID == i.ID;
        }

        public override int GetHashCode()
        {
            return pID;
        }
	
    }
}
