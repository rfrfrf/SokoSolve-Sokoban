using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI.Nodes
{
    class NotificationEvent : EventArgs
    {
        public NotificationEvent(NodeBase source, string command, object tag)
        {
            this.source = source;
            this.command = command;
            this.tag = tag;
        }

        public NodeBase Source
        {
            get { return source; }
        }

        public string Command
        {
            get { return command; }
        }

        public object Tag
        {
            get { return tag; }
        }

        private NodeBase source;
        private string command;
        private object tag;
    }
}
