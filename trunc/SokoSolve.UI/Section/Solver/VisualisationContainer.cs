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
            set { visualisation = value; }
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
        /// Render the Visualisation
        /// </summary>
        public void Render()
        {
            if (visualisation != null)
            {
                if (pictureBoxPayload.Image == null)
                {
                    pictureBoxPayload.Image = new Bitmap(visualisation.RenderCanvas.Size.Width, visualisation.RenderCanvas.Size.Height);
                }

                visualisation.Draw(Graphics.FromImage(pictureBoxPayload.Image));
                pictureBoxPayload.Refresh();
            }
        }

        private void pictureBoxPayload_MouseClick(object sender, MouseEventArgs e)
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
                
                if (e.Clicks > 0) Render();
            }
        }

        public EventHandler<VisEventArgs> OnVisualisationClick;

        private Visualisation visualisation;

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            Render();
            tsLabel.Text = "Refreshed";
        }
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
