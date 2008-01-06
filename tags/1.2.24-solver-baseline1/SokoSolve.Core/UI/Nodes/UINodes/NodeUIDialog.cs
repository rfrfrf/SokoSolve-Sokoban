using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;

namespace SokoSolve.Core.UI.Nodes.UINodes
{
    class NodeUIDialog : NodeUI
    {
        public NodeUIDialog(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            buttons = new List<NodeUIButton>();

            doStep(); // This will set the initial positions
        }

    

        RectangleInt TitleRect
        {
            get
            {
                return new RectangleInt(CurrentAbsolute.Add(3, 3), new SizeInt(CurrentRect.Width - 6, 30));
            }
        }

        RectangleInt TextRect
        {
            get
            {
                return new RectangleInt(CurrentAbsolute.Add(3, 33), new SizeInt(CurrentRect.Width - 6, CurrentRect.Height-33));
            }
        }

        RectangleInt CommandsRect
        {
            get
            {
                return new RectangleInt(CurrentRect.BottomLeft.Subtract(-3, 23), new SizeInt(CurrentRect.Width - 6, 20));
            }
        }

        public void Add(NodeUIButton button)
        {
            buttons.Add(button);
            if (!GameUI.HasNode(button))
            {
                GameUI.Add(button);
            }

            button.OnClick += new EventHandler<NotificationEvent>(OnClickButton);
        }

        protected virtual void OnClickButton(object sender, NotificationEvent e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string TextTitle
        {
            get { return textTitle; }
            set { textTitle = value; }
        }

        public override void doStep()
        {
            // Dock the buttons
            int offset = 0;
            foreach (NodeUIButton button in buttons)
            {
                button.CurrentCentre = CurrentRect.BottomMiddle.Subtract(offset, 23);
                offset -= 40;
            }
        }

        public override void Render()
        {
            // Draw entire surface
            GameUI.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 10, 10, 10)), CurrentRect.ToDrawingRect());

            // Draw border
            GameUI.Graphics.DrawRectangle(new Pen(Color.DarkGray), CurrentRect.ToDrawingRect());

            // Draw Title
            GameUI.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(210, Color.DarkBlue)), TitleRect.ToDrawingRect());
            GameUI.Graphics.DrawString(textTitle, new Font("Lucida Console", 13f, FontStyle.Bold), new SolidBrush(Color.Yellow), TitleRect.ToDrawingRect());

            // Draw Text
            GameUI.Graphics.DrawString(text, new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.White), TextRect.ToDrawingRect());
        }

        private string text;
        private string textTitle;
        private List<NodeUIButton> buttons;
        private int sizeTitle;
    }
}

