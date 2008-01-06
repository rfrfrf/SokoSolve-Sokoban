using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.UI.Section
{
	public class CommandInstance<T> 
	{
		private Command<T> command;
		private List<T> context;
	    private ExecutionStatus status;
	    private object param;

		public CommandInstance(Command<T> command, List<T> context)
		{
			this.context = context;
			this.command = command;
		    this.status = ExecutionStatus.Waiting;
		}

		public List<T> Context
		{
			get { return context; }
			set { context = value; }
		}

		public Command<T> Command
		{
			get { return command; }
		}

	    public ExecutionStatus Status
	    {
	        get { return status; }
	        set { status = value; }
	    }

        /// <summary>
        /// Untyped parameter to allow simple extension and state enrichment (particilarly for call-backs)
        /// </summary>
	    public object Param
	    {
	        get { return param; }
	        set { param = value; }
	    }

	    /// <summary>
        /// Is there a selection contect fo this command (ie does this command apply to any items)
        /// </summary>
	    public bool hasSelection
	    {
	        get
	        {
	            return (context != null && context.Count > 0);
	        }
	    }
	}
}
