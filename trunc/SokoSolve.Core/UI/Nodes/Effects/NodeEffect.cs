using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Paths;

namespace SokoSolve.Core.UI.Nodes.Effects
{
    /// <summary>
    /// Basic node with a predetermined Animation Path
    /// </summary>
    public class NodeEffect : NodeBase
    {
        public NodeEffect(GameUI myGameUI, int myDepth)
            : base(myGameUI, myDepth)
        {
        }

        /// <summary>
        /// Integrate the animation path to affect the current position.
        /// </summary>
        public override void doStep()
        {
            base.doStep();

            if (path != null)
            {
                VectorInt nextPos = path.getNext();

                if (nextPos == null) Remove();
                    else CurrentAbsolute = nextPos;
            }
        }

        /// <summary>
        /// Animation path to use
        /// </summary>
        public virtual IPath Path
        {
            get { return path; }
            set { path = value; }
        }

        IPath path;
    }
}
