using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Math
{
	public class VectorDouble
	{
		private double x;
		private double y;

		public VectorDouble(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public VectorDouble(VectorDouble copy)
		{
			this.x = copy.x;
			this.y = copy.y;
		}

		public double X
		{
			get { return x; }
			set { x = value; }
		}

		public double Y
		{
			get { return y; }
			set { y = value; }
		}

		public VectorDouble Add(VectorDouble value)
		{
			return new VectorDouble(x + value.X, y + value.Y);
		}

		public VectorDouble Subtract(VectorDouble value)
		{
			return new VectorDouble(x - value.X, y - value.Y);
		}

		public VectorDouble Multiply(VectorDouble value)
		{
			return new VectorDouble(x * value.X, y * value.Y);
		}

		public VectorDouble Divide(VectorDouble value)
		{
			return new VectorDouble(x / value.X, y / value.Y);
		}

		public static VectorDouble operator +(VectorDouble lhs, VectorDouble rhs)
		{
			return lhs.Add(rhs);
		}

		public static VectorDouble operator -(VectorDouble lhs, VectorDouble rhs)
		{
			return lhs.Subtract(rhs);
		}

		public static VectorDouble operator *(VectorDouble lhs, VectorDouble rhs)
		{
			return lhs.Multiply(rhs);
		}

		public static VectorDouble operator /(VectorDouble lhs, VectorDouble rhs)
		{
			return lhs.Divide(rhs);
		}

		public static VectorDouble operator ++(VectorDouble lhs)
		{
			return lhs.Add(new VectorDouble(1, 1));
		}

		public static VectorDouble operator --(VectorDouble lhs)
		{
			return lhs.Subtract(new VectorDouble(1, 1));
		}

		public static bool operator ==(VectorDouble lhs, VectorDouble rhs)
		{
			return GeneralHelper.StaticEqualityCheckHelper(lhs, rhs);
		}

		public static bool operator !=(VectorDouble lhs, VectorDouble rhs)
		{
			return !GeneralHelper.StaticEqualityCheckHelper(lhs, rhs);
		}

		public override bool Equals(object obj)
		{
			if (obj is VectorDouble)
			{
				VectorDouble rhs = (VectorDouble)obj;
				return x == rhs.X && y == rhs.Y;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("X:{0} Y:{1}", x, y);
		}

		public VectorInt ToVectorInt()
		{
			return new VectorInt((int)X, (int)Y);
		}
	}
}
