using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.UI.Controls.Web
{
    public class UIBrowserEvent : EventArgs
    {
        public UIBrowserEvent(HtmlView browser, Uri command)
        {
            this.browser = browser;
            this.command = command;
        }

        public HtmlView Browser
        {
            get { return browser; }
        }

        public Uri Command
        {
            get { return command; }
        }


        /// <summary>
        /// Has this commond be implemented/handled.
        /// false means not handled
        /// </summary>
        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }


        private HtmlView browser;
        private Uri command;
        private bool completed;
    }
}
