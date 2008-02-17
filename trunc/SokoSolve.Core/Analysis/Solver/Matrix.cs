using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Provide a float-map (instead of a <see cref="Bitmap"/>)
    /// </summary>
    public class Matrix
    {
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="size"></param>
        public Matrix(SizeInt size)
        {
            Size = size;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="rhs"></param>
        public Matrix(Matrix rhs)
        {
            Size = rhs.Size;
            Set(rhs);
        }

        /// <summary>
        /// Init from a bitmap, setting on true positions to <paramref name="Value"/>
        /// </summary>
        /// <param name="Bitmap"></param>
        /// <param name="Value"></param>
        public Matrix(IBitmap Bitmap, float Value)
        {
            Size = Bitmap.Size;
            for (int ccx = 0; ccx < Width; ccx++)
                for (int ccy = 0; ccy < Height; ccy++)
                {
                    if (Bitmap[ccx, ccy]) this[ccx, ccy] = Value;
                }
        }

        public float this[int x, int y]
        {
            get { return values[x, y];  }
            set { values[x, y] = value;  }
        }

        public float this[VectorInt pos]
        {
            get { return values[pos.X, pos.Y]; }
            set { values[pos.X, pos.Y] = value; }
        }

        public SizeInt Size
        {
            get { return new SizeInt(values.GetLength(0), values.GetLength(1));}
            set
            {
                if (values ==null)
                {
                    values = new float[value.X,value.Y];
                    Set(0);
                }
                else
                {
                    // resize
                    throw new NotSupportedException("Resize");
                }
            }
        }

        public int Width
        {
            get { return values.GetLength(0);  }
        }

        public int Height
        {
            get { return values.GetLength(1); }
        }

        public void Set(float newValue)
        {
            for (int ccx=0; ccx<Width; ccx++)
                for (int ccy=0; ccy<Height; ccy++)
                {
                    values[ccx, ccy] = newValue;
                }
        }

        public void Set(Matrix rhs)
        {
            int ccxSize = Math.Min(Width, rhs.Width);
            int ccySize = Math.Min(Height, rhs.Height);

            for (int ccx = 0; ccx < ccxSize; ccx++)
                for (int ccy = 0; ccy < ccySize; ccy++)
                {
                    values[ccx, ccy] = rhs[ccx, ccy];
                }
        }

        public Matrix Add(Matrix rhs)
        {
            Matrix res = new Matrix(Size);
            int ccxSize = Math.Min(Width, rhs.Width);
            int ccySize = Math.Min(Height, rhs.Height);

            for (int ccx = 0; ccx < ccxSize; ccx++)
                for (int ccy = 0; ccy < ccySize; ccy++)
                {
                   res.values[ccx, ccy] = this[ccx, ccy] + rhs[ccx, ccy];
                }
            return res;
        }


        public Matrix Multiply(Matrix rhs)
        {
            Matrix res = new Matrix(Size);
            int ccxSize = Math.Min(Width, rhs.Width);
            int ccySize = Math.Min(Height, rhs.Height);

            for (int ccx = 0; ccx < ccxSize; ccx++)
                for (int ccy = 0; ccy < ccySize; ccy++)
                {
                    res.values[ccx, ccy] = this[ccx, ccy] * rhs[ccx, ccy];
                }
            return res;
        }

        public Matrix Divide(Matrix rhs)
        {
            Matrix res = new Matrix(Size);
            int ccxSize = Math.Min(Width, rhs.Width);
            int ccySize = Math.Min(Height, rhs.Height);

            for (int ccx = 0; ccx < ccxSize; ccx++)
                for (int ccy = 0; ccy < ccySize; ccy++)
                {
                    res.values[ccx, ccy] = this[ccx, ccy]/ rhs[ccx, ccy];
                }
            return res;
        }

        public delegate bool EvalBooleanDelegate(float value);

        /// <summary>
        /// Evluate a boolean result for each value in the Matrix creating a result boolean map
        /// </summary>
        /// <param name="boolAction"></param>
        /// <returns></returns>
        public Bitmap EvalBoolean(EvalBooleanDelegate boolAction)
        {
            Bitmap result = new Bitmap(Size);
            for (int ccx = 0; ccx < Width; ccx++)
                for (int ccy = 0; ccy < Height; ccy++)
                {
                    result[ccx, ccy] = boolAction(values[ccx, ccy]);
                }
            return result;
        }

        /// <summary>
        /// Return true for all non-zero values
        /// </summary>
        /// <returns></returns>
        public Bitmap BooleanNotZero()
        {
            return EvalBoolean(delegate(float item) { return item != 0; });
        }

        /// <summary>
        /// Total all values
        /// </summary>
        /// <returns></returns>
        public float Total()
        {
            float total = 0;
             for (int ccx = 0; ccx < Width; ccx++)
                for (int ccy = 0; ccy < Height; ccy++)
                {
                    total += values[ccx, ccy];
                }
            return total;
        }

        /// <summary>
        /// Average the Matrix values in generations
        /// </summary>
        /// <param name="VoidCells">Positions that are not include (null/void/zero) in the average process</param>
        /// <returns></returns>
        public Matrix Average(Bitmap VoidCells)
        {
            // Clone
            Matrix result = new Matrix(this);

            // Start will all set values
            Bitmap currentValues = result.BooleanNotZero();

            // Fill in the blanks
            bool valueSet = false;
            do
            {
                valueSet = false;

                Bitmap nextValues = new Bitmap(currentValues);

                for (int ccx = 0; ccx < Width; ccx++)
                for (int ccy = 0; ccy < Height; ccy++)
                {
                    // Skip void cells
                    if (VoidCells[ccx, ccy]) continue;

                    if (currentValues[ccx,ccy] == false)
                    {
                        // Not set
                        float total = 0;
                        int count = 0;

                        // Left
                        if (ccx - 1 > 0 && currentValues[ccx - 1, ccy])
                        {
                            total += result[ccx - 1, ccy];
                            count++;
                        }


                        // Right
                        if (ccx+1 < Width && currentValues[ccx+1,ccy])
                        {
                            total += result[ccx + 1, ccy];
                            count++;
                        }
                        

                        // Up
                        if (ccy - 1 > 0 && currentValues[ccx, ccy-1])
                        {
                            total += result[ccx, ccy - 1];
                            count++;
                        }
                        

                        // Down
                        if (ccy + 1 < Height && currentValues[ccx, ccy+1])
                        {
                            total += result[ccx, ccy + 1];
                            count++;
                        }

                        if (false)
                        {
                            // Top Left
                            if (ccx - 1 > 0 && ccy - 1 > 0 && currentValues[ccx - 1, ccy - 1])
                            {
                                total += result[ccx - 1, ccy - 1];
                                count++;
                            }


                            // Top Right
                            if (ccx + 1 < Width && ccy - 1 > 0 && currentValues[ccx + 1, ccy - 1])
                            {
                                total += result[ccx + 1, ccy - 1];
                                count++;
                            }


                            // Bottom Left
                            if (ccx - 1 > 0 && ccy + 1 < Height && currentValues[ccx - 1, ccy + 1])
                            {
                                total += result[ccx - 1, ccy + 1];
                                count++;
                            }


                            // Bottom Right
                            if (ccx + 1 < Width && ccy + 1 < Height && currentValues[ccx + 1, ccy + 1])
                            {
                                total += result[ccx + 1, ccy + 1];
                                count++;
                            }
                        }


                        if (count > 0)
                        {
                            valueSet = true;
                            result[ccx, ccy] = total/count/3f;
                            nextValues[ccx, ccy] = true;
                        }   
                    }
                }
                currentValues = nextValues;


            } while (valueSet);

            return result;
        }

        public override bool Equals(object obj)
        {
            Matrix rhs = obj as Matrix;
            if (rhs != null)
            {
                if (this.Size.X != rhs.Size.X && this.Size.Y != rhs.Size.Y) return false;

                int ccxSize = Math.Min(Width, rhs.Width);
                int ccySize = Math.Min(Height, rhs.Height);

                for (int ccx = 0; ccx < ccxSize; ccx++)
                    for (int ccy = 0; ccy < ccySize; ccy++)
                    {
                       if (this[ccx, ccy] !=  rhs[ccx, ccy]) return false;
                    }

                return true;
            }

            return base.Equals(obj);
        }


        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int ccy = 0; ccy < Height; ccy++)
            {
                for (int ccx = 0; ccx < Width; ccx++)  
                {
                    if (ccx > 0) sb.Append(", ");
                    sb.Append(values[ccx, ccy].ToString("0.00"));
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Build an HTML table
        /// </summary>
        /// <returns></returns>
        public string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            for (int ccy = 0; ccy < Height; ccy++)
            {
                sb.Append("<tr>");
                for (int ccx = 0; ccx < Width; ccx++)
                {
                    sb.Append("<td>");
                    sb.Append(values[ccx, ccy].ToString("0.00"));
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
                sb.Append(Environment.NewLine);
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        private float[,] values;

        
    }
}
