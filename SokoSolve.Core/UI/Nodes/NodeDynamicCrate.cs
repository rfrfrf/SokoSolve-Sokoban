using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI;
using SokoSolve.Core.UI.Nodes.Effects;


namespace SokoSolve.Core.UI.Nodes
{
    class NodeDynamicCrate : NodeDynamic
    {
        public NodeDynamicCrate(GameUI myGameUI, VectorInt myPuzzleLocation, int myDepth)
            : base(myGameUI, Cell.Crate, myPuzzleLocation, myDepth)
        {
            
        }

        

        NodeEffect effect;
        
    }
}
