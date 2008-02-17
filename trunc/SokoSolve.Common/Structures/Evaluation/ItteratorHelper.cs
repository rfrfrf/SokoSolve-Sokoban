using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures.Evaluation
{
    public static class ItteratorHelper
    {
        public static int DefaultGetLocationNodeDepth<T>(INode<T> node)
        {
            TreeNode<T> upcast = node as TreeNode<T>;
            if (upcast == null) return 0;
            return upcast.Depth;
        }
    }
}
