using System;
using System.Collections.Generic;
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
                main.ShowInBrowser("http://sokosolve.sourceforge.net/");
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

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            FormMain main = Controller.Explorer.TreeView.FindForm() as FormMain;
            if (main != null)
            {
                main.ShowInBrowser("http://sokosolve.sourceforge.net/VersionCurrent.xml");
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

    
}
