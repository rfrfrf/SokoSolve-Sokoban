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
        public void SetLayer(Layer layer)
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

            if (layerButton == null)
            {
                layerButton = new ToolStripDropDownButton("Layers");
                visualisationContainer.toolStrip.Items.Add(layerButton);
            }

            layerButton.DropDownItems.Clear();
            foreach (Layer value in layers.Values)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(value.Name);
                SolidBrush br = (value.Brush as SolidBrush);
                if (br != null) item.ForeColor = Color.FromArgb(br.Color.R ^ 200, br.Color.G ^ 200, br.Color.B ^ 200);
                if (br != null) item.BackColor = br.Color;
                item.Checked = value.IsVisible;
                layerButton.DropDownItems.Add(item);
                item.Tag = value;
                item.Alignment = ToolStripItemAlignment.Right;
                item.Click += new EventHandler(LayerButton_OnClick);

            }
            
        }

        void LayerButton_OnClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                Layer value = item.Tag as Layer;
                if (value != null)
                {
                    value.IsVisible = !value.IsVisible;
                    item.Checked = value.IsVisible;
                }
            }

            // Update display
            visualisationContainer.Render();
        }

        private ToolStripDropDownButton layerButton;

        /// <summary>
        /// Overloaded Helper.
        /// </summary>
        /// <param name="Bitmap"></param>
        /// <param name="BrushColor"></param>
        public void SetLayer(SolverBitmap Bitmap, Brush BrushColor)
        {
            Layer tmp = new Layer();
            tmp.Order = layers.Count;
            tmp.IsVisible = true;
            tmp.Name = Bitmap.Name;
            tmp.Brush = BrushColor;
            tmp.Bitmap = Bitmap;
            SetLayer(tmp);
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
            public Image CellImage;
            public bool IsVisible;
        }

        private BitmapViewerVisualisation vis;
        private Dictionary<string, Layer> layers;
    }
}