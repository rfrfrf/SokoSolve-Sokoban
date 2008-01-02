using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;
using Bitmap=SokoSolve.Common.Structures.Bitmap;

namespace SokoSolve.UI.Section.Solver
{
    /// <summary>
    /// Perform simple visualisation of multiple bitmaps
    /// </summary>
    public partial class BitmapViewer : UserControl
    {
        #region Delegates

        public delegate void DrawCellDelegate(Graphics graphics, VectorInt cellIdx, RectangleInt cellDrawingRect);

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public BitmapViewer()
        {
            InitializeComponent();

            cellSize = new SizeInt(16, 16);

            title = null;

            bitmaps = new List<DisplayBitmap>();
            globalOffst = new VectorInt(1, 1);


            penHorzLine = new Pen(this.BackColor, 2f);
            penVertLine = new Pen(this.BackColor, 2f);

             titleBrush = new SolidBrush(Color.Black);
            titleFont = new Font("Arial", 10f);
       
        }

        public class DisplayBitmap
        {
            public Brush Brush = new SolidBrush(Color.Beige);
            public string Name = "Unnamed";
            public SolverBitmap Bitmap;
            public bool Show = true;

            public override string  ToString()
            {
                return Name;
            }
        }

        public void Add(SolverBitmap bitmap, Brush Brush)
        {
            DisplayBitmap bit = new DisplayBitmap();
            bit.Name = bitmap.Name;
            bit.Bitmap = bitmap;
            bit.Show = true;
            bit.Brush = Brush;
            Add(bit);
        }

        public void Add(DisplayBitmap bitmap)
        {
            if (bitmap == null) return;
            bitmaps.Add(bitmap);

            SyncCheckBox();
        }

        public void Clear()
        {
           bitmaps.Clear();

            SyncCheckBox();
        }

        private void SyncCheckBox()
        {
            //return;
            checkedListBox.BeginUpdate();
            checkedListBox.Items.Clear();
            foreach (DisplayBitmap bitmap in bitmaps)
            {
                string name = bitmap.Name;
                SolidBrush clr = bitmap.Brush as SolidBrush;
                if (clr != null) name += " " + clr.Color.Name;
                checkedListBox.Items.Add(bitmap, bitmap.Show);
            }
            checkedListBox.EndUpdate();
        }
     

        /// <summary>
        /// Helper, gets the bitmap size (based on first)
        /// </summary>
        [Browsable(false)]
        private SizeInt BitmapSize
        {
            get
            {
                if (bitmaps == null || bitmaps.Count == 0) return null;
                return bitmaps[0].Bitmap.Size;
            }
        }

        public bool HasBitmaps
        {
            get { return bitmaps.Count > 0; }
        }

        /// <summary>
        /// Coord helper. Get Abs pixel pos based on logical cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private RectangleInt GetPixelCoordsFromCell(VectorInt cell)
        {
            VectorInt topLeft =
                new VectorInt(cell.X*cellSize.Width + globalOffst.X, cell.Y*cellSize.Height + globalOffst.Y);
            return new RectangleInt(topLeft, topLeft.Add(cellSize));
        }

        /// <summary>
        /// Coord helper. Get logical cell from abs pixel
        /// </summary>
        /// <param name="mouse"></param>
        /// <returns></returns>
        private VectorInt GetLogicalCellCoordsFromPixel(VectorInt mouse)
        {
            return mouse.Subtract(globalOffst).Divide(cellSize);
        }

