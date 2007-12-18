using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.UI.Paths
{
    /// <summary>
    /// An animation path which allows a node to 'dock' to another node
    /// </summary>
    class Docked : IPath
    {
        /// <summary>
        /// Attached to another node offset
        /// </summary>
        /// <param name="offset">How much to offset from another node</param>
        /// <param name="attached">Which node to attach to</param>
        public Docked(VectorInt offset, NodeBase attached)
        {
            this.offset = offset;
            this.attached = attached;
        }

        /// <summary>
        /// Offset from Attached node
        /// </summary>
        public VectorInt Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Docking target
        /// </summary>
        public NodeBase Attached
        {
            get { return attached; }
            set { attached = value; }
        }

        /// <summary>
        /// Get the next location
        /// </summary>
        /// <returns></returns>
        public VectorInt getNext()
        {
            return attached.CurrentAbsolute.Add(offset);
        }

        private VectorInt offset;
        private NodeBase attached;
    }
}
