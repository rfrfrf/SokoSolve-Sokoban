using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
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
        protected SolverProgress store = new SolverProgress();

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

        public void Add(PuzzleMap map)
        {
            string[] uniform = GenerateUniformMap(map.Map);
            if (Contains(uniform)) return;

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
