using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Nodes.Actions;
using SokoSolve.Core.UI.Nodes.UINodes;
using SokoSolve.Core.UI.Paths;

namespace SokoSolve.Core.UI.Nodes.Specific
{
    class NodeUIDialogHelp : NodeUIDialog
    {
        public NodeUIDialogHelp(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            Text =
                @"Place all crates/gem on the goal positions.
You can push but never pull crates, you can only push one crate at a time.

Use the arrow keys to move. [U] for Undo, [R] for Restart. [ESC] to exit.

Click on a floor to move there. Drag a crate to a new position.";
            TextTitle = "Sokoban Help";

            CurrentAbsolute = GameUI.GameCoords.PuzzleRegion.Center.Subtract(100, 100);
            Size = new SizeInt(500, 200);
        }

        public override void doStep()
        {
            if (counter.PerformStep()) Remove();
        }

        ActionCounter counter = new ActionCounter(0, 100, 1);
    }
}
