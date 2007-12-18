using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Section
{
    /// <summary>
    /// Allow execution 'working' status for controller, commands and other mini transaction-like code.
    /// </summary>
	public enum ExecutionStatus
	{
		None,
		Waiting,
		Working,
		Complete,
        AwaitingCallback,
        Incomplete,
		Error
	}

    /// <summary>
    /// The controller for a specific domain class has a list of possible commands and the current selection (context)
    /// </summary>
    /// <typeparam name="T">Domain Object</typeparam>
	public abstract class Controller<T>
	{
		/// <summary>
		/// Abstract constructor
		/// </summary>
		protected Controller()
		{
		    logger = new ContextLogger();
			commands = new List<Command<T>>();
		   
			statusText = "Startup...";
			status = ExecutionStatus.Waiting;
		}

        /// <summary>
        /// Provide icons for the commands
        /// </summary>
        public IconBinder IconBinder
        {
            get { return iconBinder; }
        }

        /// <summary>
        /// List of all commands (not editable)
        /// </summary>
		public IEnumerable<Command<T>> Commands
		{
			get { return commands; }
		}

        /// <summary>
        /// Current selection
        /// </summary>
        public List<T> Selection
        {
            get { return selection; }
        }

        /// <summary>
        /// Current Status Text
        /// </summary>
		public string StatusText
		{
			get { return statusText; }
		}

		/// <summary>
		/// Current Status
		/// </summary>
        public ExecutionStatus Status
		{
			get { return status; }
		}

        /// <summary>
        /// Allows intellegent internal context aware logging
        /// </summary>
	    public ContextLogger Logger
	    {
	        get { return logger; }
	    }

        /// <summary>
        /// Set the status
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="text"></param>
	    public virtual void SetStatus(ExecutionStatus newStatus, string text)
		{
			this.statusText = text;
			this.status = newStatus;
	        Logger.Add(this, "Status: {0} - {1}.", newStatus, text);
		}

        /// <summary>
        /// Overloaded
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="stringFormat"></param>
        /// <param name="parm"></param>
		public void SetStatus(ExecutionStatus newStatus, string stringFormat, params  object[] parm)
		{
		    SetStatus(newStatus, string.Format(stringFormat, parm));
		}

        /// <summary>
        /// Update the UI, informs the commands and allows them to bind to the UI.
        /// </summary>
        /// <param name="context"></param>
		public abstract void UpdateUI(string context);


        /// <summary>
        /// Overload for <see cref="UpdateSelection"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateSelectionSingle(T item)
        {
            List<T> helper = new List<T>(1);
            helper.Add(item);
            return UpdateSelection(helper);
        }

        /// <summary>
        /// Change the current selection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
		public virtual bool UpdateSelection(List<T> newSelection)
		{
			List<T> oldSelection = this.selection;

			bool updated = false;
			if (oldSelection == null || oldSelection.Count == 0)
			{
				if (newSelection != null && newSelection.Count > 0) updated = true;
			}

			if (oldSelection != null && newSelection != null)
			{
				updated = !(oldSelection.Equals(newSelection));
			}

			this.selection = newSelection;

		    Logger.Add(this, "UpdateSelection {0} - {1}", updated, newSelection);

			return updated;
		}

		/// <summary>
		/// Handle an error
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <returns>false will rethrow</returns>
		public bool HandelException(Exception ex)
		{
			return false;
		}

        /// <summary>
        /// Perform a command via a URI
        /// </summary>
        /// <param name="appURI"></param>
        /// <returns></returns>
        public virtual bool PerformCommandURI(Uri appURI)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Register a new command for this controller
        /// </summary>
        /// <param name="newCommand"></param>
        protected void Register(Command<T> newCommand)
        {
            commands.Add(newCommand);
        }

        protected List<Command<T>> commands;
        private string statusText;
        private ExecutionStatus status;
        private List<T> selection;
        private ContextLogger logger;
        private IconBinder iconBinder = new IconBinder(new StaticImage(ResourceFactory.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16)));

	}

	

	

}
