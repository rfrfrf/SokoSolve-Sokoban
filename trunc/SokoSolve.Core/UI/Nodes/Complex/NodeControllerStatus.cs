using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI.Nodes.Effects;

namespace SokoSolve.Core.UI.Nodes.Complex
{
    /// <summary>
    /// This node controlls other nodes
    /// </summary>
    class NodeControllerStatus : NodeBase
    {
        public NodeControllerStatus(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            VectorInt locStatus = myGameUI.GameCoords.PositionStatus;

            labelMoves = new NodeEffectText(myGameUI, myDepth + 1, "Moves", locStatus);
            valueMoves = new NodeEffectText(myGameUI, myDepth + 1, "", locStatus.Add(0, 20));
            valueMoves.Brush = new SolidBrush(Color.LightBlue);
            myGameUI.Add(labelMoves);
            myGameUI.Add(valueMoves);

            labelPushes = new NodeEffectText(myGameUI, myDepth + 1, "Pushes", locStatus.Add(0, 40));
            valuePushes = new NodeEffectText(myGameUI, myDepth + 1, "", locStatus.Add(0, 60));
            valuePushes.Brush = new SolidBrush(Color.LightBlue);
            myGameUI.Add(labelPushes);
            myGameUI.Add(valuePushes);

            labelComplete = new NodeEffectText(myGameUI, myDepth + 1, "Complete", locStatus.Add(0, 80));
            valueComplete = new NodeEffectText(myGameUI, myDepth + 1, "", locStatus.Add(0, 100));
            valueComplete.Brush = new SolidBrush(Color.LightBlue);
            myGameUI.Add(labelComplete);
            myGameUI.Add(valueComplete);

            labelUndo = new NodeEffectText(myGameUI, myDepth + 1, "Undo", locStatus.Add(0, 120));
            valueUndo = new NodeEffectText(myGameUI, myDepth + 1, "", locStatus.Add(0, 140));
            valueUndo.Brush = new SolidBrush(Color.LightBlue);
            myGameUI.Add(labelUndo);
            myGameUI.Add(valueUndo);

            labelRestart = new NodeEffectText(myGameUI, myDepth + 1, "Restart", locStatus.Add(0, 160));
            valueRestart = new NodeEffectText(myGameUI, myDepth + 1, "", locStatus.Add(0, 180));
            valueRestart.Brush = new SolidBrush(Color.LightBlue);
            myGameUI.Add(labelRestart);
            myGameUI.Add(valueRestart);

            labelTime = new NodeEffectText(myGameUI, myDepth + 1, "Time", locStatus.Add(0, 200));
            valueTime = new NodeEffectText(myGameUI, myDepth + 1, "", locStatus.Add(0, 220));
            valueTime.Brush = new SolidBrush(Color.LightBlue);
            myGameUI.Add(labelTime);
            myGameUI.Add(valueTime);            
        }

        public override void doStep()
        {
            valueMoves.Text = string.Format("{0}/{1}", GameUI.Stats.Moves, GameUI.Stats.MovesTotal);
            valuePushes.Text = string.Format("{0}/{1}", GameUI.Stats.Pushes, GameUI.Stats.PushesTotal);
            valueComplete.Text = string.Format("{0}%", PuzzleAnalysis.PercentageComplete(GameUI.Current));
            valueUndo.Text = GameUI.Stats.Undos.ToString();
            valueRestart.Text = GameUI.Stats.Restarts.ToString();
            valueTime.Text = GameUI.Stats.Duration.ToString();
        }

        private NodeEffectText labelMoves;
        private NodeEffectText valueMoves;

        private NodeEffectText labelPushes;
        private NodeEffectText valuePushes;

        private NodeEffectText labelComplete;
        private NodeEffectText valueComplete;

        private NodeEffectText labelUndo;
        private NodeEffectText valueUndo;

        private NodeEffectText labelRestart;
        private NodeEffectText valueRestart;

        private NodeEffectText labelTime;
        private NodeEffectText valueTime;
    }
}
