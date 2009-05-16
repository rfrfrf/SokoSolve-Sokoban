using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Actions
{
    class ActionChain
    {
        public ActionChain()
        {
            actions = new List<NodeAction>();
        }

        /// <summary>
        /// Perform an action step
        /// </summary>
        /// <returns>true = completed</returns>
        public bool PerformStep()
        {
            if (current == null) return true;

            // Perform a step
            bool complete = current.PerformStep();

            // Bind
            if (current.OnBind != null) current.OnBind(current);

            // Completed logic, move to next or exit
            if (complete)
            {
                if (actions.IndexOf(current) == actions.Count-1)
                {
                    return true;
                }
                else
                {
                    current = actions[actions.IndexOf(current) + 1];    
                }
            }
            return false;
        }

        public void Init()
        {
            if (actions != null && actions.Count > 0)
            {
                foreach (NodeAction action in actions)
                {
                    action.Init();
                }

                current = actions[0];
            }
            else
            {
                current = null;    
            }
            
        }


        public List<NodeAction> Actions
        {
            get { return actions; }
        }

        /// <summary>
        /// Current action
        /// </summary>
        public NodeAction Current
        {
            get { return current; }
            set { current = value; }
        }

        public void Add(NodeAction newNodeAction)
        {
            actions.Add(newNodeAction);
        }

        private List<NodeAction> actions;
        private NodeAction current;
    }
}
