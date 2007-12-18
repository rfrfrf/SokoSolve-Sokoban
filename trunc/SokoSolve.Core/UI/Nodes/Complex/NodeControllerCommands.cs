using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI.Nodes.Effects;
using SokoSolve.Core.UI.Nodes.UINodes;
using SokoSolve.Core.UI.Paths;

namespace SokoSolve.Core.UI.Nodes.Complex
{
    class NodeControllerCommands : NodeBase
    {
        public NodeControllerCommands(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            RectangleInt panel = myGameUI.GameCoords.PositionMovementCommands;
            VectorInt locCommands = myGameUI.GameCoords.PositionMovementCommands.TopLeft;

            NodeUIButton buttonUp = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(22, 2), "$Graphics/Icons/Up.png", "Up");
            buttonUp.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonUp);

            NodeUIButton buttonDown = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(22, 42), "$Graphics/Icons/Down.png", "Down");
            buttonDown.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonDown);

            NodeUIButton buttonLeft = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(2, 22), "$Graphics/Icons/Left.png", "Left");
            buttonLeft.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonLeft);

            NodeUIButton buttonRight = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(42, 22), "$Graphics/Icons/Right.png", "Right");
            buttonRight.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonRight);

            NodeUIButton buttonHelp = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(2, 42), "$Graphics/Icons/Help.png", "Help");
            buttonHelp.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonHelp);

            NodeUIButton buttonUndo = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(22, 22), "$Graphics/Icons/Undo.png", "Undo");
            buttonUndo.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonUndo);

            NodeUIButton buttonRestart = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(42, 42), "$Graphics/Icons/Restart.png", "Restart");
            buttonRestart.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonRestart);

            NodeUIButton buttonExit = new NodeUIButton(myGameUI, myDepth + 1, locCommands.Add(42, 2), "$Graphics/Icons/Exit.png", "Exit");
            buttonExit.OnClick += new EventHandler<NotificationEvent>(Button_OnClick);
            myGameUI.Add(buttonExit);

        }

        void Button_OnClick(object sender, NotificationEvent e)
        {
            switch(e.Command)
            {
                case("Undo") : GameUI.Undo(); return;
                case ("Restart"): GameUI.Reset(); return;
                case ("Exit"): GameUI.Exit(); return;
                case ("Up"): GameUI.Player.doMove(Direction.Up); return;
                case ("Down"): GameUI.Player.doMove(Direction.Down); return;
                case ("Left"): GameUI.Player.doMove(Direction.Left); return;
                case ("Right"): GameUI.Player.doMove(Direction.Right); return;

                default:
                    NodeEffectText msg = new NodeEffectText(GameUI, 100, "Command not implementd:"+e.Command, new VectorInt(100, 100));
                    msg.Path = new StaticPath(new VectorInt(100, 100), 30);
                    GameUI.Add(msg);
                    return;
            }
        }

        public override void Render()
        {
            //GameUI.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), GameUI.GameCoords.PositionMovementCommands.ToDrawingRect());
        }
    }
}
