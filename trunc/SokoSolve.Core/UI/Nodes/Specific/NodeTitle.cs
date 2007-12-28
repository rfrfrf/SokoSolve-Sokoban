using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Nodes.Actions;
using SokoSolve.Core.UI.Nodes.Effects;

namespace SokoSolve.Core.UI.Nodes.Specific
{
    class NodeTitle : NodeEffectText
    {
        public NodeTitle(GameUI myGameUI, int myDepth) : 
            base(myGameUI, 
                myDepth, 
                myGameUI.Puzzle.Details.Name,
                new Font("Lucida Console", 17f),
                new SolidBrush(Color.Yellow),
                new SolidBrush(Color.DarkOrange),
                new VectorInt(myGameUI.GameCoords.WindowRegion.TopMiddle.Add(-50, 3)))
        {
            secondLine = new NodeEffectText(myGameUI, myDepth - 1, "Sokoban rocks!", CurrentAbsolute.Add(0, 30));
            secondLine.Brush = new SolidBrush(secondLineColour);
            secondLine.IsVisible = false;

            lines = new string[]
                {
                    "Sokoban rocks!",
                    myGameUI.Puzzle.Details.Name,
                    myGameUI.Puzzle.Details.Description,
                    "David S. Skinner",
                    "http://sokosolve.sf.net/",
                    DateTime.Now.ToLongDateString()
                };

            myGameUI.Add(secondLine);

            chain = new ActionChain();
            chain.Add(new ActionMethod(SelectNextMessage));
            chain.Add(new ActionCounter(20, 250, 4, new ActionDelegate(SetAlpha))); // FadeIn    
            chain.Add(new ActionCounter(0, 50)); // wait
            chain.Add(new ActionCounter(250, 0, -7, new ActionDelegate(SetAlpha))); // FadeOut
            chain.Add(new ActionRetartChain(chain));

            chain.Init();
        }

        private Color secondLineColour = Color.Sienna;


        bool SetAlpha(Action current)
        {
            secondLine.IsVisible = true;
            ActionCounter ctr = current as ActionCounter;
            if (ctr != null)
            {
                secondLine.Brush = new SolidBrush(Color.FromArgb(ctr.Current, Color.Salmon));
                secondLine.BrushShaddow = new SolidBrush(Color.FromArgb(ctr.Current, Color.Black));
            }
            return true;
        }

        bool SelectNextMessage(Action current)
        {
            secondLine.Text = RandomHelper.Select<string>(lines);
            return true;
        }

       

        public override void doStep()
        {
            base.doStep();

            chain.PerformStep();
        }

        private ActionChain chain;
        private NodeEffectText secondLine;
        private string[] lines;
    }
}

