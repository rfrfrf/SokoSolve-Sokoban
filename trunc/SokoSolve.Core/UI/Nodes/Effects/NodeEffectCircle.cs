using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using SokoSolve.Common.Math;
using SokoSolve.Core.Game;
using SokoSolve.Core.UI.Paths;


namespace SokoSolve.Core.UI.Nodes.Effects
{
    public class NodeEffectCircle : NodeEffect
    {
        public NodeEffectCircle(GameUI myGameUI, int myDepth, VectorInt myPos) : base(myGameUI, myDepth)
        {
            CurrentAbsolute = myPos;
			Path = new Linear(new VectorInt(10, 10), new VectorInt(32, 32), new VectorDouble(1, 1), false);
            pen = new Pen(Color.Goldenrod, 1.5f);
        }

        public override void doStep()
        {
			VectorInt next = Path.getNext();

            if (next.IsNull)
            {
                GameUI.Remove(this);
            }
            else
            {
                width = next.X;
                height = next.Y;    
            }
        }

        public override void Render()
        {
            GameUI.Graphics.DrawEllipse(pen, CurrentCentre.X - width/2, CurrentCentre.Y-height /2, width, height);
        }

        Pen pen;
        float width;
        float height;
 
    }
}
