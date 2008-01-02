using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Common.Math;

namespace SokoSolve.UI.Section.Solver
{
    

    class BitmapViewerVisualisation : GridVisualisation
    {
        public BitmapViewerVisualisation(Dictionary<string, BitmapViewer.Layer> layers, SizeInt size)
        {
            GridSize = size;
            CellSize = new SizeInt(16, 16);
            this.layers = layers;
        }

        /// <summary>
        /// From a logical position, return the element
        /// </summary>
        /// <param name="LogicalCell"></param>
        /// <returns></returns>
        public override GridVisualisationElement this[VectorInt LogicalCell]
        {
            get
            {
                if (LogicalCell.X >= 0 && LogicalCell.Y >= 0 && LogicalCell.X < GridSize.X && LogicalCell.Y < GridSize.Y)
                {
                    return cells[LogicalCell.X, LogicalCell.Y];
                }
                return null;
            }
            set
            {
                if (LogicalCell.X >= 0 && LogicalCell.Y >= 0 && LogicalCell.X < GridSize.X && LogicalCell.Y < GridSize.Y)
                {
                    cells[LogicalCell.X, LogicalCell.Y] = value as BitmapViewerVisualisationElement;
                    if (value != null && cells[LogicalCell.X, LogicalCell.Y] == null) throw new InvalidCastException("Wrong type");
                }
                throw new ArgumentOutOfRangeException("LogicalCell");
            }
        }


        /// <summary>
        /// Logical Size of the entire grid measured in cells.
        /// </summary>
        public override SizeInt GridSize
        {
            get { return base.GridSize; }
            set
            {
                base.GridSize = value;
                cells = new BitmapViewerVisualisationElement[value.Width, value.Height];
                for(int cx=0; cx<value.Width; cx++)
                    for (int cy=0; cy<value.Height; cy++)
                    {
                        cells[cx, cy] = new BitmapViewerVisualisationElement(this, new VectorInt(cx, cy));
                    }
            }
        }

        /// <summary>
        /// Draw the entire scene
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            drawLayers = new List<BitmapViewer.Layer>(layers.Values);
            drawLayers.Sort(delegate(BitmapViewer.Layer lhs, BitmapViewer.Layer rhs) { return lhs.Order.CompareTo(rhs.Order); });

            graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), RenderCanvas.ToDrawingRect());

            foreach (BitmapViewerVisualisationElement cell in cells)
            {
                cell.Draw(graphics, GetDrawRegion(cell));
            }
        }

        public List<BitmapViewer.Layer> DrawLayers
        {
            get { return drawLayers; }
        }



        private Dictionary<string, BitmapViewer.Layer> layers;
        private List<BitmapViewer.Layer> drawLayers;
        private BitmapViewerVisualisationElement[,] cells;

    }

    class BitmapViewerVisualisationElement : GridVisualisationElement
    {
        public BitmapViewerVisualisationElement(BitmapViewerVisualisation owner, VectorInt logicalPosition)
        {
            this.owner = owner;
            this.logicalPosition = logicalPosition;
        }

        public List<BitmapViewer.Layer> GetTrueValues()
        {
            List<BitmapViewer.Layer> res = new List<BitmapViewer.Layer>();
            foreach (BitmapViewer.Layer layer in owner.DrawLayers)
            {
                if (!layer.IsVisible) continue;

                if (layer.Bitmap != null)
                {
                    if (layer.Bitmap[logicalPosition]) res.Add(layer);
                }
            }
            return res;
        }

        private VectorInt logicalPosition;
        private BitmapViewerVisualisation owner;

        public override VectorInt LogicalPosition
        {
            get { return logicalPosition; }
            set { logicalPosition = value; }
        }

        public override string GetID()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }

        public override string GetDisplayData()
        {
            return StringHelper.Join(GetTrueValues(), delegate(BitmapViewer.Layer item) { return item.Name; }, ", ");
        }

        public override void Draw(Graphics graphics, RectangleInt region)
        {
            int slice = owner.CellSize.Width;

            List<BitmapViewer.Layer> slices = GetTrueValues();
            if (slices != null && slices.Count > 0)
            {
                slice = owner.CellSize.Width / slices.Count;
            }

            foreach (BitmapViewer.Layer current in owner.DrawLayers)
            {
                if (!current.IsVisible) continue;

                if (current.Bitmap != null)
                {
                    if (slices.Contains(current))
                    {
                        if (current.Map != null)
                        {
                            Image tile = DrawingHelper.Images.GetImage(current.Map[logicalPosition]);
                            graphics.DrawImage(tile, region.ToDrawingRect());
                        }
                        else if (current.CellImage != null)
                        {
                            graphics.DrawImage(current.CellImage, region.ToDrawingRect());
                        }
                        else
                        {
                            //Bitmap
                            int idx = slices.IndexOf(current);
                            graphics.FillRectangle(current.Brush, region.TopLeft.X + idx*slice, region.TopLeft.Y, slice, region.Height);
                        }
                    }
                }
                else
                {
                    if (current.Map != null)
                    {
                        Image tile = DrawingHelper.Images.GetImage(current.Map[logicalPosition]);
                        graphics.DrawImage(tile, region.ToDrawingRect());
                    }
                }

               
            }

            if (this == owner.Selected)
            {
                // Draw Selected cursor
                graphics.DrawRectangle(new Pen(Color.Black, 3f), region.TopLeft.X - 2, region.TopLeft.Y - 2, region.Size.Width + 4, region.Size.Height + 4);
            }
        }
    }
}
