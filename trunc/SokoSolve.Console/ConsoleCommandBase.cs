using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Console
{
    /// <summary>
    /// A abstract helper class to manage a Console Application
    /// </summary>
    public abstract class ConsoleCommandBase
    {
        private string commandName;
        private string commandDesc;
        private string commandExample;
        private int minParams;

        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="commandName">Example: PING</param>
        /// <param name="commandDesc">Example: PING a machine on the internet</param>
        /// <param name="commandExample">Example: PING www.nba.com</param>
        /// <param name="minParams">Example: 1</param>
        protected ConsoleCommandBase(string commandName, string commandDesc, string commandExample, int minParams)
        {
            this.commandName = commandName;
            this.commandDesc = commandDesc;
            this.commandExample = commandExample;
            this.minParams = minParams;
        }

        public string CommandName
        {
            get { return this.commandName; }
        }

        public string CommandDesc
        {
            get { return this.commandDesc; }
        }

        public string CommandExample
        {
            get { return this.commandExample; }
        }

       

        public int MinParams
        {
            get { return this.minParams; }
        }

        /// <summary>
        /// Can this command process this command name
        /// </summary>
        /// <param name="argCommandName"></param>
        /// <returns></returns>
        public virtual bool CanProcess(string argCommandName)
        {
            return commandName.ToLower() == argCommandName.ToLower();
        }

        public abstract int Execute(ConsoleCommandController controller);


        
    }
}
