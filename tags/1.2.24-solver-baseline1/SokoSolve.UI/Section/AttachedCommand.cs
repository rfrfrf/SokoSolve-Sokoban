using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SokoSolve.UI.Section
{
    /// <summary>
    /// Allows a commmand to be attached (easily) to a number of UI controls.
    /// This class then can handle status (enabled, visible) and give consistent presentation (text, color, icons).
    /// </summary>
    /// <typeparam name="T">Domain Class</typeparam>
	public abstract class AttachedCommand<T> : Command<T>
	{
		private List<object> targetsUI;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="displayName"></param>
		public AttachedCommand(Controller<T> controller, string displayName) : base(controller, displayName)
		{
			targetsUI = new List<object>();
		}

        /// <summary>
        /// Overloaded.
        /// </summary>
        /// <param name="controller"></param>
		public AttachedCommand(Controller<T> controller) : this(controller, "")
		{
			
		}

        /// <summary>
        /// Setup presentation infoamtion
        /// </summary>
        /// <param name="displayName">Icon short name</param>
        /// <param name="displayDescription">Longer (single sentance) description</param>
		protected void Init(string displayName, string displayDescription)
		{
			DisplayName = displayName;
			Description = displayDescription;
		}

        /// <summary>
        /// Attach to a number of UI controls.
        /// <see cref="Attach"/>
        /// </summary>
        /// <param name="attachedControls"></param>
		protected void InitUI(object[] attachedControls)
		{
			foreach(object attachedControl in attachedControls)
			{
				Attach(attachedControl);
			}
		}

        /// <summary>
        /// Attach (register) a UI control (weakly via huristics) to the command
        /// </summary>
        /// <param name="targetUIcontrol">A ui control to attach to the command (buttons, menuitems, toolstrip items etc)</param>
		protected virtual void Attach(object targetUIcontrol)
		{
			bool attached = false;

			if (targetUIcontrol is Control)
			{
				Control control = targetUIcontrol as Control;
				control.Tag = this;
				attached = true;
			}

			ToolStripMenuItem menuItem = targetUIcontrol as ToolStripMenuItem;
			if (menuItem != null)
			{
				menuItem.Click += new EventHandler(TargetControl_Click);
				attached = true;
			}

			ToolStripButton button = targetUIcontrol as ToolStripButton;
			if (button != null)
			{
				button.Tag = this;
				button.Click += new EventHandler(TargetControl_Click);
				attached = true;
			}

			if (!attached)
			{
				throw new Exception("Could not attach to type : "+ targetUIcontrol.GetType().ToString());
			}

			targetsUI.Add(targetUIcontrol);

			UpdateUI("Command.Attach");
		}

        /// <summary>
        /// Bind the command to the target UI control.
        /// </summary>
        /// <param name="context">Freeform text context to allow more control. May be null.</param>
		public virtual void UpdateUI(string context)
		{
            if (targetsUI == null || targetsUI.Count == 0)
            {
                Controller.Logger.Add(this, "SKIPPED => UpdateUI - {0}", context);
            }
            else
            {
                bool updated = false;
                foreach (object target in targetsUI)
                {
                   
                    ToolStripMenuItem menuItem = target as ToolStripMenuItem;
                    if (menuItem != null)
                    {
                        if (menuItem.Visible != Visible) menuItem.Visible = Visible;
                        if (menuItem.Enabled != Enabled) menuItem.Enabled = Enabled;
                        if (menuItem.Text != DisplayName) menuItem.Text = DisplayName;
                        if (menuItem.ToolTipText != Description) menuItem.ToolTipText = Description;
                        updated = true;
                    }

                    ToolStripButton button = target as ToolStripButton;
                    if (button != null)
                    {
                        if (button.Visible != Visible) button.Visible = Visible;
                        if (button.Enabled != Enabled) button.Enabled = Enabled;
                        if (button.Text != DisplayName) button.Text = DisplayName;
                        if (button.ToolTipText != Description) button.ToolTipText = Description;
                        updated = true;
                    }

                    Control control = target as Control;
                    if (control != null)
                    {
                        if (control.Visible != Visible) control.Visible = Visible;
                        if (control.Enabled != Enabled) control.Enabled = Enabled;
                        if (control.Text != DisplayName) control.Text = DisplayName;
                        ToolTip tip = new ToolTip();
                        tip.ToolTipTitle = DisplayName;
                        tip.SetToolTip(control, Description);
                        updated = true;
                    }
                }

                if (updated)
                {
                    Controller.Logger.Add(this, "Success with UpdateUI - {0}", context);
                }
                else
                {
                    Controller.Logger.Add(this, "SKIPPED(unable to find a known target control) => UpdateUI - {0}", context);
                }
            }
			
		}

        /// <summary>
        /// Handle the click event. Fire the command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void TargetControl_Click(object sender, EventArgs e)
		{
			List<T> context = null;
			if (Controller.Selection != null)
			{
				context = new List<T>(Controller.Selection);
			}
			CommandInstance<T> inst = CreateInstance(context);
			Execute(inst);
		}

		
	}
}

