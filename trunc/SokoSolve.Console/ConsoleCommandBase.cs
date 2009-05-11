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
        private bool isDefaultCommand;
        protected ConsoleCommandController controller;

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

        /// <summary>
        /// This will be automatically called when this command is enrolled with the controller 
        /// </summary>
        /// <param name="consoleCommandController"></param>
        protected internal void Enroll(ConsoleCommandController consoleCommandController)
        {
            controller = consoleCommandController;
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

        public bool IsDefaultCommand
        {
            get { return isDefaultCommand; }
            set { isDefaultCommand = value; }
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

        public abstract ReturnCodes Execute(ConsoleCommandController controller);


        
    }
}
