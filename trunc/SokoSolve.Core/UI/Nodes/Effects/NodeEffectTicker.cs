using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Effects
{
    class NodeEffectTicker : NodeBase
    {
        public NodeEffectTicker(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
        }

        public override void Render()
        {
            
        }

        private string GetCurrentText()
        {
            return "Welcome to SokoSolve. You are playing 'Sasquatch' puzzle 'EasyRider'";
        }

        private int TickerCharLength = 30;
        private StringBuilder current;
    }
}
