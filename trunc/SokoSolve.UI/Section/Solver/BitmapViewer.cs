using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;
using Bitmap=SokoSolve.Common.Structures.Bitmap;

namespace SokoSolve.UI.Section.Solver
{
    /// <summary>
    /// Perform simple visualisation of multiple bitmaps
    /// </summary>
    public partial class BitmapViewer : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BitmapViewer()
        {
            InitializeComponent();

            layers = new Dictionary<string, Layer>();
            
        }

        

        /// <summary>
        /// Are there any layers set up?
        /// </summary>
        public bool HasLayers
        {
            get { return layers.Count > 0; }
        }

        /// <summary>
        /// Set a Layer (updating and keeping state)
        /// </summary>
        /// <param name="layer"></param>
        public Layer SetLayer(Layer layer)
        {
            if (layers.ContainsKey(layer.Name))
            {
                // Update

                // Keep state
                layer.IsVisible = layers[layer.Name].IsVisible;

                // Replace
                layers[layer.Name] = layer;
            }
            else
            {
                // Add
                layers.Add(layer.Name, layer);
            }

            lvLayers.BeginUpdate();
            IsListViewUpdating = true;
            lvLayers.Items.Clear();
            foreach (Layer value in layers.Values)
            {
                ListViewItem item = new ListViewItem(value.Name);

                SolidBrush br = (value.Brush as SolidBrush);
                if (br != null) item.ForeColor = Color.FromArgb(br.Color.R ^ 255, (255-br.Color.G) ^ 255, br.Color.B ^ 255);
                if (br != null) item.BackColor = br.Color;
                item.Checked = value.IsVisible;
                item.Tag = value;
                lvLayers.Items.Add(item);
            }
            lvLayers.EndUpdate();
            IsListViewUpdating = false;
            return layer;
        }

        /// <summary>
        /// Overloaded Helper.
        /// </summary>
        /// <param name="Bitmap"></param>
        /// <param name="BrushColor"></param>
        public Layer SetLayer(SolverBitmap Bitmap, Brush BrushColor)
        {
            Layer tmp = new Layer();
            tmp.Order = layers.Count;
            tmp.IsVisible = false;
            if (Bitmap != null)
            {
                tmp.Name = Bitmap.Name;
                tmp.Bitmap = Bitmap;    
            }
            else
            {
                tmp.Name = "None";
            }
            tmp.Brush = BrushColor;
            return SetLayer(tmp);
        }


        public SizeInt MapSize
        {
            get { return mapSize; }
            set 
            { 
                mapSize = value;
                vis = new BitmapViewerVisualisation(layers, mapSize);
                visualisationContainer.Visualisation = vis;
            }
        }

        private SizeInt mapSize;

        /// <summary>
        /// Draw the visualistion
        /// </summary>
        public void Render()
        {
            if (layers.Count == 0) return;
            if (vis == null) return;
            
            visualisationContainer.Render();

        }

        /// <summary>
        /// Remove all layers
        /// </summary>
        public void Clear()
        {
            layers.Clear();
        }

        /// <summary>
        /// Encapsulte a single layer
        /// </summary>
        public class Layer
        {
            public int Order;
            public string Name;
            public Brush Brush;
            public Bitmap Bitmap;
            public SokobanMap Map;
            public Matrix Matrix; 
            public Image CellImage;
            public bool IsVisible;
            public Font Font;
            public Brush BrushAlt;
        }

        private BitmapViewerVisualisation vis;
        private Dictionary<string, Layer> layers;

        private bool IsListViewUpdating = false;

        private void lvLayers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (IsListViewUpdating) return;

            Layer layer = e.Item.Tag as Layer;
            if (layer != null) layer.IsVisible = e.Item.Checked;

            // Update display
            visualisationContainer.Render();
        }
    }
}
