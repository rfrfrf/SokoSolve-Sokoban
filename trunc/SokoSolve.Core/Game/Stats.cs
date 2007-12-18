using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Game
{
    /// <summary>
    /// Basic Namespace for all stats
    /// </summary>
    public class Stats
    {
        public DateTime Start;
        public DateTime End;
        public int Moves;
        public int MovesTotal;
        public int Pushes;
        public int PushesTotal;
        public int Undos;
        public int Restarts;
        public TimeSpan Time;
        public TimeSpan TotalTime;

        public TimeSpan Duration
        {
            get
            {
                if (Start != DateTime.MinValue)
                {
                    if (End != DateTime.MinValue)
                    {
                        return End - Start;
                    }
                    else
                    {
                        return DateTime.Now - Start;
                    }
                }
                return new TimeSpan(0);
            }
        }
    }
}
