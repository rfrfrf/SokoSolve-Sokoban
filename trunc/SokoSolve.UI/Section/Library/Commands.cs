using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SokoSolve.Common;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.UI.Controls.Secondary;

namespace SokoSolve.UI.Section.Library
{


    //#################################################################
    //#################################################################
    //#################################################################


    class HelpAbout: CommandLibraryBase
    {
        public HelpAbout(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("About", "About SokoSolve");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("$html/about.html");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpHowToPlay : CommandLibraryBase
    {
        public HelpHowToPlay(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("How To Play", "How to play sokoban");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("$html/HowToPlay.html");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpLicense : CommandLibraryBase
    {
        public HelpLicense(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Software License", "Software License for SokoSolve");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("$html/License.html");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpRelease : CommandLibraryBase
    {
        public HelpRelease(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Release Notes", "Release Notes for SokoSolve");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("$html/ReleaseNotes.html");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpWebSite : CommandLibraryBase
    {
        public HelpWebSite(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Project Web Site", "Got SokoSolve web site");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("http://sokosolve.sourceforge.net/?Version="+ProgramVersion.VersionString);
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpCheckVersion: CommandLibraryBase
    {
        public HelpCheckVersion(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Check Version", "Check the current version");
        }

        /// <summary>
        /// Perform a version check
        /// </summary>
        public static void PerformVersionCheck(bool MessageIfOk)
        {
            try
            {
                WebRequest req = WebRequest.Create("http://sokosolve.sourceforge.net/VersionCurrent.xml");
                req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);
                XmlDocument reqXML = new XmlDocument();
                reqXML.Load(req.GetResponse().GetResponseStream());

                string xmlLatest = reqXML.DocumentElement.GetAttribute("Latest");
                if (xmlLatest != ProgramVersion.VersionString)
                {
                    string message = string.Format("Current internet version is {0}, while you are currently running {1}. Do you want to download the latest version?", xmlLatest, ProgramVersion.VersionString);
                    if (MessageBox.Show(message, "Version Check", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Process.Start("http://sokosolve.sourceforge.net/install.html?UpdateFromVersion="+ProgramVersion.VersionString);
                    }
                }
                else if (MessageIfOk)
                {
                    MessageBox.Show("This is the latest version of SokoSolve");
                }

                // Remember when last version
                ProfileController.Current.LastVersionCheck = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve SokoSolve from the Internet\n" + ex.Message, "Version Check", MessageBoxButtons.OK);
            }
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            PerformVersionCheck(true);
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpReturn : CommandLibraryBase
    {
        public HelpReturn(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Return", "Return to welcome home page");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.Mode = FormMain.Modes.Welcome;
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }


    //#################################################################
    //#################################################################
    //#################################################################


    class HelpSupportBug : CommandLibraryBase
    {
        public HelpSupportBug(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Submit bug report", "Submit an error or bug report");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("http://sourceforge.net/tracker/?group_id=85742&atid=577153");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpSupportGeneralDiscussion  : CommandLibraryBase
    {
        public HelpSupportGeneralDiscussion(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("General Discussion", "Open forum discussion");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("http://sourceforge.net/forum/forum.php?forum_id=293483");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpSupportFeature : CommandLibraryBase
    {
        public HelpSupportFeature(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Feature Request", "Request a new feature for SokoSolve.");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("http://sourceforge.net/tracker/?group_id=85742&atid=577156");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }

    //#################################################################
    //#################################################################
    //#################################################################


    class HelpSupportPuzzle : CommandLibraryBase
    {
        public HelpSupportPuzzle(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Submit a Puzzle Library", "Submit a new puzzle library for SokoSolve.");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("http://sourceforge.net/forum/forum.php?forum_id=765121");
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = true;
        }

    }


    
}
