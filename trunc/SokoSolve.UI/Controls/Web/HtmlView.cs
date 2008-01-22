using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SokoSolve.UI.Controls.Secondary;
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

        public bool ShowCommandBack
        {
            get { return tsbBack.Visible;  }
            set { tsbBack.Visible = value;  }
        }

        public bool ShowCommandForward
        {
            get { return tsbForward.Visible; }
            set { tsbForward.Visible = value; }
        }

        public bool ShowCommandHome
        {
            get { return tsbHome.Visible; }
            set { tsbHome.Visible = value; }
        }


        public bool ShowCommandDone
        {
            get { return tsbDone.Visible; }
            set { tsbDone.Visible = value; }
        }

        public bool ShowCommandPrint
        {
            get { return tsbPrint.Visible; }
            set { tsbPrint.Visible = value; }
        }

        public bool ShowCommandSave
        {
            get { return tsbSave.Visible; }
            set { tsbSave.Visible = value; }
        }


        private void tsbBack_Click(object sender, EventArgs e)
        {
            if (OnCommand != null)
            {
                UIBrowserEvent args = new UIBrowserEvent(this, new Uri("app://Controller/Back"));
                OnCommand(sender, args);
                if (!args.Completed)
                {
                    webBrowser.GoBack();
                }
            }
            else
            {
                webBrowser.GoBack();
            }
        }

        private void tsbForward_Click(object sender, EventArgs e)
        {
            if (OnCommand != null)
            {
                UIBrowserEvent args = new UIBrowserEvent(this, new Uri("app://Controller/Forward"));
                OnCommand(sender, args);
                if (!args.Completed)
                {
                    webBrowser.GoForward();
                }
            }
            else
            {
                webBrowser.GoForward();
            }
        }

        private void tsbHome_Click(object sender, EventArgs e)
        {
            if (OnCommand != null)
            {
                UIBrowserEvent args = new UIBrowserEvent(this, new Uri("app://Controller/Home"));
                OnCommand(sender, args);
                if (!args.Completed)
                {
                    webBrowser.GoHome();
                }
            }
            else
            {
                webBrowser.GoHome();
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            webBrowser.Print();
        }

        private void tsbDone_Click(object sender, EventArgs e)
        {
            if (OnCommand != null) OnCommand(sender, new UIBrowserEvent(this, new Uri("app://Controller/Done")));
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
                FormError error = new FormError();
                error.Exception = ex;
                error.ShowDialog();
            }
            
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            tsLabelStatus.Text = e.Url.ToString();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, webBrowser.DocumentText);
            }
        }
    }
}
