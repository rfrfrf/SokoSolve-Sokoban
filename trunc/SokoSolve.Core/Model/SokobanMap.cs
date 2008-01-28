using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Model
{
    /// <summary>
    /// Encapsulates a complete logical Sokoban puzzle map, but only includes data/cell manipulation
    /// routines. There should be no actual game logic here. <see cref="Game"/>. <see cref="Puzzle"/>
    /// </summary>
    /// <remarks>
    /// Author: Guy Langston.
    /// This is the basis of the Sokoban puzzle.
    /// </remarks>
    public class SokobanMap
    {
        /// <summary>
        /// Standard internet sequence
        /// </summary>
        /// <remarks>Sequence is Void, Wall, Floor, Crate, Goal, CrateAndGoal, Player, PlayerAndGoal</remarks>
        public static readonly string InternetChars = "~# $.*@+";

        /// <summary>
        /// Standard SokoSolve SSX xml encoding.
        /// </summary>
        /// <remarks>Sequence is Void, Wall, Floor, Crate, Goal, CrateAndGoal, Player, PlayerAndGoal</remarks>
		public  static readonly string StandardEncodeChars = "~#.XO$P*";

    

        /// <summary>
        /// Map array
        /// </summary>
		CellStates[,] map;

        /// <summary>
        /// Default Construction
        /// </summary>
        public SokobanMap()
        {
            Init(new SizeInt(10, 10));
            this[3, 3] = CellStates.FloorPlayer;
            this[4, 4] = CellStates.FloorGoal;
            this[5, 5] = CellStates.FloorCrate;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="copy"></param>
        public SokobanMap(SokobanMap copy)
        {
            map = new CellStates[copy.Size.X, copy.Size.Y];
            for (int px = 0; px < Size.X; px++)
                for (int py = 0; py < Size.Y; py++)
                    this[new VectorInt(px, py)] = copy[new VectorInt(px, py)];
        }

        /// <summary>
        /// The current player position.
        /// NULL if no player position is defined.
        /// </summary>
        public VectorInt Player
        {
            get
            {
                SizeInt s = Size;
                for (int px = 0; px < s.X; px++)
                    for (int py = 0; py < s.Y; py++)
                    {
                        if (this[px, py] == CellStates.FloorPlayer || this[px, py] == CellStates.FloorGoalPlayer)
                        {
                            return new VectorInt(px, py);
                        }
                    }
                return VectorInt.Null;
            }
        }
        
        /// <summary>
        /// Return the cell state as a position
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public CellStates this[VectorInt P]
        {
            get
            {
                if (P.IsNull) throw new ArgumentNullException("P");
                if (!Rectangle.Contains(P)) throw new ArgumentOutOfRangeException("P");
                return map[P.X, P.Y];
            }
            set
            {
                if (!Rectangle.Contains(P)) throw new ArgumentOutOfRangeException("P");

                // Remove the other player position is it exists
                if ((value == CellStates.FloorPlayer || value == CellStates.FloorGoalPlayer) && !Player.IsNull)
                {
                    // Remove the old player
                    if (this[Player] == CellStates.FloorPlayer)
                    {
                        this[Player] = CellStates.Floor;
                    }
                    else if (this[Player] == CellStates.FloorGoalPlayer) 
                    {
                        this[Player] = CellStates.FloorGoal;
                    }
                   
                    // Set the new state
                    map[P.X, P.Y] = value;  
                }
                else
                {
                    map[P.X, P.Y] = value;    
                }
                
            }
        }

        /// <summary>
        /// Return the cell state as a position
        /// </summary>
        public CellStates this[int pX, int pY]
        {
            get
            {
                return map[pX, pY];
            }
            set
            {
                this[new VectorInt(pX, pY)] = value;
            }
        }

        /// <summary>
        /// Get the puzzles size
        /// </summary>
        public SizeInt Size
        {
            get { return new SizeInt(map.GetLength(0), map.GetLength(1)); }
        }

        /// <summary>
        /// Return the puzzle size as a Rectangle
        /// </summary>
        public RectangleInt Rectangle
        {
            get
            {
				return new RectangleInt(VectorInt.Zero, new VectorInt(Size.X - 1, Size.Y - 1));
            }
        }

        

        /// <summary>
        /// Set State based on the fundemental cell type
        /// </summary>
        /// <param name="P"></param>
        /// <param name="aCell"></param>
        public void setState(VectorInt P, Cell aCell)
        {
            if (aCell == Cell.Void) this[P] = CellStates.Void;
            if (aCell == Cell.Wall) this[P] = CellStates.Wall;
            if (aCell == Cell.Floor) this[P] = CellStates.Floor;

            if (aCell == Cell.Crate)
                if (this[P] == CellStates.FloorGoal) this[P] = CellStates.FloorGoalCrate;
                else this[P] = CellStates.FloorCrate;

            if (aCell == Cell.Goal)
                if (this[P] == CellStates.FloorPlayer) this[P] = CellStates.FloorGoalPlayer;
                else if (this[P] == CellStates.FloorCrate) this[P] = CellStates.FloorGoalCrate;
                else this[P] = CellStates.FloorGoal;

            if (aCell == Cell.Player)
                if (this[P] == CellStates.FloorGoal) this[P] = CellStates.FloorGoalPlayer;
                else this[P] = CellStates.FloorPlayer;
        }

        

        /// <summary>
        /// Get the list of cell's at this postision
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public Cell[] getCells(VectorInt P)
        {
            return Cell2CellStatesMap[(int)this[P]];
        }

        /// <summary>
        /// Does a position have a cell
        /// </summary>
        /// <param name="P"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public bool isCell(VectorInt P, Cell C)
        {
            return Array.IndexOf(getCells(P), C) >= 0;
        }

        /// <summary>
        /// Void, Wall, Floor, FloorCrate,FloorGoal,FloorGoalCrate,FloorPlayer,FloorGoalPlayer
        /// </summary>
        Cell[][] Cell2CellStatesMap = new Cell[][] 
            { new Cell[] {Cell.Void },
                new Cell[] { Cell.Wall },
                new Cell[] { Cell.Floor },
                new Cell[] { Cell.Floor, Cell.Crate },
                new Cell[] { Cell.Floor, Cell.Goal },
                new Cell[] { Cell.Floor, Cell.Crate, Cell.Goal },
                new Cell[] { Cell.Floor, Cell.Player },
                new Cell[] { Cell.Floor, Cell.Player, Cell.Goal } };

        /// <summary>
        /// Count the number of cellstate found in the entire puzzle
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public int Count(CellStates cs)
        {
            int c = 0;
            for (int x=0; x<Size.X; x++)
                for (int y = 0; y < Size.Y; y++)
                {
                    if (this[x, y] == cs) c++;
                }
            return c;
        }

        /// <summary>
        /// Count the number of cell's found in the entire puzzle
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public int Count(Cell cs)
        {
            int c = 0;
            for (int x = 0; x < Size.X; x++)
                for (int y = 0; y < Size.Y; y++)
                {
                    if (Array.IndexOf(getCells(new VectorInt(x, y)), cs) >= 0) c++;
                }
            return c;
        }

        /// <summary>
        /// Initialise the puzzle to a set size.
        /// </summary>
        /// <param name="s"></param>
        /// <remarks>
        /// Create an empty puzzle with a wall around the edges
        /// </remarks>
        public void Init(SizeInt s)
        {
            if (s.X <= 0 || s.Y <= 0) throw new ArgumentOutOfRangeException("s");
            map = new CellStates[s.X, s.Y];
            Fill(CellStates.Floor);
            FillBox(Rectangle, CellStates.Wall);
        }

        

        /// <summary>
        /// Fill a region <paramref name="R"/> with state <paramref name="C"/>
        /// </summary>
        /// <param name="R">Region</param>
        /// <param name="C">Cell state to fill with</param>
        public void Fill(RectangleInt R, CellStates C)
        {
            for (int px=R.TopLeft.X; px<=R.BottomRight.X; px++)
                for (int py = R.TopLeft.Y; py <=R.BottomRight.Y; py++)
                {
                    this[new VectorInt(px, py)] = C;
                }
        }

        /// <summary>
        /// Fill the entire puzzle with <paramref name="C"/>
        /// </summary>
        /// <param name="C"></param>
        public void Fill(CellStates C)
        {
            Fill(Rectangle, C);
        }

        /// <summary>
        /// Fill a box, not the insides with a CellState
        /// </summary>
        /// <param name="R"></param>
        /// <param name="C"></param>
        public void FillBox(RectangleInt R, CellStates C)
        {
            for (int px = R.TopLeft.X; px <= R.BottomRight.X; px++)
                for (int py = R.TopLeft.Y; py <= R.BottomRight.Y; py++)
                {
                    if (px == R.TopLeft.X || py == R.TopLeft.Y || px == R.BottomRight.X || py == R.BottomRight.Y)
                    {
                        this[new VectorInt(px, py)] = C;
                    }
                }
        }

        /// <summary>
        /// Generate a bitmap for a particular cell state
        /// </summary>
        /// <param name="cell">Search for</param>
        /// <returns></returns>
        public Bitmap ToBitmap(CellStates cell)
        {
            SizeInt size = Size;

            Bitmap result = new Bitmap(size);
            for (int cx = 0; cx < size.X; cx++)
                for (int cy = 0; cy < size.Y; cy++)
                {
                    if (this[cx, cy] == cell) result[cx, cy] = true;
                }

            return result;
        }

        /// <summary>
        /// Clean up a map, converting outner cells to void
        /// </summary>
        public void ApplyVoidCells()
        {
            // Do each Row
            for(int yy=0; yy<Size.Y; yy++)
            {
                for (int xx=0; xx<Size.X; xx++)
                {
                    if (this[xx, yy] == CellStates.Floor || this[xx,yy] == CellStates.Void)
                    {
                        // Convert to void
                        this[xx, yy] = CellStates.Void;
                    }
                    else
                    {
                        break;    
                    }
                    
                }

                for (int xx = Size.X-1; xx> -1; xx--)
                {
                    if (this[xx, yy] == CellStates.Floor || this[xx,yy] == CellStates.Void)
                    {
                        // Convert to void
                        this[xx, yy] = CellStates.Void;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Do each Row
            for (int xx = 0; xx < Size.X; xx++)
            {
                for (int yy = 0; yy < Size.Y; yy++)
                {
                    if (this[xx, yy] == CellStates.Floor || this[xx, yy] == CellStates.Void)
                    {
                        // Convert to void
                        this[xx, yy] = CellStates.Void;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int yy = Size.Y - 1; yy > -1; yy--)
                {
                    if (this[xx, yy] == CellStates.Floor || this[xx, yy] == CellStates.Void)
                    {
                        // Convert to void
                        this[xx, yy] = CellStates.Void;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Resize the puzzle
        /// </summary>
        /// <param name="newSize"></param>
        public void Resize(SizeInt newSize)
        {
            if (newSize.Equals(Size)) return;
            CellStates[,] old = map;
            

            // Min common size
            SizeInt minMap = SizeInt.Min(Size, newSize);
            Init(newSize);

            for (int cx=0; cx<minMap.X; cx++)
                for (int cy = 0; cy < minMap.Y; cy++)
                {
                    this[cx, cy] = old[cx, cy];
                }
        }

        /// <summary>
        /// Init the puzzle from a simple string map, with the default cell chars
        /// </summary>
        public void SetFromString(string LinesWithBreaks)
        {
            SetFromStrings(StringHelper.Split(LinesWithBreaks, Environment.NewLine));
        }

        /// <summary>
        /// Init the puzzle from a simple string map, with the default cell chars
        /// </summary>
        /// <param name="InputMap"></param>
        public void SetFromStrings(string[] InputMap)
        {
            SetFromStrings(InputMap, StandardEncodeChars);
        }

        /// <summary>
        /// Init the puzzle from a simple string map
        /// </summary>
        /// <param name="InputMap"></param>
        /// <param name="ValidChars">Void, Wall, Floor, Crate, Goal, CrateAndGoal, Player, PlayerAndGoal</param>
        public void SetFromStrings(string[] InputMap, string ValidChars)
        {
            int size = 8;
            string[] simple = new string[size];
            for(int c=0; c<CellStatesClass.Size; c++)
                simple[c] = new string(ValidChars[c], 1);

            SetFromStrings(InputMap, simple);
        }

        /// <summary>
        /// Init the puzzle from a simple string map
        /// </summary>
        public void SetFromStrings(string[] StrMap, string[] ValidChars)
        {
            int maxx = 0;

            // Clean up
            List<string> InputMap = new List<string>(StrMap.Length);
            foreach (string row in StrMap)
            {
                if ( row == null) continue;
                string cleanRow = row.TrimEnd();
                if (cleanRow.Length == 0) continue;
                if (cleanRow.Length > maxx) maxx = cleanRow.Length;
                InputMap.Add(cleanRow);
            }


            SizeInt sz = new SizeInt(maxx, InputMap.Count);
            Init(sz);

            for(int cx=0; cx<sz.X; cx++)
                for (int cy = 0; cy < sz.Y; cy++)
                {
                    // Default value
                    this[cx, cy] = CellStates.Void;

                    if (InputMap.Count <= cy) continue;
                    if (InputMap[cy].Length <= cx) continue;
                    

                    for (int cell = 0; cell < CellStatesClass.Size; cell++)
                    {
                        if (ValidChars[cell].IndexOf(InputMap[cy][cx]) >= 0)
                        {
                            // Hit
                            this[cx, cy] = (CellStates)cell;
                        }
                    }
                }
        }


        /// <summary>
        /// Encode a row as a string using a char to represent each char
        /// </summary>
        /// <param name="ColY"></param>
        /// <param name="StateChars"></param>
        /// <returns></returns>
        public string EncodeRow(int ColY, string StateChars)
        {
            if (StateChars.Length != Enum.GetValues(typeof(CellStates)).Length) throw new ArgumentException("StateChars");

            StringBuilder sb = new StringBuilder(Size.X);
            for (int cx = 0; cx < Size.X; cx++)
            {
                sb.Append(StateChars[(int)map[cx, ColY]]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Is the puzzle complete.
        /// Rule:Are all crates on a goal position.
        /// </summary>
        /// <returns></returns>
        public bool isPuzzleComplete()
        {
            return Count(CellStates.FloorCrate) == 0;
        }

        /// <summary>
        /// Is the puzzle valid
        /// </summary>
        /// <param name="Messages"></param>
        /// <returns></returns>
        public bool IsValid(out StringCollection Messages)
        {
            // Init.
            Messages = new StringCollection();

            // Valid Puzzle.
            bool result = true;

            // (1) Is there a single player position?
            int cc = Count(CellStates.FloorPlayer) + Count(CellStates.FloorGoalPlayer);
            if (cc != 1)
            {
                result = false;
                Messages.Add(String.Format("Check 1. There must be a single player start location. Player positions found={0}", cc));
            }

            // (2) The number of goals must be => crates, and there most be one crate.
            cc = Count(Cell.Crate);
            int gg = Count(Cell.Goal);
            if (cc > gg || cc <= 0) 
            {
                result = false;
                Messages.Add(String.Format("Check 2. The number of goals must be => crates, and there most be one crate. Crates found={0}, Goals found={1}", cc, gg));
            }

            // (3) The puzzle must be enclosed with walls, void cells cannot be within the puzzle
            

            return result;
        }

        /// <summary>
        /// Convert logic cell's to combination cell states
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static CellStates Convert(Cell cell)
        {
            switch(cell)
            {
                case (Cell.Void): return CellStates.Void;
                case (Cell.Wall): return CellStates.Wall;
                case (Cell.Floor): return CellStates.Floor;
                case (Cell.Crate): return CellStates.FloorCrate;
                case (Cell.Goal): return CellStates.FloorGoal;
                case (Cell.Player): return CellStates.FloorPlayer;
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Convert logic cell's to combination cell states
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Cell Convert(CellStates cell)
        {
            switch (cell)
            {
                case (CellStates.Void): return Cell.Void;
                case (CellStates.Wall): return Cell.Wall;
                case (CellStates.Floor): return Cell.Floor;
                case (CellStates.FloorPlayer): return Cell.Player;
                case (CellStates.FloorGoal): return Cell.Goal;
                case (CellStates.FloorGoalCrate): return Cell.Goal;
                case (CellStates.FloorCrate): return Cell.Crate;
                case (CellStates.FloorGoalPlayer): return Cell.Player;
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Rotate the puzzle by 90degrees
        /// </summary>
        public void Rotate()
        {
            map = GeneralHelper.Rotate<CellStates>(map);
        }

        /// <summary>
        /// Show the map as a string seperated with line breaks
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
            return ToString(null);
		}

        /// <summary>
        /// Show the map as a string seperated with line breaks
        /// </summary>
        /// <returns></returns>
        public string ToString(string SokobanChars)
        {
            string schars = SokobanChars;
            if (string.IsNullOrEmpty(schars)) schars = StandardEncodeChars;
            StringBuilder sb = new StringBuilder();
            for (int cc = 0; cc < Size.Y; cc++)
            {
                sb.Append(EncodeRow(cc, schars));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Show the map as a string seperated with line breaks
        /// </summary>
        /// <returns></returns>
        public  string[] ToStringArray(string SokobanChars)
        {
            string schars = SokobanChars;
            if (string.IsNullOrEmpty(schars)) schars = StandardEncodeChars;
            List<string> res = new List<string>();
            for (int cc = 0; cc < Size.Y; cc++)
            {
                res.Add(EncodeRow(cc, schars));
            }
            return res.ToArray();
        }

	}


}
