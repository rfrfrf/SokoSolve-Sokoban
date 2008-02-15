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
        /// All Doors
        /// </summary>
        public List<Door> Doors
        {
            get { return doors; }
        }

        /// <summary>
        /// All Rooms
        /// </summary>
        public List<Room> Rooms
        {
            get { return rooms; }
        }

        /// <summary>
        /// All Doors in bitmap form
        /// </summary>
        public SolverBitmap DoorBitmap
        {
            get { return doorBitmap; }
        }

        /// <summary>
        /// Find the puzzles Doors and Rooms
        /// </summary>
        public void Analyse()
        {
            controller.DebugReport.AppendHeading(1, "Room Analysis -- Doors");
            AnalyseDoors();
            controller.DebugReport.CompleteSection();

            controller.DebugReport.AppendHeading(1, "Room Analysis -- Rooms");
            AnalyseRooms();
            controller.DebugReport.CompleteSection();

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
                        Bitmap boundry = controller.StaticAnalysis.BoundryMap.BitwiseOR(doorBitmap);
                        FloodFillStrategy floodfill = new FloodFillStrategy(boundry, exit);

                        Evaluator<LocationNode> eval = new Evaluator<LocationNode>();
                        eval.Evaluate(floodfill);

                        // Set region from floodfill
                        newRoom.Set(floodfill.Result);
                        newRoom[exit] = true; // Make sure the exit is also in the room

                        newRoom.Goals = newRoom.BitwiseAND(controller.StaticAnalysis.GoalMap).Count;
                        if (newRoom.Goals == 0)
                        {
                            newRoom.Roomtype = Room.RoomTypes.StorageRoom;
                        }
                        else
                        {
                            newRoom.Roomtype = Room.RoomTypes.GoalRoom;
                        }

                        // Human Readable name
                        newRoom.Name = newRoom.Description;

                        // Link to other doors
                        LinkDoors(newRoom);

                        // Add it the size if more than one floor space
                        if (newRoom.Count > 1)
                        {
                            rooms.Add(newRoom);

                            
                            controller.DebugReport.AppendHeading(3,"{0} Room ({1}) found with {2} goals:", newRoom.Roomtype, newRoom.Name, newRoom.Goals);
                            controller.DebugReport.Append("Doors: {0}",  StringHelper.Join(newRoom.Doors, 
                                delegate(Door item)
                                    {
                                        return string.Format("{0}({2})@{1}", item.Type, item.Position, item.DoorID);
                                    }, ", "));
                            controller.DebugReport.Append(newRoom.ToString());
                            controller.DebugReport.CompleteSection();
                        }
                    }
                }
            }
            controller.DebugReport.CompleteSection();
        }

        /// <summary>
        /// Find other doors that the room links too.
        /// </summary>
        /// <param name="room"></param>
        private void LinkDoors(Room room)
        {
            foreach (Door door in doors)
            {
                foreach (VectorInt exit in door.Exits)
                {
                    if (room[exit])
                    {
                        if (!room.Doors.Contains(door))
                        {
                            room.Doors.Add(door);
                        }
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

            Hint diag1 = new Hint("Diag Door 1", HintsRule.Convert(new string[] { "?FW", "?F?", "WF?" }));
            Hint diag2 = new Hint("Diag Door 2", GeneralHelper.Rotate(diag1.HintArray));
            Hint diag3 = new Hint("Diag Door 3", GeneralHelper.Rotate(diag2.HintArray));
            Hint diag4 = new Hint("Diag Door 4", GeneralHelper.Rotate(diag3.HintArray));

            Hint half1 = new Hint("Half Door 1", HintsRule.Convert(new string[] {"?FW", "WF?", "?F?"}));
            Hint half2 = new Hint("Half Door 2", GeneralHelper.Rotate(half1.HintArray));
            Hint half3 = new Hint("Half Door 3", GeneralHelper.Rotate(half2.HintArray));
            Hint half4 = new Hint("Half Door 4", GeneralHelper.Rotate(half3.HintArray));

            List<Hint> doorHints = new List<Hint>();
            doorHints.Add(basicVert);
            doorHints.Add(basicHorz);
            doorHints.Add(diag1);
            doorHints.Add(diag2);
            doorHints.Add(diag3);
            doorHints.Add(diag4);
            doorHints.Add(half1);
            doorHints.Add(half2);
            doorHints.Add(half3);
            doorHints.Add(half4);

            // Find Matches
            foreach (Hint hint in doorHints)
            {
                hint.PreProcess(controller.StaticAnalysis);
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
                            newDoor.SourceHint = hint;

                            doorIDCounter++; 
                            
                            if (hint == basicHorz) newDoor.Type = Door.Types.Basic;
                            else if (hint == basicVert) newDoor.Type = Door.Types.Basic;
                            else if (hint == half1) newDoor.Type = Door.Types.Offset;
                            else if (hint == half2) newDoor.Type = Door.Types.Offset;
                            else if (hint == half3) newDoor.Type = Door.Types.Offset;
                            else if (hint == half4) newDoor.Type = Door.Types.Offset;
                            else if (hint == diag1) newDoor.Type = Door.Types.Diagonal;
                            else if (hint == diag2) newDoor.Type = Door.Types.Diagonal;
                            else if (hint == diag3) newDoor.Type = Door.Types.Diagonal;
                            else if (hint == diag4) newDoor.Type = Door.Types.Diagonal;

                            // Find door sides
                            VectorInt exit = newDoor.Position.Offset(Direction.Up);
                            if (controller.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);
                            exit = newDoor.Position.Offset(Direction.Down);
                            if (controller.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);
                            exit = newDoor.Position.Offset(Direction.Left);
                            if (controller.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);
                            exit = newDoor.Position.Offset(Direction.Right);
                            if (controller.StaticAnalysis.FloorMap[exit]) newDoor.Exits.Add(exit);

                            controller.DebugReport.Append("Found door {0} of type {1} at {2}, with exits at {3} exits", newDoor.DoorID, newDoor.Type, newDoor.Position, StringHelper.Join<VectorInt>(newDoor.Exits, null, ", "));

                            doors.Add(newDoor);
                        }
                    }
                }
            }

            controller.DebugReport.AppendHeading(2, "Final Door Map:");
            controller.DebugReport.Append(doorBitmap.ToString());
            controller.DebugReport.CompleteSection();
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