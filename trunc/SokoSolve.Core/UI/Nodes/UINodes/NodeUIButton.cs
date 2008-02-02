using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Nodes.Complex;
using SokoSolve.Core.UI.Nodes.Effects;

namespace SokoSolve.Core.UI.Nodes.UINodes
{
    /// <summary>
    /// Provide a very basic 'button' for simple interactions
    /// </summary>
    class NodeUIButton : NodeUI
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="myGameUI"></param>
        /// <param name="myDepth"></param>
        /// <param name="PositionAbs"></param>
        /// <param name="resImage">Resource of the UI button image</param>
        /// <param name="ClickCommand">Command string passed back in the click event</param>
        public NodeUIButton(GameUI myGameUI, int myDepth, VectorInt PositionAbs, ResourceID resImage, string ClickCommand) : base(myGameUI, myDepth)
        {
            imageNormal = myGameUI.ResourceFactory[resImage].DataAsImage;
            imageBack = myGameUI.ResourceFactory[ResourceID.GameButtonBackGround].DataAsImage;
            Size = new SizeInt(imageBack.Size);
            CurrentAbsolute = PositionAbs;
            clickCommand = ClickCommand;

            mouseOverBrush = myGameUI.ResourceFactory[ResourceID.GameButtonMouseOverBrush].DataAsBrush;
            maskEffect = null;
            toolTip = null;

            if (GameUI.Cursor != null)
            {
                GameUI.Cursor.OnClick += new EventHandler<NodeCursorEventArgs>(cursor_OnClick);
            }
        }

        /// <summary>
        /// Get mouse information, relayed from the cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cursor_OnClick(object sender, NodeCursorEventArgs e)
        {
            isMouseOver = this.CurrentRect.Contains(new VectorInt(e.X, e.Y));
            if (!isMouseOver) return; // We never care about the mouse, unless it is on the button

            if (e.Clicks > 0 && e.Clicktype == GameUI.MouseClicks.Up)
            {
                 if (OnClick != null)
                 {
                     OnClick(this, new NotificationEvent(this, clickCommand, e));
                 }
            }
            else
            {
                if (OnMouseOver != null)
                {
                    OnMouseOver(this, new NotificationEvent(this, clickCommand, e));
                }
            }
        }

        /// <summary>
        /// Allow downstream objects to capture the button click event
        /// </summary>
        public event EventHandler<NotificationEvent> OnClick;

        /// <summary>
        /// When the mouse is positioned on the button
        /// </summary>
        public event EventHandler<NotificationEvent> OnMouseOver;

        /// <summary>
        /// Clickable image
        /// </summary>
        public Image ImageNormal
        {
            get { return imageNormal; }
            set 
            {
                imageNormal = value; 
            }
        }

        /// <summary>
        /// Background image
        /// </summary>
        public Image ImageBack
        {
            get { return imageBack; }
            set 
            { 
                imageBack = value;
                if (imageBack == null)
                {
                    Size = new SizeInt(imageNormal.Width, imageNormal.Height);
                }
            }
        }

        /// <summary>
        /// Tool tip text, null is none
        /// </summary>
        public string ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }


        public Brush MaskEffect
        {
            get { return maskEffect; }
            set { maskEffect = value; }
        }

        public override void Render()
        {
            
            if (imageBack != null)
            {
                VectorInt pos = CurrentRect.Center.Subtract(imageBack.Width / 2 + 2, imageBack.Height / 2 + 2);
                GameUI.Graphics.DrawImage(imageBack, pos.X, pos.Y);    
            }
            
            if (imageNormal != null)
            {
                DrawBitmapCentered(GameUI.Graphics, CurrentRect, imageNormal);    
            }
            

            if (isMouseOver)
            {
                if (mouseOverBrush != null)
                {
                    RectangleInt over = new RectangleInt(CurrentAbsolute.Subtract(1, 1), CurrentRect.BottomRight.Add(1, 1));
                    GameUI.Graphics.FillEllipse(mouseOverBrush, over.ToDrawingRect());
                }

                if (toolTip != null)
                {
                    Font ttFont = new Font("Arial", 10f);
                    SizeF ffSizeF = GameUI.Graphics.MeasureString(toolTip, ttFont);
                    SizeInt ffSize = new SizeInt((int) ffSizeF.Width, (int) ffSizeF.Height);

                    RectangleInt posTT = new RectangleInt(CurrentRect.BottomLeft.Subtract(ffSize.Width/2, -5 ), ffSize);

                    if (posTT.TopLeft.X < GameUI.GameCoords.WindowRegion.TopLeft.X)
                        posTT.TopLeft.X = GameUI.GameCoords.WindowRegion.TopLeft.X;
                    if (posTT.TopLeft.Y < GameUI.GameCoords.WindowRegion.TopLeft.Y)
                        posTT.TopLeft.Y = GameUI.GameCoords.WindowRegion.TopLeft.Y;

                    if (posTT.BottomRight.X > GameUI.GameCoords.WindowRegion.BottomRight.X)
                        posTT.BottomRight.X = GameUI.GameCoords.WindowRegion.BottomRight.X;
                    if (posTT.BottomRight.Y < GameUI.GameCoords.WindowRegion.BottomRight.Y)
                        posTT.BottomRight.Y = GameUI.GameCoords.WindowRegion.BottomRight.Y;


                    Brush ttBrush = new SolidBrush(Color.Cyan);
                    Brush ttBrushShadow = new SolidBrush(Color.DarkCyan);

                    NodeEffectText nodeTT = new NodeEffectText(GameUI, 10000, toolTip, ttFont, ttBrush, ttBrushShadow, posTT.TopLeft);
                    nodeTT.BrushBackGround = new SolidBrush(Color.DarkSlateGray);
                    nodeTT.Path = new Paths.StaticPath(posTT.TopLeft, 1);
                    GameUI.Add(nodeTT);
                }
            }

            if (maskEffect != null)
            {
                RectangleInt over = new RectangleInt(CurrentAbsolute.Subtract(5, 5), CurrentRect.BottomRight.Add(10, 10));
                GameUI.Graphics.FillEllipse(maskEffect, over.ToDrawingRect());
            }
        }

        private static void DrawBitmapCentered(Graphics Graphics, RectangleInt Target, Image image)
        {
            VectorInt pos = Target.Center.Subtract(image.Width/2, image.Height/2);
            Graphics.DrawImage(image, pos.X, pos.Y);
        }

        private bool isMouseOver;
        private Image imageNormal;
        private Image imageBack;
        private string clickCommand;
        private string toolTip;
        private Brush maskEffect;
        private Brush mouseOverBrush;
    }

}
