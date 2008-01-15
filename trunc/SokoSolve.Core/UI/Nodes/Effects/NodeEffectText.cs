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
            brushShaddow = new SolidBrush(Color.FromArgb(80,80,80));
            Path = null;
            IsVisible = true;

            UpdateSize();
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
            brushShaddow = new SolidBrush(Color.FromArgb(80, 80, 80));
            Path = new Linear(CurrentAbsolute, new VectorInt(20, 20), new VectorDouble(1, 1), false);
            text = RandomHelper.Select<string>(Text);
            IsVisible = true;

            UpdateSize();
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
        public NodeEffectText(GameUI myGameUI, int myDepth, string text, Font font, Brush brush, Brush brushShaddow, VectorInt start) : base(myGameUI, myDepth)
        {
            CurrentAbsolute = start;
            this.brush = brush;
            this.brushShaddow = brushShaddow;
            this.text = text;
            this.font = font;
            IsVisible = true;
        }

        private void UpdateSize()
        {
            if (text != null && font != null && GameUI.Graphics != null)
            {
                try
                {
                    SizeF sizeF = GameUI.Graphics.MeasureString(text, font);
                    this.Size = new SizeInt((int)sizeF.Width, (int)sizeF.Height);
                }
                catch(Exception ex)
                {
                    this.Size = new SizeInt(400, 20);
                    throw new Exception("This can only be called during a render phase, otherwise Graphics is not valid");
                }
            }
        }

        /// <summary>
        /// Render the text
        /// </summary>
        public override void Render()
        {
            if (!isVisible) return;

            UpdateSize();

            if (!CurrentAbsolute.IsNull)
            {
                if (brushBackGround != null)
                {
                    GameUI.Graphics.FillRectangle(brushBackGround, CurrentRect.ToDrawingRect());
                }

                if (brushShaddow != null)
                {
                    GameUI.Graphics.DrawString(text, font, brushShaddow, CurrentAbsolute.X + 1, CurrentAbsolute.Y + 1, stringFormat);
                }
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
            set { brush = value;}
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
        /// If not null, draw a background rectangle of this color
        /// </summary>
        public Brush BrushBackGround
        {
            get { return brushBackGround; }
            set { brushBackGround = value; }
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


        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        Brush brush;
        Brush brushShaddow;
        private Brush brushBackGround;
        string text;
        Font font;
        StringFormat stringFormat = new StringFormat();
        private IPath alphaPath;
        private bool isVisible;
    }
}
