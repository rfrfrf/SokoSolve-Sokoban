using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Actions
{
    public delegate bool ActionDelegate(NodeAction Source);

    /// <summary>
    /// Orcestrate a simple action. <see cref="ActionChain"/>
    /// </summary>
    public abstract class NodeAction
    {
        protected NodeAction(ActionDelegate onBind)
        {
            this.onBind = onBind;
        }

        protected NodeAction()
        {
        }

        public abstract bool PerformStep();
        
        /// <summary>
        /// Initialise. This allows re-init (which a pure constructor does not)
        /// </summary>
        public virtual void Init()
        {
            // Nothing to do
        }

        

        /// <summary>
        /// Fire this delegate (not a event) after a Step for binding activities
        /// </summary>
        public ActionDelegate OnBind
        {
            get { return onBind; }
            set { onBind = value; }
        }

        private ActionDelegate onBind;
    }
}