        /// <summary>
        /// Draw the user control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BitmapViewer_Paint(object sender, PaintEventArgs e)
        {
            if (bitmaps == null || bitmaps.Count == 0) return;

            if (cellSize == null ||cellSize.Width == 0 || cellSize.Height == 0) return;

            try
            {
                // Draw cells
                for (int cx = 0; cx < BitmapSize.Width; cx++)
                    for (int cy = 0; cy < BitmapSize.Height; cy++)
                    {
                        VectorInt cellIdx = new VectorInt(cx, cy);
                        RectangleInt pos = GetPixelCoordsFromCell(cellIdx);
                        DrawCell(e.Graphics, cellIdx, pos);
                    }

                // Draw lines
                for (int cx = 0; cx < BitmapSize.Width + 1; cx++)
                {
                    int x = cx*cellSize.Width + globalOffst.X;
                    int maxY = BitmapSize.Height*cellSize.Height + globalOffst.Y;
                    e.Graphics.DrawLine(penHorzLine, x, globalOffst.Y, x, maxY);
                }

                for (int cy = 0; cy < BitmapSize.Height + 1; cy++)
                {
                    int y = cy*cellSize.Height + globalOffst.Y;
                    int maxX = BitmapSize.Width*cellSize.Width + globalOffst.X;
                    e.Graphics.DrawLine(penVertLine, globalOffst.X, y, maxX, y);
                }

                if (title != null) e.Graphics.DrawString(title, titleFont, titleBrush, 0, 0);
            }
            catch(Exception ex)
            {
                labelMouse.Text = ex.Message + ex.StackTrace;
            }
        }

        /// <summary>
        /// Draw an individual cell. If <see cref="OnDrawCell"/> is set, then it is used
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="cellIdx"></param>
        /// <param name="cellDrawingRect"></param>
        private void DrawCell(Graphics graphics, VectorInt cellIdx, RectangleInt cellDrawingRect)
        {
            if (OnDrawCell != null)
            {
                OnDrawCell(graphics, cellIdx, cellDrawingRect);
                return;
            }
            else
            {
                // Default implementation   
                int cc = 0;
                List<int> todraw = new List<int>();
                foreach (DisplayBitmap bitmap in bitmaps)
                {
                    if (bitmap.Bitmap[cellIdx] && bitmap.Show)
                    {
                        todraw.Add(cc);
                    }
                    cc++;
                }

                if (todraw.Count == 0) return;

                if (todraw.Count == 1)
                {
                    graphics.FillRectangle(bitmaps[todraw[0]].Brush, cellDrawingRect.ToDrawingRect());
                }
                else
                {
                    int slice = cellSize.Width/todraw.Count;
                    for (int dd = 0; dd < todraw.Count; dd++)
                    {
                        graphics.FillRectangle(bitmaps[todraw[dd]].Brush, cellDrawingRect.TopLeft.X + dd * slice,
                                               cellDrawingRect.TopLeft.Y, slice, cellDrawingRect.Height);
                    }
                }
            }
        }

        /// <summary>
        /// Capture mouse movement to show which cell we are on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BitmapViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (bitmaps == null || bitmaps.Count == 0) return;

            VectorInt cell = GetLogicalCellCoordsFromPixel(new VectorInt(e.X, e.Y));

            if (cell.X < 0 || cell.X >= BitmapSize.Width) return;
            if (cell.Y < 0 || cell.Y >= BitmapSize.Height) return;

            string hitlist = "";
            int cc = 0;
            foreach (DisplayBitmap bitmap in bitmaps)
            {
                cc++;
                if (bitmap.Bitmap[cell])
                {
                    SolverBitmap sb = bitmap.Bitmap;
                    if (sb != null)
                    {
                        if (hitlist.Length != 0) hitlist += ", ";
                        hitlist += sb.Name;
                    }
                    else
                    {
                        if (hitlist.Length != 0) hitlist += ", ";
                        hitlist += cc.ToString();
                    }
                }
            }
            mouseText = string.Format("({0}, {1}) -> {2}", cell.X, cell.Y, hitlist);
            labelMouse.Text = mouseText;
        }

        private List<DisplayBitmap> bitmaps;
        private SizeInt cellSize;
        private VectorInt globalOffst;
        private string mouseText;
        public DrawCellDelegate OnDrawCell;
        private Pen penHorzLine;
        private Pen penVertLine;
        private string title;
        private Brush titleBrush;
        private Font titleFont;

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           
            (checkedListBox.Items[e.Index] as DisplayBitmap).Show = e.NewValue == CheckState.Checked;
            Refresh();
        }

        
    }
}