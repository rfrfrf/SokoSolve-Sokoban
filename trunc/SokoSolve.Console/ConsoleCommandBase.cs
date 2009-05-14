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

        /// <summary>
        /// Command argument name
        /// </summary>
        public string CommandName
        {
            get { return this.commandName; }
        }

        /// <summary>
        /// Friendly command description for help display
        /// </summary>
        public string CommandDesc
        {
            get { return this.commandDesc; }
        }

        /// <summary>
        /// Friendly command example for help display
        /// </summary>
        public string CommandExample
        {
            get { return this.commandExample; }
        }

        /// <summary>
        /// The min number of REQUIRED parameters
        /// </summary>
        public int MinParams
        {
            get { return this.minParams; }
        }

        /// <summary>
        /// Allow a command to be default, if no specific command is given in command-line arguments
        /// </summary>
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

        /// <summary>
        /// The command implementation
        /// </summary>
        /// <param name="controller">The source for parameters, logging, display and meta-data</param>
        /// <returns>Success/Failure return code</returns>
        public abstract ReturnCodes Execute(ConsoleCommandController controller);
        
    }
}
