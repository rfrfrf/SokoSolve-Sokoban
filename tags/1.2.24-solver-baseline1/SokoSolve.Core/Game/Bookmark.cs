using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Game
{
    /// <summary>
    /// A way point is a partially complete puzzle that a user may choose to return to a.k.a bookmark
    /// </summary>
    public class Bookmark
    {
        public SokobanMap Current
        {
            get { return current; }
            set { current = value; }
        }

        public Path PlayerMoves
        {
            get { return playerMoves; }
            set { playerMoves = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private SokobanMap current;
        private Path playerMoves;
        private string name;
    }
}
