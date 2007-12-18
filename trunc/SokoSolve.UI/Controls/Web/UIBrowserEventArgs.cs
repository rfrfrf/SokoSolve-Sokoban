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

        private HtmlView browser;
        private Uri command;
    }
}
