using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI;
using SokoSolve.Core.Game;
using SokoSolve.Core.UI.Nodes.Effects;
using SokoSolve.Core.UI.Nodes.Specific;
using SokoSolve.Core.UI.Nodes.UINodes;

namespace SokoSolve.Core.UI.Nodes
{
    /// <summary>
    /// This is a key class, as it also acts as the mechanism to drive the input
    /// </summary>
    public class NodeDynamicPlayer : NodeDynamic
    {
        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="myGameUI"></param>
        /// <param name="myPuzzleLocation"></param>
        /// <param name="myDepth"></param>
        public NodeDynamicPlayer(GameUI myGameUI, VectorInt myPuzzleLocation, int myDepth) : base(myGameUI, Cell.Player, myPuzzleLocation, myDepth)
        {
            futureMoves = new Queue<Direction>();
        }

        public bool HasFutureMoves
        {
            get
            {
                return futureMoves.Count > 0;
            }
        }

        /// <summary>
        /// Perform a single move
        /// </summary>
        /// <param name="aDirection"></param>
        public void doMove(Direction aDirection)
        {
            futureMoves.Enqueue(aDirection);        
        }

        /// <summary>
        /// Perform a unit of puzzle time
        /// </summary>
        public override void doStep()
        {
            base.doStep();

            //if (target == CurrentAbsolute)
            {
                if (futureMoves.Count > 0)
                {
                    if (moveCurrentWait > 0)
                    {
                        moveCurrentWait--;
                    }
                    else
                    {
                        // Introduce a delay
                        moveCurrentWait = moveWaitPeriod;

                        Game.Game.MoveResult res = GameUI.Move(futureMoves.Dequeue());
                        if (res == Game.Game.MoveResult.Invalid)
                        {
                            string[] rndMsg = GameUI.ResourceFactory[ResourceID.GameMiscStringInvalidMove].DataAsStringArray;
                            NodeEffectText msg = new NodeEffectText(GameUI, 20, rndMsg, CurrentCentre);
                            msg.Brush = new SolidBrush(Color.Red);
                            GameUI.Add(msg);

                            GameUI.Sound.PlaySound(GameUI.ResourceFactory[ResourceID.GameSoundInvalid].DataAsSound);
                        } 
                        else if (res == Game.Game.MoveResult.ValidPushGoal)
                        {
                            string[] rndMsg = GameUI.ResourceFactory[ResourceID.GameMiscStringCrateGoal].DataAsStringArray;
                            NodeEffectText msg = new NodeEffectText(GameUI, 20, rndMsg, CurrentCentre);
                            GameUI.Add(msg);

                            GameUI.Sound.PlaySound(GameUI.ResourceFactory[ResourceID.GameSoundPushGoal].DataAsSound);
                        }
                        else if (res == Game.Game.MoveResult.ValidPushWin)
                        {
                            
                            if (GameUI.InitType == UI.GameUI.InitTypes.SolutionReplay)
                            {
                                NodeEffectText msg = new NodeEffectText(GameUI, 20, "Complete. ESC to exit.", CurrentCentre);
                                msg.Path = new Paths.StaticPath(CurrentCentre);
                                GameUI.Add(msg);
                            }
                            else
                            {
                                NodePuzzleWin win = new NodePuzzleWin(GameUI, 5000, GameUI.GameCoords.PuzzleRegion.Center);
                                GameUI.Add(win);

                                GameUI.Sound.PlaySound(GameUI.ResourceFactory[ResourceID.GameSoundWin].DataAsSound);
                            }
                        }
                    }
                    
                }
            }
        }

        Queue<Direction> futureMoves;
        private int moveWaitPeriod = 0; // frames
        private int moveCurrentWait;
    }
}

