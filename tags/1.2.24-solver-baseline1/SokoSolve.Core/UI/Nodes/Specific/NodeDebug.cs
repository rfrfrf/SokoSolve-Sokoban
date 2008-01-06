using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Specific
{
    class NodeDebug : NodeBase
    {
        public NodeDebug(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            this.CurrentAbsolute = myGameUI.GameCoords.WindowRegion.BottomLeft.Subtract(0, 12);

        }

        public override void Render()
        {
            string text = string.Format("??.? fps, mouse:{0}, step:{1}", 
                    GameUI.Cursor == null ? "" : GameUI.Cursor.CurrentAbsolute.ToString()
                , GameUI.StepCurrent);
            GameUI.Graphics.DrawString(text, debugFont, debugBrush, CurrentAbsolute.X, CurrentAbsolute.Y);
        }

        Font debugFont = new Font("Arial", 8f);
        private Brush debugBrush = new SolidBrush(Color.DarkGray);
    }
}
