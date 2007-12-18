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

        public override void doStep()
        {
            base.doStep();

            isPushable = Array.IndexOf(PuzzleLocation.OffsetDirections(), GameUI.Current.Player) >= 0;

            if (isPushable && effect == null)
            {
                effect = new NodeEffectCircle(GameUI, 10, CurrentCentre);
                GameUI.Add(effect);
            }

            if (!isPushable && effect != null)
            {
                GameUI.Remove(effect);
                effect = null;
            }

        }

        NodeEffect effect;
        public bool isPushable;
    }
}
