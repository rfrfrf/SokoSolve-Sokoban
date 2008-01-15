using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Analysis.Solver
{
    public class SolverBitmap : Bitmap
    {

        #region Base Contructors


        public SolverBitmap(string name, IBitmap copy)
            : base(copy)
        {
            this.name = name;
        }

        public SolverBitmap(string name, SizeInt aSize)
            : base(aSize)
        {
            this.name = name;
        }

        public SolverBitmap(string name, int aSizeX, int aSizeY)
            : base(aSizeX, aSizeY)
        {
            this.name = name;
        }

        public SolverBitmap(SolverBitmap copy) : this(copy.name, new Bitmap(copy))
        {
            
        }
        #endregion

       

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string name;
    }
}
