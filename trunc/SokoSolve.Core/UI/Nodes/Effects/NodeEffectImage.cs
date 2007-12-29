using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.UI.Nodes.Effects
{
    class NodeEffectImage : NodeEffect
    {
        public NodeEffectImage(GameUI myGameUI, int myDepth, Image image) : base(myGameUI, myDepth)
        {
            this.image = image;
            this.Size = new SizeInt(image.Width, image.Height);
        }

        public override void Render()
        {
            if (image != null)
            {
                GameUI.Graphics.DrawImage(image, CurrentRect.ToDrawingRect());    
            }
            
        }

        private Image image;
    }
}
