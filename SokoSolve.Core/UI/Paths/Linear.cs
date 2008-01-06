using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Game;




namespace SokoSolve.Core.UI.Paths
{
    
    public class Linear : IPath
    {
        public Linear(VectorInt aCurrent, VectorInt aTarget, VectorDouble aStep, bool useBounce)
        {
            _Start = new VectorDouble(aCurrent.X, aCurrent.Y);
            _Current = new VectorDouble(aCurrent.X, aCurrent.Y);
            _Target = _Start.Add(aTarget.ToVectorDouble());
            _Step = aStep;
            _Bounce = useBounce;
        }
        
        public VectorInt getNext()
        {
            _Current = _Current.Add(_Step);
            if (_Bounce)
            {
                if (_Current.X >= _Target.X) _Step.X *= -1;
                if (_Current.X <= _Start.X) _Step.X *= -1;
                if (_Current.Y >= _Target.Y) _Step.Y *= -1;
                if (_Current.Y <= _Start.Y) _Step.Y *= -1;
                return _Current.ToVectorInt();
            }
            else
            {
                if (_Current == _Target) return null;
                return _Current.ToVectorInt();
            }
        }

        VectorDouble _Start;
        VectorDouble _Current;
        VectorDouble _Step;
        VectorDouble _Target;
        bool _Bounce;
    }
}
