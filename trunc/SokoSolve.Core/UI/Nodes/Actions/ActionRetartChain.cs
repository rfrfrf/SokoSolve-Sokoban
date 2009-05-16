using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Actions
{
    class ActionRetartChain : NodeAction
    {
        public ActionRetartChain(ActionChain chain)
        {
            this.chain = chain;
        }


        public ActionRetartChain(ActionChain chain, int maxRetarts)
        {
            this.chain = chain;
            restartCount = new ActionCounter(0, maxRetarts);
        }

        public override bool PerformStep()
        {
            if (restartCount != null)
            {
                bool compelted  = restartCount.PerformStep();
                if (!compelted)
                {
                    chain.Init();
                    return false; // Don't move to next in chain    
                }
                else
                {
                    return true;
                }
            }
            else
            {
                chain.Init();
                return false; // Don't move to next in chain    
            }
        }

        private ActionChain chain;
        private ActionCounter restartCount;
    }
}
