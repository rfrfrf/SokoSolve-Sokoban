using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Common.Structures.Evaluation;
using SokoSolve.Core.Analysis.DeadMap;

namespace SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis
{
    /// <summary>
    /// This class can take a puzzle and break it down into smaller self-contained elements.
    /// </summary>
    public class RoomAnalysis
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="controller">Controller/Owner</param>
        public RoomAnalysis(SolverController controller)
        {
            this.controller = controller;
            doors = new List<Door>();
            rooms = new List<Room>();
            doorBitmap = new SolverBitmap("Doors", controller.Map.Size);
        }

        /// <summary>
        /// Find the puzzles Doors and Rooms
        /// </summary>
        public void Analyse()
        {
            AnalyseDoors();
            AnalyseRooms();
        }

        /// <summary>
        /// Find Rooms based on door positions
        /// </summary>
        private void AnalyseRooms()
        {
            int roomID = 0;
            foreach (Door door in doors)
            {
                foreach (VectorInt exit in door.Exits)
                {
                    // Does this belong to a room already?
                    if (FindRooms(exit.X, exit.Y) == null)
                    {
                        roomID++;
                        Room newRoom = new Room("R" + roomID.ToString(), controller.Map.Size);
                        newRoom.Doors.Add(door);

                        // Set Room region
                        Bitmap boundry = controller.Strategy.StaticAnalysis.BoundryMap.BitwiseOR(doorBitmap);
                        FloodFillStrategy floodfill = new FloodFillStrategy(boundry, exit);

                        Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
                        eval.Evaluate(floodfill);

                        // Set region from floodfill
                        newRoom.Set(floodfill.Result);

                        newRoom.Goals = newRoom.BitwiseAND(controller.Strategy.StaticAnalysis.GoalMap).Count;
                        if (newRoom.Goals == 0)
                        {
                            newRoom.Roomtype = Room.RoomTypes.StorageRoom;
                        }
                        else
                        {
                            newRoom.Roomtype = Room.RoomTypes.GoalRoom;
                        }

                        // Add it the size if more than one floor space
                        if (newRoom.Count > 1) rooms.Add(newRoom);
                    }
                }
            }
        }

        /// <summary>
        /// Find all doors
        /// </summary>
        private void AnalyseDoors()
        {
            // List of 3x3 map, of which the middle cell is a door if all other positions match
            Hint basicHorz = new Hint("Basic Door Horz", HintsRule.Convert(new string[] {"?F?", "WFW", "?F?"}));
            Hint basicVert = new Hint("Basic Door Vert", HintsRule.Convert(new string[] {"?W?", "FFF", "?W?"}));

            Hint half1 = new Hint("Half Door 1", HintsRule.Convert(new string[] {"?FW", "WF?", "???"}));
            Hint half2 = new Hint("Half Door 2", GeneralHelper.Rotate(half1.HintArray));
            Hint half3 = new Hint("Half Door 3", GeneralHelper.Rotate(half2.HintArray));
            Hint half4 = new Hint("Half Door 4", GeneralHelper.Rotate(half3.HintArray));

            List<Hint> doorHints = new List<Hint>();
            doorHints.Add(basicVert);
            doorHints.Add(basicHorz);
            doorHints.Add(half1);
            doorHints.Add(half2);
            doorHints.Add(half3);
            doorHints.Add(half4);

            // Find Matches
            foreach (Hint hint in doorHints)
            {
                hint.PreProcess(controller.Strategy.StaticAnalysis);
            }

            // Process matches
            int doorIDCounter = 0;
            foreach (Hint hint in doorHints)
            {
                if (hint.HasPreProcessedMatches)
                {
                    foreach (VectorInt checkLocation in hint.CheckLocations)
                    {
                        if (FindDoor(checkLocation) == null)
                        {
                            // Add new door
                            Door newDoor = new Door();
                            newDoor.Position = checkLocation.Add(1, 1); // Center of the hint is the door
                            doorBitmap[newDoor.Position] = true;
                            newDoor.DoorID = "D" + doorIDCounter.ToString();
                            doorIDCounter++;

                            // Find door sides
                            VectorInt exit = newDoor.Position.Offset(Direction.Up);
                            if (controller.Strategy.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);
                            exit = newDoor.Position.Offset(Direction.Down);
                            if (controller.Strategy.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);
                            exit = newDoor.Position.Offset(Direction.Left);
                            if (controller.Strategy.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);
                            exit = newDoor.Position.Offset(Direction.Right);
                            if (controller.Strategy.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);

                            Console.WriteLine("Found door {0} at {1}, with exits at {2} exits", doorIDCounter, newDoor.Position, StringHelper.Join<VectorInt>(newDoor.Exits, null, ", "));

                            doors.Add(newDoor);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find a door by coordinate
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Door FindDoor(VectorInt position)
        {
            foreach (Door door in doors)
            {
                if (door.Position == position) return door;
            }
            return null;
        }

        /// <summary>
        /// Check if a position is a door
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public bool IsDoor(int X, int Y)
        {
            return FindDoor(new VectorInt(X, Y)) != null;
        }

        /// <summary>
        /// Based on a position find a room
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public List<Room> FindRooms(int X, int Y)
        {
            if (rooms == null || rooms.Count == 0) return null;

            List<Room> result = new List<Room>();
            foreach (Room room in rooms)
            {
                if (room[X, Y]) result.Add(room);
            }
            if (result.Count == 0) return null;
            return result;
        }


        private List<Door> doors;
        private List<Room> rooms;
        private SolverController controller;
        private SolverBitmap doorBitmap;
    }
}