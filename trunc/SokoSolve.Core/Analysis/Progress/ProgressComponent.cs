using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Analysis.Solver.SolverStaticAnalysis;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.Analysis;
using SokoSolve.Core.Model.Services;

namespace SokoSolve.Core.Analysis.Progress
{
    /// <summary>
    /// Allow the solver to keep track of 
    /// </summary>
    public class ProgressComponent
    {
        protected SolverProgress store;
        private int storeMaxAttempts;

        public ProgressComponent()
        {
            store = new SolverProgress();
            StoreMaxAttempts = 5;
        }

        public List<SolverPuzzle> Items
        {
            get { return store.Items;  }
        }


        public int StoreMaxAttempts
        {
            get { return storeMaxAttempts; }
            set 
            {
                if (value < 2) throw new ArgumentOutOfRangeException("Must be >= 2");
                storeMaxAttempts = value;
            }
        }

        public void Load(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(SolverProgress));
            using(var fs = File.OpenRead(fileName))
            {
                store = ser.Deserialize(fs) as SolverProgress;    
            }
        }

        public void Save(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(SolverProgress));
            using (var fs = File.OpenWrite(fileName))
            {
                ser.Serialize(fs, store);
            }
        }

        public void Add(Library library)
        {
            foreach (Puzzle puzzle in library.Puzzles)
            {
                foreach (var map in puzzle.Maps)
                {
                    Add(map);        
                }
            }
        }

        public int Count
        {
            get { return store.Items.Count; }
        }

        public SolverPuzzle Add(PuzzleMap map)
        {
            string[] uniform = GenerateUniformMap(map.Map);
            if (Contains(uniform)) return Get(map.Map);

            SolverPuzzle nP = new SolverPuzzle();
            nP.NormalisedMap = uniform;
            nP.Name = string.Format("Library: {0}[{1}], Puzzle: {2}[{3}], MapID: {4}",
                                    map.Puzzle.Library.Details.Name,
                                    map.Puzzle.Library.LibraryID,
                                    map.Puzzle.GetDetails().Name,
                                    map.Puzzle.PuzzleID,
                                    map.MapID);
            nP.Width = map.Map.Size.Width;
            nP.Height = map.Map.Size.Height;
            nP.Author = map.GetDetails().Author.Name;
            nP.Email = map.GetDetails().Author.Email;
            nP.Rating = (float)PuzzleAnalysis.CalcRating(map.Map);
            nP.SourceURL = null;
            if (map.HasSolution)
            {
                nP.BestKnownSolution = Solution.FindBest(map.Solutions).Steps;
            }
            nP.Hash = CalculateHash(nP.NormalisedMap);            
            store.Items.Add(nP);

            return nP;
        }

        public SolverPuzzle Add(SokobanMap map)
        {
            string[] uniform = GenerateUniformMap(map);
            if (Contains(uniform)) return Get(map);

            SolverPuzzle nP = new SolverPuzzle();
            nP.NormalisedMap = uniform;
            nP.Name = "";
            nP.Width = map.Size.Width;
            nP.Height = map.Size.Height;
            nP.Author = "";
            nP.Email = "";
            nP.Rating = (float)PuzzleAnalysis.CalcRating(map);
            nP.SourceURL = null;
          
            nP.Hash = CalculateHash(nP.NormalisedMap);
            store.Items.Add(nP);

            return nP;
        }

        static public ulong CalculateHash(string[] strings)
        {
            ulong res = 0;
            foreach (var row in strings)
            {
                res += (ulong)row.GetHashCode();
            }
            return res;
        }

        public SolverPuzzle Get(SokobanMap Map)
        {
            return Get(GenerateUniformMap(Map));
        }

        /// <summary>
        /// The is a key method. This updates/adds a solver attempt
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public void Update(SolverResult result)
        {
            SolverPuzzle record = Get(result.Map);
            if (record == null)
            {
                record = Add(result.Map);
            }

            List<SolverAttempt> items = record.Attempts.Items;

            SolverAttempt attempt = new SolverAttempt();
            attempt.Created = DateTime.Now;
            attempt.ElapsedTime = TimeSpan.FromSeconds(result.Info.TotalSeconds);
            attempt.SolverResultInfo = result.Info;
            record.Attempts.TotalTime += attempt.ElapsedTime.TotalSeconds;
            if (result.HasSolution)
            {
                record.Attempts.TotalAttemptSucceed++;
                attempt.Solution = Solution.FindBest(result.Solutions).Steps;
            }
            else
            {
                record.Attempts.TotalAttemptFailed++;
            }
            

            record.Attempts.Add(attempt);

            if (record.Attempts.Count > StoreMaxAttempts)
            {
                // Keep the best and the wost
                SolverAttempt best = FindBest(items);
                SolverAttempt worst = FindWorst(items);
                items.Remove(best);
                if (items.Contains(worst)) items.Remove(worst);
                if (items.Contains(attempt)) items.Remove(attempt);
                items.Sort(AttemptOrderDefault);
                items.Reverse();
                while (items.Count > StoreMaxAttempts - 3)
                {
                    items.RemoveAt(0);
                }
                record.Attempts.Add(attempt);
                if (attempt != best) record.Attempts.Add(best);
                if (attempt != worst && worst != best) record.Attempts.Add(worst);
                items.Sort(AttemptOrderDefault);
            }
        }

        private SolverAttempt FindBest(List<SolverAttempt> attempts)
        {
            if (attempts == null || attempts.Count == 0) return null;
            
            SolverAttempt best = attempts[0];
            int bestSolutionLen = best.Solution == null ? -1 : best.Solution.Length;
            TimeSpan bestSolutionTime = best.ElapsedTime;

            foreach (var attempt in attempts)
            {
                if (attempt == best) continue;

                int iLen = attempt.Solution == null ? -1 : attempt.Solution.Length;
                if (iLen < bestSolutionLen)
                {
                    best = attempt;
                    bestSolutionLen = best.Solution == null ? -1 : best.Solution.Length;
                    bestSolutionTime = best.ElapsedTime;
                    continue;
                }

                if (attempt.ElapsedTime < bestSolutionTime)
                {
                    best = attempt;
                    bestSolutionLen = iLen;
                    bestSolutionTime = best.ElapsedTime;
                }
            }

            return best;
        }

        private SolverAttempt FindWorst(List<SolverAttempt> attempts)
        {
            if (attempts == null || attempts.Count == 0) return null;

            SolverAttempt worst = attempts[0];
            int worstLen = worst.Solution == null ? -1 : worst.Solution.Length;
            TimeSpan worstTime = worst.ElapsedTime;

            foreach (var attempt in attempts)
            {
                if (attempt == worst) continue;

                int iLen = attempt.Solution == null ? -1 : attempt.Solution.Length;
                if (iLen > worstLen)
                {
                    worst = attempt;
                    worstLen = worst.Solution == null ? -1 : worst.Solution.Length;
                    worstTime = worst.ElapsedTime;
                    continue;
                }

                if (attempt.ElapsedTime > worstTime)
                {
                    worst = attempt;
                    worstLen = iLen;
                    worstTime = worst.ElapsedTime;
                }
            }

            return worst;
        }

        private int AttemptOrderDefault(SolverAttempt lhs, SolverAttempt rhs)
        {
            if (lhs.Solution == null && rhs.Solution != null) return 1;
            if (lhs.Solution != null && rhs.Solution == null) return -1;
            if (lhs.Solution != null && rhs.Solution != null)
            {
                if (lhs.Solution.Length > rhs.Solution.Length) return 1;
                if (lhs.Solution.Length < rhs.Solution.Length) return -1;
            }

            //TODO... ? I should be much more intelligent about all this...
            // Ie. Quality of properties, etc should be preferred

            return lhs.ElapsedTime.CompareTo(rhs.ElapsedTime);
        }

        


        protected bool Contains(string[] uniformMap)
        {
            return Get(uniformMap) != null;
        }



        protected SolverPuzzle Get(string[] uniformMap)
        {
            return store.Items.Find(x => Match(x.NormalisedMap, uniformMap));   
        }

        public bool Match(string[] lhs, string[] rhs)
        {
            if (lhs.Length != rhs.Length) return false;
            for(int cc=0; cc<lhs.Length; cc++)
            {
                if (lhs[cc] != rhs[cc]) return false;
            }
            return true;
        }

        public string[] GenerateUniformMap(SokobanMap map)
        {
            // TODO: Convert all unreachable floor positions to walls
            return map.ToStringArray(SokobanMap.StandardEncodeChars);
        }
    }
}
