using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Actions
{
    class ActionCounter : Action
    {

        public ActionCounter(int fromValue, int toValue, int delta, ActionDelegate binder) : base(binder)
        {
            this.fromValue = fromValue;
            this.toValue = toValue;
            this.delta = delta;
            this.current = fromValue;
        }


        public ActionCounter(int fromValue, int toValue, int delta)
        {
            this.fromValue = fromValue;
            this.toValue = toValue;
            this.delta = delta;
            this.current = fromValue;
        }


        public ActionCounter(int fromValue, int toValue)
        {
            this.fromValue = fromValue;
            this.toValue = toValue;
            this.delta = 1;
            this.current = fromValue;
        }

        public override void Init()
        {
            current = fromValue;
        }

        public override bool PerformStep()
        {
            if (delta > 0)
            {
                if (current + delta > toValue)
                {
                    current = toValue;
                    return true;
                }
            }
            else
            {
                if (current + delta < toValue)
                {
                    current = toValue;
                    return true;
                }
            }
            

            current += delta;

            return false;
        }


        public int Current
        {
            get { return current; }
        }

        private int fromValue;
        private int toValue;
        private int delta;
        private int current;
    }
}
