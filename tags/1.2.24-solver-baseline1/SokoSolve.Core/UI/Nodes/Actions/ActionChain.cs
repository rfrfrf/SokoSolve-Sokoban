using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Actions
{
    class ActionChain
    {
        public ActionChain()
        {
            actions = new List<Action>();
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
                foreach (Action action in actions)
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


        public List<Action> Actions
        {
            get { return actions; }
        }

        /// <summary>
        /// Current action
        /// </summary>
        public Action Current
        {
            get { return current; }
            set { current = value; }
        }

        public void Add(Action newAction)
        {
            actions.Add(newAction);
        }

        private List<Action> actions;
        private Action current;
    }
}
