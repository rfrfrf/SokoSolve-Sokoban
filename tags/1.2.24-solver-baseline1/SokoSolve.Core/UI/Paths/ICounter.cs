using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Paths
{
    public interface ICounter
    {
        void Reset();
        float Next();
        float Current
        {
            get;
        }
        bool isComplete
        {
            get;
        }
    }
}
