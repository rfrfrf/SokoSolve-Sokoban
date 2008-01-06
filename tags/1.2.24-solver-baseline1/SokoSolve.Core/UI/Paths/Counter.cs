using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Paths
{
    public class Counter : ICounter
    {
        public Counter(float aStart, float aEnd, float aStep)
        {
            Start = aStart;
            End = aEnd;
            _Current = Start;
            Step = aStep;
        }

        public float Start;
        float _Current;
        public float Step;
        public float End;

        #region ICounter Members

        public void Reset()
        {
            _Current = Start;
        }

        public float Next()
        {
            if (isComplete) return _Current;
            _Current += Step;            
            return _Current;
        }

        public float Current
        {
            get { return _Current; }
        }

        public bool isComplete
        {
            get
            {
                if (Step >= 0)
                {
                    return (_Current >= End);
                }
                else
                {
                    return (_Current <= End);
                }
            }
        }

        #endregion
    }
}
