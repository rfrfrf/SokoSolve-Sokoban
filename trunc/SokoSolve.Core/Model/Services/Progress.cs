﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Core.Model.Services
{
    /// <summary>
    /// A DTO-style class to capture a single solver attempt
    /// </summary>
    public class SolverPuzzle
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public ulong Hash { get; set; }
        [XmlAttribute]
        public float Rating { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        [XmlAttribute]
        public string Author { get; set; }
        [XmlAttribute]
        public string Email { get; set; }
        [XmlAttribute]
        public string SourceURL { get; set; }

        public string BestKnownSolution { get; set; }

        public string[] NormalisedMap { get; set; }

        public List<SolverAttempt> Attempts { get; set; }
    }

    public class SolverAttempt
    {
        public string Solution { get; set; }

        public DateTime Created { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public SolverResultInfo SolverResultInfo { get; set; }
    }


    public class SolverProgress
    {
        public SolverProgress()
        {
            Items = new List<SolverPuzzle>();
        }
        public List<SolverPuzzle> Items { get; set; }
    }
}
