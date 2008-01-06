using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.UI.Section
{
    /// <summary>
    /// An abstract base class to allow the entire command implementation, 
    /// complete with:
    /// <list type="">
    ///    <item>stateful command instances</item>
    ///    <item>command state (enabled, visible)</item>
    ///    <item>name, description, icons, etc</item>
    ///    <item>Undo facility</item>
    ///    <item>Error handling</item>
    /// </list>
    /// Note that the command is linked to its controller to allow controller level interactions
    /// </summary>
    /// <typeparam name="T">Domain class</typeparam>
	public abstract class Command<T>
	{
		private Controller<T> controller;
		private bool enabled;
		private bool visible;
		private string displayName;
		private string description;
		
        /// <summary>
        /// Abstract class contructor
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="displayName"></param>
		protected Command(Controller<T> controller, string displayName)
		{
			enabled = true;
			visible = true;
			this.controller = controller;
			this.displayName = displayName;
		}

        /// <summary>
        /// An internal implementation for the command logic. This is wrapped with error and status protection by <see cref="Execute"/>
        /// </summary>
        /// <param name="instance">context</param>
        protected virtual void ExecuteImplementation(CommandInstance<T> instance)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provide an optional implementation of an Undo facility. <see cref="Execute"/>
        /// </summary>
        /// <param name="instance">context</param>
        /// <returns>false means not support</returns>
        protected virtual bool UndoImplementation(CommandInstance<T> instance)
        {
            return false;
        }

        /// <summary>
        /// Create a command instance
        /// </summary>
        /// <param name="selection"></param>
        /// <returns></returns>
		public virtual CommandInstance<T> CreateInstance(List<T> selection)
		{
			return new CommandInstance<T>(this, selection);
		}

        /// <summary>
        /// Allow the command states (enabled, visible, etc) to be updated depending on a selection criteria
        /// </summary>
        /// <param name="selection"></param>
		public virtual void UpdateForSelection(List<T> selection)
		{
            // BY default, if there is no selection disable
            Enabled = (selection != null && selection.Count > 0);
		}

        /// <summary>
        /// Execute a command, based on its current context/instance.
        /// This method provides status and exception management.
        /// <see cref="ExecuteImplementation"/>
        /// </summary>
        /// <param name="instance">cannot be null</param>
		public void Execute(CommandInstance<T> instance)
		{
            if (instance == null) throw new ArgumentNullException("instance");

            if (instance.Status == ExecutionStatus.Waiting)
            {
                instance.Status = ExecutionStatus.Working;
                controller.SetStatus(instance.Status, string.Format("Working on {0}.", DisplayName));   
            }
            
			try
			{
			    ExecuteImplementation(instance);
                
                controller.UpdateUI("Command Execute");

                // Update status if not already manually changed.
                if (instance.Status == ExecutionStatus.Working)
                {
                    instance.Status = ExecutionStatus.Complete;
                    controller.SetStatus(instance.Status, string.Format("Completed {0}.", DisplayName));
                }
			}
            catch(NotImplementedException notImpl)
            {
                instance.Status = ExecutionStatus.Incomplete;
                controller.SetStatus(instance.Status, string.Format("This command has not been fully implemented - {0}, {1}.", DisplayName, notImpl.Message));
            }
			catch (Exception ex)
			{
                instance.Status = ExecutionStatus.Error;
                controller.SetStatus(instance.Status, string.Format("Error during {0}.", DisplayName));
				if (controller.HandelException(ex) == false) throw;
			}
           
		}

        /// <summary>
        /// Allow the command to be undone.
        /// </summary>
        /// <param name="instance"></param>
		public void Undo(CommandInstance<T> instance)
		{
            if (instance.Status != ExecutionStatus.Complete && instance.Status != ExecutionStatus.Complete)
            {
                // Nothing to do
                return;    
            }

			try
			{
				UndoImplementation(instance);
			    instance.Status = ExecutionStatus.Waiting;
			}
			catch (Exception ex)
			{
                instance.Status = ExecutionStatus.Error;
                controller.SetStatus(instance.Status, string.Format("Error during {0}.", DisplayName));

				if (controller.HandelException(ex) == false) throw;
			}
		}

		public Controller<T> Controller
		{
			get { return controller; }
		}

		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		public bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}

		public string DisplayName
		{
			get { return displayName; }
			set { displayName = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public string CommandPath
		{
			get { return this.GetType().Name; }
		}

		
	}
}

