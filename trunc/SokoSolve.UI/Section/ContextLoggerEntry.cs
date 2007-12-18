using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.UI.Section
{
    public class ContextLoggerEntry
    {
        private string message;
        private Type source;
        private int depth;


        public ContextLoggerEntry(string message, Type source, int depth)
        {
            this.message = message;
            this.source = source;
            this.depth = depth;
        }


        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public Type Source
        {
            get { return source; }
            set { source = value; }
        }

        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }
    }
}
