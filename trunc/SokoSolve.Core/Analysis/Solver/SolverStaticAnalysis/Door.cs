using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;


namespace SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis
{
    /// <summary>
    /// Entrance to a room
    /// </summary>
    public class Door
    {
        public Door()
        {
            exits = new List<VectorInt>();
        }

        /// <summary>
        /// Entrances/Exit to/from the door
        /// </summary>
        public List<VectorInt> Exits
        {
            get { return exits; }
        }

        /// <summary>
        /// Door ID or Name
        /// </summary>
        public string DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }

        /// <summary>
        /// Entrance in a room
        /// </summary>
        public Room RoomA
        {
            get { return roomA; }
            set { roomA = value; }
        }

        /// <summary>
        /// Entrance in a room
        /// </summary>
        public Room RoomB
        {
            get { return roomB; }
            set { roomB = value; }
        }

        /// <summary>
        /// Door position
        /// </summary>
        public VectorInt Position
        {
            get { return position; }
            set { position = value; }
        }

        private string doorID;
        private Room roomA;
        private Room roomB;
        private VectorInt position;
        private List<VectorInt> exits;
    }
}
