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
            imageBack = new Bitmap(FileManager.getContent("$Graphics\\Icons\\BasicButtonBK.png"));
            Size = new SizeInt(imageNormal.Size);
            CurrentAbsolute = PositionAbs;
            clickCommand = ClickCommand;

            if (GameUI.Cursor != null)
            {
                GameUI.Cursor.OnClick += new EventHandler<NodeCursorEventArgs>(cursor_OnClick);
            }
        }

        void cursor_OnClick(object sender, NodeCursorEventArgs e)
        {
            if (e.Clicks > 0 && e.Clicktype == GameUI.MouseClicks.Up)
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
            int offset = 6;

            Brush bkBrush = new SolidBrush(Color.FromArgb(140, Color.WhiteSmoke));
            RectangleInt fullSize = new RectangleInt(CurrentRect.TopLeft.Subtract(offset, offset), new SizeInt(CurrentRect.Width + 2 * offset, CurrentRect.Height + 2 * offset));
            //if (fullSize.Contains(GameUI.Cursor.CurrentAbsolute))
            //{
            //    bkBrush = new SolidBrush(Color.FromArgb(180,  Color.DarkGray));

            //    GameUI.Graphics.FillEllipse(bkBrush, CurrentAbsolute.X - offset, CurrentAbsolute.Y - offset, Size.Width + (2 * offset), Size.Height + (2 * offset));
            //}

            DrawBitmapCentered(GameUI.Graphics, CurrentRect, imageBack);
            DrawBitmapCentered(GameUI.Graphics, CurrentRect, imageNormal);
        }

        private static void DrawBitmapCentered(Graphics Graphics, RectangleInt Target, Bitmap image)
        {
            VectorInt pos = Target.Center.Subtract(image.Width/2, image.Height/2);
            Graphics.DrawImage(image, pos.X, pos.Y);
        }

        private Bitmap imageNormal;
        private Bitmap imageBack;
        private string clickCommand;
    }
}
