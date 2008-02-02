using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.UI.Nodes.Effects
{
    class NodeEffectImage : NodeEffect
    {
        /// <summary>
        /// Strong Contructor. 
        /// Enforce the use of resource for images
        /// </summary>
        /// <param name="myGameUI"></param>
        /// <param name="myDepth"></param>
        /// <param name="resourceImage"></param>
        public NodeEffectImage(GameUI myGameUI, int myDepth, ResourceID resourceImage) : base(myGameUI, myDepth)
        {
            this.image = myGameUI.ResourceFactory[resourceImage].DataAsImage;
            this.Size = new SizeInt(image.Width, image.Height);
        }

        /// <summary>
        /// Strong Contructor. 
        /// Direct use of image
        /// </summary>
        /// <param name="myGameUI"></param>
        /// <param name="myDepth"></param>
        /// <param name="directImage"></param>
        public NodeEffectImage(GameUI myGameUI, int myDepth, Image directImage)
            : base(myGameUI, myDepth)
        {
            this.image = directImage;
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
