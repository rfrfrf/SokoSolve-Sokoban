using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Core.UI.Nodes.UINodes;

namespace SokoSolve.Core.UI.Nodes.Specific
{
    internal class NodeUIDialogPuzzleWin : NodeUIDialog
    {
        public NodeUIDialogPuzzleWin(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            NodeUIButton buttonNext;
            NodeUIButton buttonCancel;
            NodeUIButton buttonSave;
            NodeUIButton buttonExit;
            buttonNext = new NodeUIButton(myGameUI, myDepth + 1, CurrentAbsolute, ResourceID.GameButtonRight, "Next");
            buttonCancel = new NodeUIButton(myGameUI, myDepth + 1, CurrentAbsolute, ResourceID.GameButtonCancel, "Cancel");
            buttonSave = new NodeUIButton(myGameUI, myDepth + 1, CurrentAbsolute, ResourceID.GameButtonSave, "Save");
            buttonExit = new NodeUIButton(myGameUI, myDepth + 1, CurrentAbsolute, ResourceID.GameButtonHome, "Home");

            buttonNext.ToolTip = "Start the next puzzle";
            buttonCancel.ToolTip = "Redo this puzzle";
            buttonSave.ToolTip = "Save the solution to the library";
            buttonExit.ToolTip = "Return to home page";

            Add(buttonNext);
            Add(buttonCancel);
            Add(buttonSave);
            Add(buttonExit);

            Text = @"Congrats! You have solved the puzzle. Do you want to continue with the next puzzle in the library? Again, well done!";
            TextTitle = "Puzzle Complete";
        }

        protected override void OnClickButton(object sender, NotificationEvent e)
        {
            if (this.GameUI.OnGameWin != null)
            {
                this.GameUI.OnGameWin(this, e);
            }
        }
    }
}