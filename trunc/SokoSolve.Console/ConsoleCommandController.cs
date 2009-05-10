using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SokoSolve.Console
{
    /// <summary>
    /// This is a helper class to allow multiple commands with multiple parameters to be quickly built for a Console app.
    /// </summary>
    public class ConsoleCommandController
    {
        private string applicationName;
        private string applicationDescription;
        private List<ConsoleCommandBase> commands;
        private string[] args;

        /// <summary>
        /// Strong Construction
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="applicationDescription"></param>
        public ConsoleCommandController(string applicationName, string applicationDescription)
        {
            this.applicationName = applicationName;
            this.applicationDescription = applicationDescription;
            commands = new List<ConsoleCommandBase>();
        }

        public string FindArg(string Name)
        {
            foreach (string arg in args)
            {
                if (arg.StartsWith(Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return arg.Remove(0, Name.Length);
                }
            }
            return null;
        }

        /// <summary>
        /// Find the argument (not including the command name)
        /// </summary>
        /// <param name="Index">cmd.exe PLAY file; here file is index 0</param>
        /// <returns></returns>
        public string FindArg(int Index)
        {
            return args[Index + 1];
        }

        public void Add(ConsoleCommandBase cmd)
        {
            commands.Add(cmd);
        }

        public int Execute(string[] ExecuteArgs)
        {
            int result = ExecuteInternal(ExecuteArgs);
            Display("Exiting with ReturnCode: {0}.", result);
            return result;
        }

        public int ExecuteInternal(string[] ExecuteArgs)
        {
            args = ExecuteArgs;
            try
            {
                DisplayHeader(applicationName);
                Display(string.Empty);

                // Using the first argument check to see if there is a command we can execute
                foreach (ConsoleCommandBase command in commands)
                {
                    if (command.CanProcess(args[0]))
                    {
                        if (command.MinParams > args.Length -1)
                        {
                            Display("This command {0} requires {1} params, only {2} was given.", command.CommandName, args.Length, command.MinParams);
                            return -3;
                        }

                        try
                        {
                            Display("Attempting {0}...", command.CommandName.ToUpper());
                            int result = command.Execute(this);
                            return result;
                        }
                        catch(Exception ex)
                        {
                            Display(ex);
                            return -4;
                        }
                    }
                }

                // No Command Found
                Display("No commands Found");
                Display(string.Empty);
                DisplayHelp();

                return -2;
            }
            catch(Exception ex)
            {
                Display(ex);
                return -1;
            }
        }

        public void DisplayHelp()
        {
            DisplayHeader("Commad Help:");
            Display("In for format APPLICTION.EXE COMMAND param1 param2 -arg:123");
            foreach (ConsoleCommandBase command in commands)
            {
                DisplayHelp(command);
            }
        }


        public void DisplayHelp(ConsoleCommandBase cmd)
        {
            Display("{0} {1}", cmd.CommandName, cmd.CommandDesc);
            Display("\t -> Example: {0}", cmd.CommandExample);
        }


        public void Display(Exception ex)
        {
            DisplayHeader("Error");
            Display(ex.Message);
            Display(ex.StackTrace);
            if (ex.InnerException != null) Display(ex.InnerException);
        }

        private void DisplayHeader(string title)
        {
            Display(title);
            Display("=========================================");
        }

        public void Display(string aLine)
        {
            System.Console.WriteLine(aLine);
        }

        public void DisplayLable(string Label, string Value)
        {
            Display("{0,40}: {1}", Label, Value);
        }

        public void Display(string StringFormat, params object[] args)
        {
            Display(string.Format(StringFormat, args));
        }
    }
}
