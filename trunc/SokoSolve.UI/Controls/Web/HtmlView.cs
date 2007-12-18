using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.UI.Controls.Web;

namespace SokoSolve.UI.Controls.Web
{
    /// <summary>
    /// A wrapper of the .NET browser control.
    /// It has features for embedding in a winform application.
    /// </summary>
    public partial class HtmlView : UserControl
    {
        public HtmlView()
        {
            InitializeComponent();

            ShowStatus = false;
            ShowCommands = false;
        }

        public void SetHTML(string html)
        {
            webBrowser.DocumentText = html;
        }

        public void Navigate(string Url)
        {
            webBrowser.Navigate(Url);
        }

        public event EventHandler<UIBrowserEvent> OnCommand; 

        public bool ShowCommands
        {
            get
            {
                return toolStripCommands.Visible;
            }
            set
            {
                toolStripCommands.Visible = value;
            }
        }

        public bool ShowStatus
        {
            get
            {
                return statusStrip.Visible;
            }
            set
            {
                statusStrip.Visible = value;
            }
        }


        private void tsbBack_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }

        private void tsbForward_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();
        }

        private void tsbHome_Click(object sender, EventArgs e)
        {
            webBrowser.GoHome();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            webBrowser.Print();
        }

        private void tsbDone_Click(object sender, EventArgs e)
        {
            if (OnCommand != null) OnCommand(sender, new UIBrowserEvent(this, new Uri("app://done")));
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                if (e.Url.Scheme == "app")
                {
                    e.Cancel = true;
                    if (OnCommand != null)
                    {
                        OnCommand(this, new UIBrowserEvent(this, e.Url));
                    }
                }
            }
            catch(Exception ex)
            {
                e.Cancel = true;
            }
            
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            tsLabelStatus.Text = e.Url.ToString();
        }
    }
}
