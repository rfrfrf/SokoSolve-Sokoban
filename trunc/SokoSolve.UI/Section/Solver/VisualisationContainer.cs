using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;

namespace SokoSolve.UI.Section.Solver
{
    public partial class VisualisationContainer : UserControl
    {
        public VisualisationContainer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Visualisation Renderer
        /// </summary>
        [Browsable(false)]
        public Visualisation Visualisation
        {
            get { return visualisation; }
            set { visualisation = value;  ClearImage(); }
        }

        /// <summary>
        /// Basic Tooltip text
        /// </summary>
        [Browsable(true)]
        public string Status
        {
            get { return tsLabel.Text;  }
            set { tsLabel.Text = value; }
        }

        /// <summary>
        /// Should the control render if a click is performed
        /// </summary>
        [Browsable(true)]
        public bool RenderOnClick
        {
            get { return renderOnClick; }
            set { renderOnClick = value; }
        }

        /// <summary>
        /// Should the control render if a click is performed
        /// </summary>
        [Browsable(true)]
        public ToolStrip ToolStrip
        {
            get { return toolStrip; }
            set { toolStrip = value; }
        }

        /// <summary>
        /// Clear/Remove the current image
        /// </summary>
        public void ClearImage()
        {
            pictureBoxPayload.Image = null;
        }

        /// <summary>
        /// Render the Visualisation
        /// </summary>
        public void Render()
        {
            if (visualisation != null)
            {
                if (pictureBoxPayload.Image == null)
                {
                    if (visualisation.RenderCanvas == null) throw new Exception("RenderCanvas is not set");
                    pictureBoxPayload.Image = new Bitmap(visualisation.RenderCanvas.Size.Width, visualisation.RenderCanvas.Size.Height);
                }

                // Reset the size for scrolling
                pictureBoxPayload.Size = pictureBoxPayload.Image.Size;

                visualisation.Draw(Graphics.FromImage(pictureBoxPayload.Image));
                pictureBoxPayload.Refresh();
            }
        }

        private void pictureBoxPayload_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (visualisation != null)
            {
                VisualisationElement element = visualisation.GetElement(new VectorInt(e.X, e.Y));
                if (element != null)
                {
                    tsLabel.Text = element.GetDisplayData();
                    visualisation.Selected = element;
                }
                else
                {
                    tsLabel.Text = string.Format("Nothing at ({0}, {1})", e.X, e.Y);
                }

                if (OnVisualisationClick != null)
                {
                    OnVisualisationClick(this, new VisEventArgs(visualisation, element, e));
                }
                
                if (e.Clicks > 0 && renderOnClick) Render();
            }
        }

        /// <summary>
        /// When the visualisation is clicked
        /// </summary>
        public EventHandler<VisEventArgs> OnVisualisationClick;

        private Visualisation visualisation;

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            Render();
            tsLabel.Text = "Refreshed";
        }

        private bool renderOnClick;
    }

    public class VisEventArgs : EventArgs
    {
        public VisEventArgs(Visualisation visualisation, VisualisationElement element, MouseEventArgs mouse)
        {
            this.mouse = mouse;
            this.element = element;
            this.visualisation = visualisation;
        }


        public MouseEventArgs Mouse
        {
            get { return mouse; }
        }

        public VisualisationElement Element
        {
            get { return element; }
        }

        public Visualisation Visualisation
        {
            get { return visualisation; }
        }

        private MouseEventArgs mouse;
        private VisualisationElement element;
        private Visualisation visualisation;
    }
}
