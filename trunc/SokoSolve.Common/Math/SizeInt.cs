using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SokoSolve.Common.Math
{
	public struct SizeInt
    {

        #region SizeInt
        
        public SizeInt(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public SizeInt(VectorInt copy): this(copy.X, copy.Y )
        {
            
        }

        public SizeInt(SizeInt copy) : this(copy.X, copy.Y)
        {

        }

        public SizeInt(System.Drawing.Size FromDrawing) : this(FromDrawing.Width, FromDrawing.Height)
        {
        }

		public int Width
		{
			get { return x;  }
			set { x = value; }
		}

		public int Height
		{
			get { return y; }
			set { y = value; }
        }

	    public VectorInt ToVectorInt
	    {
	        get { return new VectorInt(x, y);}
	    }

        #endregion SizeInt

        #region Vector

        public  static readonly SizeInt Zero = new SizeInt(0, 0);
        public  static readonly SizeInt Empty = new SizeInt(0, 0);
        private int x;
        private int y;

      

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }


        public static SizeInt MinValue
        {
            get { return new SizeInt(int.MinValue, int.MinValue); }
        }

        public VectorInt Add(VectorInt value)
        {
            return new VectorInt(x + value.X, y + value.Y);
        }

        public VectorInt Subtract(VectorInt value)
        {
            return new VectorInt(x - value.X, y - value.Y);
        }

        public VectorInt Multiply(VectorInt value)
        {
            return new VectorInt(x*value.X, y*value.Y);
        }

        public VectorInt Divide(VectorInt value)
        {
            return new VectorInt(x/value.X, y/value.Y);
        }

        public VectorInt Add(int aX, int aY)
        {
            return new VectorInt(x + aX, y + aY);
        }

        public VectorInt Subtract(int aX, int aY)
        {
            return new VectorInt(x - aX, y - aY);
        }

        public VectorInt Multiply(int aX, int aY)
        {
            return new VectorInt(x*aX, y*aY);
        }

        public VectorInt Divide(int aX, int aY)
        {
            return new VectorInt(x/aX, y/aY);
        }



        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return IsNull;
            }

            if (obj is VectorInt)
            {
                VectorInt rhs = (VectorInt)obj;
                return x == rhs.X && y == rhs.Y;
            }

            if (obj is SizeInt)
            {
                SizeInt rhs = (SizeInt)obj;
                return x == rhs.X && y == rhs.Y;
            }
            return false;
        }

        public bool IsNull
        {
            get { return x == int.MinValue && y == int.MinValue; }
        }

        public static SizeInt Min(SizeInt A, SizeInt B)
        {
            return new SizeInt(System.Math.Min(A.X, B.X), System.Math.Min(A.Y, B.Y));
        }

        public static SizeInt Max(SizeInt A, SizeInt B)
        {
            return new SizeInt(System.Math.Max(A.X, B.X), System.Math.Max(A.Y, B.Y));
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("X:{0} Y:{1}", x, y);
        }

        public VectorDouble ToVectorDouble()
        {
            return new VectorDouble((double) X, (double) Y);
        }

        /// <summary>
        /// Convert to a Drawing position
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Point ToPoint()
        {
            return new Point(x, y);
        }

        /// <summary>
        /// Convert to a Drawing position
        /// </summary>
        /// <returns></returns>
        public System.Drawing.PointF ToPointF()
        {
            return new PointF(x, y);
        }

        public VectorInt Offset(Direction dir)
        {
            switch (dir)
            {
                case (Direction.Up):
                    return this.Add(0, -1);
                case (Direction.Down):
                    return this.Add(0, 1);
                case (Direction.Left):
                    return this.Add(-1, 0);
                case (Direction.Right):
                    return this.Add(1, 0);
            }
            throw new InvalidOperationException();
        }


        /// <summary>
        /// Geint FOUR offsets (Up, Down, Left, Right) as an array
        /// </summary>
        /// <returns></returns>
        public VectorInt[] OffsetDirections()
        {
            return new VectorInt[]
                {
                    Offset(Direction.Up),
                    Offset(Direction.Down),
                    Offset(Direction.Left),
                    Offset(Direction.Right)
                };
        }

        #endregion Vector
    }
}
