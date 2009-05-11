using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SokoSolve.Console
{
    public enum ReturnCodes
    {
        OK = 0,
        GeneralError = -1,
        ArgInvalid = -2,
        ArgMissing,
        NotImplemented
    }

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

        /// <summary>
        /// Default Construction, get the name, description, author and copyright from the Assembly
        /// </summary>
        public ConsoleCommandController() : this(RetrieveName(), RetrieveDescription())
        {
          
        }

        private static string RetrieveDescription()
        {
            var asm = Assembly.GetExecutingAssembly();
            var title = asm.GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
            var copyright = asm.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);

            return string.Format("{0}, {1}, v{2}.", ((AssemblyTitleAttribute)title[0]).Title, ((AssemblyCopyrightAttribute)copyright[0]).Copyright, asm.GetName().Version.ToString());
        }

        private static string RetrieveName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        /// <summary>
        /// Find an command line argument (case-insensitive)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
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
        /// Find an command line argument (case-insensitive)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string FindArgExpected(string Name)
        {
            string res = FindArg(Name);
            if (res == null) throw new ArgumentNullException(Name, "Argument is missing");
            return res;
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

        /// <summary>
        /// Enroll/Register a new command with the controller
        /// </summary>
        /// <param name="cmd"></param>
        public void Enroll(ConsoleCommandBase cmd)
        {
            commands.Add(cmd);
            cmd.Enroll(this);
        }

        /// <summary>
        /// Attempt to *automatically* enroll all classes which derive from <see cref="ConsoleCommandBase"/>
        /// </summary>
        /// <param name="NameSpace"></param>
        public int Enroll(string NameSpace)
        {
            int cc = 0;
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.FullName.StartsWith("mscorlib")) continue;
                if (asm.FullName.StartsWith("System")) continue;
                if (asm.FullName.StartsWith("Microsoft")) continue;

                foreach (Type type in asm.GetTypes())
                {
                    if (type.Namespace == null) continue;
                    if (type.Namespace.StartsWith(NameSpace))
                    {
                        if (type.IsSubclassOf(typeof(ConsoleCommandBase)))
                        {
                            Enroll(Activator.CreateInstance(type) as ConsoleCommandBase);
                            cc++;
                        }
                    }
                }
            }
            return cc;
        }

        public ReturnCodes Execute(string[] ExecuteArgs)
        {
            ReturnCodes result = ExecuteInternal(ExecuteArgs);
            Display("Exiting with ReturnCode: {0}.", result);
            return result;
        }

        /// <summary>
        /// Execute the command with the appropriate wrapping
        /// </summary>
        /// <param name="ExecuteArgs"></param>
        /// <returns></returns>
        protected ReturnCodes ExecuteInternal(string[] ExecuteArgs)
        {
            args = ExecuteArgs;
            try
            {
                DisplayHeader(applicationName + " | " + applicationDescription, 1);
                Display(string.Empty);

                if (args.Length == 0)
                {
                    var defCom = commands.Find(x => x.IsDefaultCommand);
                    if (defCom != null)
                    {
                        Display("Attempting {0}...", defCom.CommandName.ToUpper());
                        return defCom.Execute(this);
                    }
                    else
                    {
                        DisplayHelp();
                        return ReturnCodes.ArgInvalid;
                    }
                }

                // Using the first argument check to see if there is a command we can execute
                foreach (ConsoleCommandBase command in commands)
                {
                    if (command.CanProcess(args[0]))
                    {
                        if (command.MinParams > args.Length -1)
                        {
                            Display("This command {0} requires {1} params, only {2} was given.", command.CommandName, args.Length, command.MinParams);
                            return ReturnCodes.ArgMissing;
                        }

                        try
                        {
                            Display("Attempting {0}...", command.CommandName.ToUpper());
                            return command.Execute(this);
                        }
                        catch(Exception ex)
                        {
                            Display(ex);
                            return ReturnCodes.GeneralError;
                        }
                    }
                }

                // No Command Found
                Display("No commands Found");
                Display(string.Empty);
                DisplayHelp();
                Display(string.Empty);
                return ReturnCodes.ArgInvalid;
            }
            catch(Exception ex)
            {
                Display(ex);
                return ReturnCodes.GeneralError;
            }
        }

        public void DisplayHelp()
        {
            DisplayHeader("Command Help", 3);
            Display(string.Empty);
            Display("All commands are in for format \"APPLICTION.EXE COMMAND param1 param2 -arg:123\"");
            Display(string.Empty);
            Display("The available commands are:");
            foreach (ConsoleCommandBase command in commands)
            {
                DisplayHelp(command);
            }
        }


        public void DisplayHelp(ConsoleCommandBase cmd)
        {
            Display("(o) {0}\t\"{1}\"", cmd.CommandName, cmd.CommandDesc);
            Display("   \t\t -> Example: {0}", cmd.CommandExample);
            Display(string.Empty);
        }


        public void Display(Exception ex)
        {
            DisplayHeader("Error: "+ ex.Message, 2);
            Display(ex.StackTrace);
            if (ex.InnerException != null) Display(ex.InnerException);
        }

        private void DisplayHeader(string title, int level)
        {
            if (level <= 1)
            {
                Display("==================================================================================");
                Display(" " + title);
                Display("==================================================================================");
                return;
            }

            if (level == 2)
            {
                Display("==========================================");
                Display(" " + title);
                Display("------------------------------------------");
                return;
            }

            Display(string.Format("##### {0} #####", title));
            
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
