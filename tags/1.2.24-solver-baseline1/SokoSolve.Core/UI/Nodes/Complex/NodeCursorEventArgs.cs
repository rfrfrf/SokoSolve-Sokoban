using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SokoSolve.Core.UI.Nodes.Complex
{
    /// <summary>
    /// See <see cref="NodeCursor"/>
    /// </summary>
    public class NodeCursorEventArgs : NotificationEvent
    {
        public NodeCursorEventArgs(NodeBase source, string command, object tag, 
            int X, int Y, int ClickCount, GameUI.MouseButtons Button, GameUI.MouseClicks ClickType) : base(source, command, tag)
        {
            this.x = X;
            this.y = Y;
            this.clicks = ClickCount;
            this.button = Button;
            this.clicktype = ClickType;
        }


        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Clicks
        {
            get { return clicks; }
        }

        public GameUI.MouseButtons Button
        {
            get { return button; }
        }


        public GameUI.MouseClicks Clicktype
        {
            get { return clicktype; }
        }

        private int x;
        private int y;
        private int clicks;
        private GameUI.MouseButtons button;
        private GameUI.MouseClicks clicktype;
    }

   
}
