using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace SokoSolve.Common.Math
{
    public class RectangleInt
    {
		public VectorInt TopLeft;
		public VectorInt BottomRight;

		public RectangleInt(VectorInt aTopLeft, SizeInt aSize)
        {
            TopLeft = aTopLeft;
            if (TopLeft != null) BottomRight = aTopLeft.Add(aSize);
        }

		public RectangleInt(VectorInt aTopLeft, VectorInt aBottomRight)
        {
            TopLeft = aTopLeft;
            BottomRight = aBottomRight;
        }

		public RectangleInt(Rectangle DrawingRect)
        {
			TopLeft = new VectorInt(DrawingRect.X, DrawingRect.Y);
			BottomRight = new VectorInt(DrawingRect.X + DrawingRect.Width, DrawingRect.Y + DrawingRect.Height);
        }

        public Rectangle ToDrawingRect()
        {
            return new Rectangle(TopLeft.X, TopLeft.Y, Width, Height);
        }

        public int Width
        {
            get { return BottomRight.X - TopLeft.X; }
            set { throw new NotImplementedException(); }
        }

        public int Height
        {
            get { return BottomRight.Y - TopLeft.Y; }
            set { throw new NotImplementedException(); }
        }

        public SizeInt Size
        {
            get { return new SizeInt(Width, Height); }
            set { throw new NotImplementedException(); }
        }

        public VectorInt TopRight
        {
            get { return TopLeft.Add(Width, 0); }
        }

        public VectorInt TopMiddle
        {
            get { return TopLeft.Add(Width/2, 0); }
        }

        public VectorInt BottomLeft
        {
            get { return TopLeft.Add(0, Height); }
        }

        public VectorInt BottomMiddle
        {
            get { return BottomRight.Subtract(Width/2, 0); }
        }

        public VectorInt Center
        {
            get { return TopLeft.Add(Width/2, Height / 2); }
        }

        public VectorInt MiddleRight
        {
            get { return TopRight.Add(0, Height/2);  }
        }

        public VectorInt MiddleLeft
        {
            get { return TopLeft.Add(0, Height / 2); }
        }

        public bool Contains(VectorInt P)
        {
            return (P.X >= TopLeft.X && P.Y >= TopLeft.Y &&
                    P.X <= BottomRight.X && P.Y <= BottomRight.Y);
        }

		public override string ToString()
		{
			return string.Format("{0} Width:{1} Height:{2}", TopLeft, Width, Height);
		}
    }
}
