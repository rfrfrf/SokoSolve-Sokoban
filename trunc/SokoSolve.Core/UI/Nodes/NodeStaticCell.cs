using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI;


namespace SokoSolve.Core.UI.Nodes
{   
    public class NodeStaticCell : NodeCell
    {
        public NodeStaticCell(GameUI myGameUI, Cell myCell, VectorInt myPuzzleLocation, int myDepth)
            : base(myGameUI, myCell, myPuzzleLocation, myDepth)
        {
            
        }
     
    }
}
