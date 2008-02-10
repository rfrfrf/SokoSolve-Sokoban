using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis
{
    /// <summary>
    /// An individual room, is defined as a unque playing region, with one for more exits(doors). A door is defined as
    /// an entrance with only 1 cell in size. Doors may be shared between rooms. A room may be created by two or more 
    /// connected doors.
    /// </summary>
    public class Room : SolverBitmap
    {
        /// <summary>
        /// Different room types Tunnel vs. Room, Goal vs. Storage
        /// </summary>
        public enum RoomTypes
        {
            StorageRoom,
            GoalRoom,
            Tunnel,
            GoalTunnel
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Room name</param>
        /// <param name="aSize">MapSize</param>
        public Room(string name, SizeInt aSize) : base(name, aSize)
        {
            doors = new List<Door>();
        }

        /// <summary>
        /// The connection door/exits in the room
        /// </summary>
        public List<Door> Doors
        {
            get { return doors; }
        }

        /// <summary>
        /// The room type
        /// </summary>
        public RoomTypes Roomtype
        {
            get { return roomtype; }
            set { roomtype = value; }
        }

        /// <summary>
        /// The numer of goal positions inside the room
        /// </summary>
        public int Goals
        {
            get { return goals; }
            set { goals = value; }
        }

        /// <summary>
        /// Get the direction used to enter the room from a door
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        public Direction GetRoomDirection(Door door)
        {
            foreach (VectorInt exit in door.Exits)
            {
                if (this[exit])
                {
                    return VectorInt.GetDirection(door.Position, exit);
                }
            }
            throw new InvalidOperationException();
        }

        private List<Door> doors;
        private RoomTypes roomtype;
        private int goals;

        
    }

    
}
