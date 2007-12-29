using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Nodes.Actions;
using SokoSolve.Core.UI.Nodes.Effects;

namespace SokoSolve.Core.UI.Nodes.Specific
{
    class NodePuzzleWin : NodeBase
    {
        public NodePuzzleWin(GameUI myGameUI, int myDepth, VectorInt pos) : base(myGameUI, myDepth)
        {
            CurrentAbsolute = pos;
            chain = new ActionChain();
            chain.Add(new ActionCounter(0, 100, 1, ReSizeText));

            chain.Init();



            winner = new NodeEffectText(myGameUI, myDepth+1, "Congratz!!!", new Font("Arial", 10f), font, fontBK, pos);
            myGameUI.Add(winner);
        }

        private bool ReSizeText(Action Source)
        {
            ActionCounter cc = Source as ActionCounter;
            winner.Font = new Font("Arial", 10f + (float)cc.Current);
            winner.CurrentAbsolute = CurrentAbsolute.Subtract(winner.Size.Width/2, winner.Size.Height/2);
            return true;
        }

        public override void doStep()
        {
            if (chain.PerformStep())
            {
                // Add dialog
                NodeUIDialogPuzzleWin dialog = new NodeUIDialogPuzzleWin(GameUI, 5000);
                dialog.Size = new SizeInt(400, 180);
                dialog.CurrentCentre = GameUI.GameCoords.PuzzleRegion.Center;
                GameUI.Add(dialog);

                // Remove this anim
                winner.Remove();
                Remove();
            }
        }

        private NodeEffectText winner;
        private Brush font = new SolidBrush(Color.White);
        private Brush fontBK = new SolidBrush(Color.DimGray);
        private ActionChain chain;
    }
}
