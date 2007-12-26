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
            buttonOk = new NodeUIButton(myGameUI, myDepth+1, CurrentAbsolute, "$Graphics/Icons/Right.png", "Exit");
            buttonCancel = new NodeUIButton(myGameUI, myDepth + 1, CurrentAbsolute, "$Graphics/Icons/Cancel.png", "Exit");
            buttonSave = new NodeUIButton(myGameUI, myDepth + 1, CurrentAbsolute, "$Graphics/Icons/Save.png", "Exit");
            buttonExit = new NodeUIButton(myGameUI, myDepth + 1, CurrentAbsolute, "$Graphics/Icons/Home.png", "Exit");

            myGameUI.Add(buttonOk);
            myGameUI.Add(buttonCancel);
            myGameUI.Add(buttonSave);
            myGameUI.Add(buttonExit);
        }

        private NodeUIButton buttonOk;
        private NodeUIButton buttonCancel;
        private NodeUIButton buttonSave;
        private NodeUIButton buttonExit;

        private int sizeTitle;

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

        public override void doStep()
        {
            buttonOk.CurrentCentre = CurrentRect.BottomMiddle.Subtract(0, 20);
            buttonCancel.CurrentCentre = CurrentRect.BottomMiddle.Subtract(40, 20);
            buttonSave.CurrentCentre = CurrentRect.BottomMiddle.Subtract(-40, 20);
            buttonExit.CurrentCentre = CurrentRect.BottomMiddle.Subtract(-80, 20);
        }

        public override void Render()
        {
            // Draw entire surface
            GameUI.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 20, 20, 20)), CurrentRect.ToDrawingRect());

            // Draw Title
            GameUI.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(210, Color.LavenderBlush)), TitleRect.ToDrawingRect());
            GameUI.Graphics.DrawString(textTitle, new Font("Arial", 13f, FontStyle.Bold), new SolidBrush(Color.DarkBlue), TitleRect.ToDrawingRect());

            // Draw Text
            GameUI.Graphics.DrawString(text, new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.White), TextRect.ToDrawingRect());
        }

        private string text = @"Congrats! You have solved the puzzle. Do you want to continue with the next puzzle in the library? Again, well done!";
        private string textTitle = "Puzzle Complete";
    }
}

