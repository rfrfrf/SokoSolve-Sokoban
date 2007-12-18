using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Nodes.Complex;

namespace SokoSolve.Core.UI.Nodes.UINodes
{

   

    class NodeUIButton : NodeUI
    {
        public NodeUIButton(GameUI myGameUI, int myDepth, VectorInt PositionAbs, string FileImage, string ClickCommand) : base(myGameUI, myDepth)
        {
            imageNormal = new Bitmap(FileManager.getContent(FileImage));
            Size = new SizeInt(imageNormal.Size);
            CurrentAbsolute = PositionAbs;
            clickCommand = ClickCommand;

            NodeCursor cursor = (NodeCursor) GameUI.Find(typeof (NodeCursor));
            if (cursor != null)
            {
                cursor.OnClick += new EventHandler<NodeCursorEventArgs>(cursor_OnClick);
            }
        }

        void cursor_OnClick(object sender, NodeCursorEventArgs e)
        {
            if (e.Clicks > 0)
            {
                if (this.CurrentRect.Contains(new VectorInt(e.X, e.Y)))
                {
                    if (OnClick != null)
                    {
                        OnClick(this, new NotificationEvent(this, clickCommand, null));
                    }
                }
            }
        }


        public event EventHandler<NotificationEvent> OnClick;

        /// <summary>
        /// Clickable image
        /// </summary>
        public Bitmap ImageNormal
        {
            get { return imageNormal; }
            set { imageNormal = value; }
        }

        public override void Render()
        {
            // For Debug - a region box:
            // GameUI.Graphics.DrawRectangle(new Pen(Color.WhiteSmoke), CurrentAbsolute.X, CurrentAbsolute.Y, Size.Width, Size.Height);

            GameUI.Graphics.DrawImage(imageNormal, CurrentAbsolute.X, CurrentAbsolute.Y);
        }

        private Bitmap imageNormal;
        private string clickCommand;
    }
}
