using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Common.Structures
{
	/// <summary>
	/// Simple bitmap for holding maps of states, which is comparible
	/// </summary>
	/// <remarks>
	/// This is the central class for the solver and analysis of puzzles.
	/// </remarks>
	public class Bitmap : IBitmap
	{
		/// <summary>
		/// Strong Construction
		/// </summary>
		/// <param name="aSizeX"></param>
		/// <param name="aSizeY"></param>
		public Bitmap(int aSizeX, int aSizeY)
		{
			if (aSizeX > 32) throw new NotSupportedException("Only 32bit sizes are excepted");
			sizeX = aSizeX;
			map = new uint[aSizeY];

			Clear();
		}

		/// <summary>
		/// Overloaded Constructor
		/// </summary>
		/// <param name="aSize"></param>
		public Bitmap(VectorInt aSize) : this(aSize.X, aSize.Y)
		{
		}

		/// <summary>
		/// Copy Constructor. Deep copy.
		/// </summary>
		/// <param name="copy"></param>
		public Bitmap(IBitmap copy) : this(copy.Size.X, copy.Size.Y)
		{
			for (int cy=0; cy<copy.Size.Y; cy++)
				for (int cx=0; cx<copy.Size.X; cx++)
				{
					this[cx, cy] = copy[cx, cy];
				}
		}

		/// <summary>
		/// Create a BitMap from a string map of '0' and '1'.
		/// Generally this constructor is for test purposes.
		/// </summary>
		/// <param name="StringMap"></param>
		public Bitmap(string[] StringMap)
		{
			// calc max length
			int sz = 0;
			foreach (String s in StringMap)
			{
				if (s.Length > sz) sz = s.Length;
			}
			if (sz > 32) throw new NotSupportedException("Only 32bit sizes are excepted");
			sizeX = sz;
			map = new uint[StringMap.Length];

			Clear();

			// Set the strings
			for (int yy = 0; yy < Size.Y; yy++)
				for (int xx = 0; xx < Size.X; xx++)
				{
					if (StringMap[yy] != null && StringMap[yy].Length > xx)
					{
						this[xx, yy] = StringMap[yy][xx] == '1';
					}
				}
		}

		/// <summary>
		/// Universal Accessor
		/// </summary>
		public bool this[VectorInt aPoint]
		{
			get
			{
				return (map[aPoint.Y] & (1 << aPoint.X)) > 0;
			}
			set
			{
				if (value)
				{
					map[aPoint.Y] = map[aPoint.Y] | (uint)(1 << aPoint.X);
				}
				else
				{
					map[aPoint.Y] = map[aPoint.Y] & ~(uint)(1 << aPoint.X);
				}
			}
		}

		/// <summary>
		/// Overloaded.
		/// </summary>
		public bool this[int pX, int pY]
		{
			get { return this[new VectorInt(pX, pY)]; }
			set { this[new VectorInt(pX, pY)] = value; }
		}


		/// <summary>
		/// Size if the Bitmap
		/// </summary>
		public SizeInt Size
		{
			get
			{
                return new SizeInt(sizeX, map.Length);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Clear, or delete the bitmap
		/// </summary>
		public void Clear()
		{
			for (int ccy = 0; ccy < map.Length; ccy++)
			{
				map[ccy] = 0;
			}
		}

		/// <summary>
		/// The number of On's (set bits)
		/// </summary>
		/// <returns></returns>
		public int Count
		{
			get
			{
				int result = 0;
				for (int ccy = 0; ccy < map.Length; ccy++)
				{
					if (map[ccy] == 0) continue;
					for (int ccx = 0; ccx < sizeX; ccx++)
					{
						if (this[ccx, ccy]) result++;
					}
				}
				return result;
			}
		}

		/// <summary>
		/// Are any bits set? This is a fast function.
		/// </summary>
		public bool isZero
		{
			get
			{
				for (int ccy = 0; ccy < map.Length; ccy++)
					if (map[ccy] > 0) return false;
				return true;
			}
		}

		/// <summary>
		/// Return a string representation of map
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder rep = new StringBuilder();
			for (int ccy = 0; ccy < map.Length; ccy++)
			{
				for (int ccx = 0; ccx < sizeX; ccx++)
				{
					if (this[ccx, ccy]) rep.Append('1');
					else rep.Append('0');
				}
				rep.Append(Environment.NewLine);
			}
			return rep.ToString();
		}

		/// <summary>
		/// Comparison
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj is Bitmap)
			{
				Bitmap rhs = (Bitmap)obj;
				if (rhs.Size != Size)
					throw new InvalidOperationException(string.Format("Bitmaps may only be compared it the are identical sizes lhs:{0}; rhs:{1}", Size, rhs.Size));

				for (int ccy = 0; ccy < map.Length; ccy++)
				{
					if (map[ccy] != rhs.map[ccy]) return false;
				}
				return true;
			}
			return base.Equals(obj);
		}

		/// <summary>
		/// Operator overloading for comparison
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator ==(Bitmap lhs, Bitmap rhs)
		{
			if ((object)lhs == null && (object)rhs == null) return true;
			if ((object)lhs == null || (object)rhs == null) return false;
			return lhs.Equals(rhs);
		}


		/// <summary>
		/// Operator overloading for comparison
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator !=(Bitmap lhs, Bitmap rhs)
		{
			return !(lhs == rhs);
		}

		/// <summary>
		/// Create Hash code
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			uint result = 0;
			for (int ccy = 0; ccy < map.Length; ccy++)
				result += (uint)ccy * map[ccy];

			return (int)result;
		}


		//##############################################################
		//##############################################################
		//##############################################################
		// Set operators

		/// <summary>
		/// Is the set a superset. Will return true is aSet==aSuperSet
		/// </summary>
		/// <param name="aSet"></param>
		/// <param name="aSuperSet"></param>
		/// <returns></returns>
		public bool isSuperSet(Bitmap aSuperSet)
		{
			// Unoptimised: Should be much faster to iterator through aSet until a single failure is found
			return (this.BitwiseAND(aSuperSet) == this);
		}

		/// <summary>
		/// Is the set a subset, or superset of the other. Will return true is aSet==aSubSet
		/// </summary>
		/// <param name="aSet"></param>
		/// <param name="aSubSet"></param>
		/// <returns></returns>
		public bool isSubSet(Bitmap aSubSet)
		{
			// Unoptimised: Should be much faster to iterator through aSet until a single failure is found
			return (this.BitwiseAND(aSubSet) == aSubSet);
		}

		//##############################################################
		//##############################################################
		//##############################################################
		// Bitwise Functions

		/// <summary>
		/// Perform a bitwize OR operation on the two maps
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		static public Bitmap BitwiseOR(Bitmap lhs, Bitmap rhs)
		{
			if (lhs.Size != rhs.Size)
				throw new InvalidOperationException(string.Format("Bitmaps BitwiseOR may only be performed on identical sizes lhs:{0}; rhs:{1}", lhs.Size, rhs.Size));

			Bitmap result = new Bitmap(lhs.Size);

			for (int ccy = 0; ccy < lhs.Size.Y; ccy++)
			{
				result.map[ccy] = lhs.map[ccy] | rhs.map[ccy];
			}

			return result;
		}

		/// <summary>
		/// Perform a bitwize OR operation on the two maps
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		static public Bitmap BitwiseAND(Bitmap lhs, Bitmap rhs)
		{
			if (lhs.Size != rhs.Size)
				throw new InvalidOperationException(string.Format("Bitmaps BitwiseAND may only be performed on identical sizes lhs:{0}; rhs:{1}", lhs.Size, rhs.Size));

			Bitmap result = new Bitmap(lhs.Size);

			for (int ccy = 0; ccy < lhs.Size.Y; ccy++)
			{
				result.map[ccy] = lhs.map[ccy] & rhs.map[ccy];
			}

			return result;
		}

		/// <summary>
		/// Perform a bitwize OR operation on the two maps
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		static public Bitmap BitwiseNOT(Bitmap lhs)
		{

			Bitmap result = new Bitmap(lhs.Size);

			for (int ccx = 0; ccx < lhs.Size.X; ccx++)
				for (int ccy = 0; ccy < lhs.Size.Y; ccy++)
				{
					result[ccx, ccy] = !lhs[ccx, ccy];
				}

			return result;
		}

		/// <summary>
		/// Overload
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public Bitmap BitwiseAND(Bitmap rhs)
		{
			return BitwiseAND(this, rhs);
		}

		/// <summary>
		/// Overload
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public Bitmap BitwiseOR(Bitmap rhs)
		{
			return BitwiseOR(this, rhs);
		}

		public Bitmap BitwiseNOT()
		{
			return BitwiseNOT(this);
		}

		//##############################################################
		//##############################################################
		//##############################################################

		uint[] map;
		int sizeX;
	}
}
