using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Math
{
	public class SizeInt : VectorInt
	{
		public SizeInt(VectorInt copy) : base(copy)
		{
		}

		public SizeInt(int width, int height) : base(width, height)
		{
		}

        public SizeInt(System.Drawing.Size FromDrawing) : base(FromDrawing.Width, FromDrawing.Height)
        {
        }

		public int Width
		{
			get { return base.X;  }
			set { base.X = value; }
		}

		public int Height
		{
			get { return base.Y; }
			set { base.Y = value; }
		}
	}
}
