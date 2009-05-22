using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using SokoSolve.Core.Analysis.Solver;

namespace SokoSolve.Core.Model.Services
{
    /// <summary>
    /// A DTO-style class to capture a single solver attempt
    /// </summary>
    public class SolverPuzzle
    {
        

        public SolverPuzzle()
        {
            Attempts = new SolverAttemptCollection();
        }


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
        public string SolutionAuthor { get; set; }
        [XmlAttribute]
        public string Email { get; set; }
        [XmlAttribute]
        public string SourceURL { get; set; }

        public string BestKnownSolution { get; set; }

        public string[] NormalisedMap { get; set; }

        public SolverAttemptCollection Attempts { get; set; }

        [XmlIgnore]
        public bool HasSolution
        {
            get
            {
                if (Attempts == null) return false;

                foreach (var attempt in Attempts.Items)
                {
                    if (attempt.Solution != null) return true;
                }

                return false;
            }
        }
    }

    public class SolverAttemptCollection
    {
        public SolverAttemptCollection()
        {
            TotalTime = 0;
            Items = new List<SolverAttempt>();
        }

        [XmlAttribute]
        public int TotalAttemptSucceed { get; set; }
        [XmlAttribute]
        public int TotalAttemptFailed { get; set; }
        
        public double TotalTime { get; set; }

        public List<SolverAttempt> Items { get; set; }

        [XmlIgnore]
        public int Count
        {
            get { return Items.Count;  }
        }


        public void Add(SolverAttempt item)
        {
            Items.Add(item);
        }
    }

    public class SolverAttempt
    {
        
        [XmlAttribute]
        public DateTime Created { get; set; }

        [XmlAttribute]
        public double ElapsedTime { get; set; }

        [XmlAttribute]
        public SolverController.States Result { get; set; }

        [XmlAttribute]
        public string SolverSummary { get; set; }

        public string Solution { get; set; }

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
