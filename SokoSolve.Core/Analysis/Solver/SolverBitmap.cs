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

        public SolverBitmap(string name, VectorInt aSize)
            : base(aSize)
        {
            this.name = name;
        }

        public SolverBitmap(string name, int aSizeX, int aSizeY)
            : base(aSizeX, aSizeY)
        {
            this.name = name;
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
