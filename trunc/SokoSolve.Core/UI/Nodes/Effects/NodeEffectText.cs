using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Paths;



namespace SokoSolve.Core.UI.Nodes.Effects
{
    /// <summary>
    /// Display text
    /// </summary>
    public class NodeEffectText : NodeEffect
    {
        /// <summary>
        /// Simple Constructor. No path, Arial 12, White, Shaddow. No path.
        /// </summary>
        /// <param name="myGameUI">GameUI</param>
        /// <param name="myDepth">Depth Z-order</param>
        /// <param name="Text">Display Message</param>
        /// <param name="Start">StartPosition</param>
        public NodeEffectText(GameUI myGameUI, int myDepth, string Text, VectorInt Start) : base(myGameUI, myDepth)
        {
            CurrentAbsolute = Start;
            text = Text;
            font = new Font("Arial", 12, FontStyle.Bold);
            brush = new SolidBrush(Color.White);
            brushShaddow = new SolidBrush(Color.Black);
            Path = null;
        }

        /// <summary>
        /// Simple Constructor. Display a random message
        /// Linear Path (diagonal), Arial 12, Yellow, Shaddow. 
        /// </summary>
        /// <param name="myGameUI">GameUI</param>
        /// <param name="myDepth">Depth Z-order</param>
        /// <param name="Text">Display Message</param>
        /// <param name="Start">StartPosition</param>
        public NodeEffectText(GameUI myGameUI, int myDepth, string[] Text, VectorInt Start) : base(myGameUI, myDepth)
        {
            CurrentAbsolute = Start;
            
            font = new Font("Arial", 12, FontStyle.Bold);
            brush = new SolidBrush(Color.Yellow);
            brushShaddow = new SolidBrush(Color.Black);
            Path = new Linear(CurrentAbsolute, new VectorInt(20, 20), new VectorDouble(1, 1), false);
            text = RandomHelper.Select<string>(Text);
        }

        /// <summary>
        /// Full Constructor.
        /// </summary>
        /// <param name="myGameUI">GameUI</param>
        /// <param name="myDepth">Depth Z-order</param>
        /// <param name="brush">Text Font Brush</param>
        /// <param name="brushShaddow">Shaddow Brush (Null for none)</param>
        /// <param name="text">Message</param>
        /// <param name="font">Text Font</param>
        public NodeEffectText(GameUI myGameUI, int myDepth, string text, Font font, Brush brush, Brush brushShaddow) : base(myGameUI, myDepth)
        {
            this.brush = brush;
            this.brushShaddow = brushShaddow;
            this.text = text;
            this.font = font;
        }

        /// <summary>
        /// Render the text
        /// </summary>
        public override void Render()
        {
            if (CurrentAbsolute != null)
            {
                if (brushShaddow != null)
                    GameUI.Graphics.DrawString(text, font, brushShaddow, CurrentAbsolute.X + 1, CurrentAbsolute.Y + 1, stringFormat);
                GameUI.Graphics.DrawString(text, font, brush, CurrentAbsolute.X, CurrentAbsolute.Y, stringFormat);    
            }
        }

        /// <summary>
        /// Current Font
        /// </summary>
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        /// <summary>
        /// Current Brush
        /// </summary>
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }
        
        /// <summary>
        /// The shadow or back brush. Null means no shaddow.
        /// </summary>
        public Brush BrushShaddow
        {
            get { return brushShaddow; }
            set { brushShaddow = value; }
        }

        /// <summary>
        /// Current Text
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// Current String Formatting options.
        /// </summary>
        public StringFormat StringFormat
        {
            get { return stringFormat; }
            set { stringFormat = value; }
        }

        Brush brush;
        Brush brushShaddow;
        string text;
        Font font;
        StringFormat stringFormat = new StringFormat();
    }
}
