using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Math
{
	public enum Direction
	{
		None, Up, Down, Left, Right
	}

	public enum DirectionDiagonals
	{
		None,
		Up,
		Down,
		Left,
		Right,
		UpLeft,
		UpRight,
		DownLeft,
		DownRight
	}

	public class VectorInt
	{
		private int x;
		private int y;

		public VectorInt(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public VectorInt(VectorInt copy)
		{
			this.x = copy.x;
			this.y = copy.y;
		}

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

		private static readonly VectorInt zero = new VectorInt(0, 0);

		public static VectorInt Zero
		{
			get { return zero;  }
		}

        static public VectorInt MinValue
        {
            get { return new VectorInt(int.MinValue, int.MinValue); }
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
			return new VectorInt(x * value.X, y * value.Y);
		}

		public VectorInt Divide(VectorInt value)
		{
			return new VectorInt(x / value.X, y / value.Y);
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
			return new VectorInt(x * aX, y * aY);
		}

		public VectorInt Divide(int aX, int aY)
		{
			return new VectorInt(x / aX, y / aY);
		}

		public static VectorInt operator+(VectorInt lhs, VectorInt rhs)
		{
			return lhs.Add(rhs);
		}

		public static VectorInt operator-(VectorInt lhs, VectorInt rhs)
		{
			return lhs.Subtract(rhs);
		}

		public static VectorInt operator*(VectorInt lhs, VectorInt rhs)
		{
			return lhs.Multiply(rhs);
		}

		public static VectorInt operator/(VectorInt lhs, VectorInt rhs)
		{
			return lhs.Divide(rhs);
		}

		public static VectorInt operator++(VectorInt lhs)
		{
			return lhs.Add(new VectorInt(1, 1));
		}

		public static VectorInt operator--(VectorInt lhs)
		{
			return lhs.Subtract(new VectorInt(1, 1));
		}

		public static bool operator==(VectorInt lhs, VectorInt rhs)
		{
			return GeneralHelper.StaticEqualityCheckHelper(lhs, rhs);
		}

		public static bool operator !=(VectorInt lhs, VectorInt rhs)
		{
			return !GeneralHelper.StaticEqualityCheckHelper(lhs, rhs);
		}

		public override bool Equals(object obj)
		{
			if (obj is VectorInt)
			{
				VectorInt rhs = (VectorInt)obj;
				return x == rhs.X && y == rhs.Y;
			}
			return false;
		}

		public static VectorInt Min(VectorInt A, VectorInt B)
		{
			return new VectorInt(System.Math.Min(A.X, B.X), System.Math.Min(A.Y, B.Y));
		}

		public static VectorInt Max(VectorInt A, VectorInt B)
		{
			return new VectorInt(System.Math.Max(A.X, B.X), System.Math.Max(A.Y, B.Y));
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
			return new VectorDouble((double)X, (double)Y);
		}

		public VectorInt Offset(Direction dir)
		{
			switch (dir)
			{
				case (Direction.Up): return this.Add(0, -1);
				case (Direction.Down): return this.Add(0, 1);
				case (Direction.Left): return this.Add(-1, 0);
				case (Direction.Right): return this.Add(1, 0);
			}
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Geint FOUR offsets (Up, Down, Left, Right) as an array
		/// </summary>
		/// <returns></returns>
		public VectorInt[] OffsetDirections()
		{
			return new VectorInt[] {
                Offset(Direction.Up),
                Offset(Direction.Down),
                Offset(Direction.Left),
                Offset(Direction.Right)
            };
		}

        public static Direction GetDirection(VectorInt A, VectorInt B)
        {
            if (A.X > B.X) return Direction.Left;
            if (A.X < B.X) return Direction.Right;
            if (A.Y > B.Y) return Direction.Up;
            if (A.Y < B.Y) return Direction.Down;
            return Direction.None;
        }
	}

}
