using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Actions
{
    class ActionMethod : Action
    {
        public ActionMethod(ActionDelegate onStep)
        {
            this.onStep = onStep;
        }

        public override bool PerformStep()
        {
            if (onStep != null)
            {
                return onStep(this);
            }

            return true;
        }

        private ActionDelegate onStep;
    }
}
