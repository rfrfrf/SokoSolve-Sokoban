using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model.DataModel;
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
            secondLine.Font = new Font("Arial", 11f);
            secondLine.IsVisible = false;

            currentLine = 0;
            lines = GetTextLines().ToArray();

            myGameUI.Add(secondLine);

            chain = new ActionChain();
            chain.Add(new ActionMethod(SelectNextMessage));
            chain.Add(new ActionCounter(20, 250, 4, new ActionDelegate(SetAlpha))); // FadeIn    
            chain.Add(new ActionCounter(0, 50)); // wait
            chain.Add(new ActionCounter(250, 0, -7, new ActionDelegate(SetAlpha))); // FadeOut
            chain.Add(new ActionRetartChain(chain));

            chain.Init();
        }

        List<string> GetTextLines()
        {
            List<string> txt = new List<string>();

            GenericDescription desc = GameUI.Puzzle.Library.Consolidate(GameUI.Puzzle);

            if (!string.IsNullOrEmpty(desc.Description)) txt.Add(desc.Description);
            if (desc.DateSpecified) txt.Add(string.Format("Created: {0}", desc.Date.ToLongDateString()));
            if (!string.IsNullOrEmpty(desc.Comments)) txt.Add(desc.Comments);
            if (!string.IsNullOrEmpty(desc.License)) txt.Add("License: " +desc.License);
            if (desc.Author != null)
            {
                if (!string.IsNullOrEmpty(desc.Author.Name)) txt.Add(desc.Author.Name);
                if (!string.IsNullOrEmpty(desc.Author.Homepage)) txt.Add(desc.Author.Homepage);
            }
            return txt;
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

        private readonly int maxSecondLineChars = 70;

        bool SelectNextMessage(Action current)
        {
            secondLine.Text = lines[currentLine];
            if (secondLine.Text.Length > maxSecondLineChars)
            {
                secondLine.Text = secondLine.Text.Substring(0, maxSecondLineChars) + "...";
            }
            currentLine++;
            if (currentLine >= lines.Length) currentLine = 0;
            return true;
        }

       

        public override void doStep()
        {
            base.doStep();

            chain.PerformStep();

            // Redock and Resize second line
            secondLine.CurrentAbsolute = new VectorInt(GameUI.GameCoords.WindowRegion.TopMiddle.Subtract(secondLine.Size.Width/2,-30));
        }

        private ActionChain chain;
        private NodeEffectText secondLine;
        private string[] lines;
        private int currentLine;
    }
}

