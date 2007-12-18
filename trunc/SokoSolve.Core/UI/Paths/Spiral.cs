using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Game;



namespace SokoSolve.Core.UI.Paths
{
    public class Spiral : IPath
    {
        public Spiral(VectorInt Center)
        {
            center = Center;
            diameter = new Counter(0, 50, 0.3f);
            angle = new Counter(0, 360, 5);
        }


        public Spiral(VectorInt center, ICounter diameter, ICounter angle)
        {
            this.center = center;
            this.diameter = diameter;
            this.angle = angle;
        }

        #region IPath Members

        public VectorInt getNext()
        {
            // Exit condition
            if (diameter.isComplete) return null;
            if (angle.isComplete) angle.Reset();

            diameter.Next();
            angle.Next();

            return new VectorInt((int)(center.X + diameter.Current * Math.Sin(angle.Current *Math.PI/ 180)),
                                (int)(center.Y + diameter.Current * Math.Cos(angle.Current * Math.PI / 180)));
        }

        #endregion

        VectorInt center;
        ICounter diameter;
        ICounter angle;

    }
}
