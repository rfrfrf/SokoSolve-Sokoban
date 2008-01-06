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

        public override string ToString()
        {
            return string.Format("Moves {0}/{1}, Pushes {2}/{3}, Undos {4}, Restarts {5}, Duration {6}",
                                 Moves, MovesTotal,
                                 Pushes, PushesTotal,
                                 Undos,
                                 Restarts,
                                 Duration.ToString());
        }
    }
}
