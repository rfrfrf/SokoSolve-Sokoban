using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.UI;
using SokoSolve.UI.Controls.Web;

namespace SokoSolve.UI.Controls.Primary
{
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            InitializeComponent();

            InitHTML();
        }

        private Puzzle Current;

        FormMain Form
        {
            get
            {
                FormMain main = FindForm() as FormMain;
                if (main == null) throw new NullReferenceException("FormMain not found. This can only be used after the control is added to the form.");
                return main;
            }
        }

        private void InitHTML()
        {
            StringBuilder html = new StringBuilder(File.ReadAllText(FileManager.getContent("$html/Welcome.html")));

            XmlProvider prov = new XmlProvider();
            Core.Model.Library lib = prov.Load(ProfileController.Current.LibraryLastFile);

            

            StaticImage drawing = new StaticImage(ResourceFactory.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16));
            HtmlBuilder leftpane = new HtmlBuilder();
            HtmlBuilder rightpane = new HtmlBuilder();
            Current = lib.GetPuzzleByID(ProfileController.Current.LibraryLastPuzzle);
            if (Current == null)
            {
                ProfileController.Current.LibraryLastPuzzle = lib.Puzzles[0].PuzzleID;
                Current = lib.Puzzles[0];
            }

            if (Current != null)
            {
                leftpane.AddSection(null);
                HtmlReporter.Report(leftpane, Current.Library.Details);
                leftpane.AddSection(null);
                HtmlReporter.Report(leftpane, Current.Details);
                leftpane.AddSection("Stats");
                leftpane.AddLabel("Library Complete", "5%");
                leftpane.AddLabel("Time Elapse", "2h34m");
                leftpane.AddLabel("Attempts", "13");
                leftpane.AddLabel("Solutions", "6");

                rightpane.Add("<a href=\"app://Puzzle/{0}\">", Current.PuzzleID);
                rightpane.Add(Current, drawing.Draw(Current.MasterMap.Map), null);
                rightpane.Add("</a>");
                rightpane.AddLine("<br/><i>Click to play...</i>");
                
            }
            else
            {
                // Library has no puzzles    
            }

            html = html.Replace("[Details]", leftpane.GetHTMLBody());
            html = html.Replace("[Image]", rightpane.GetHTMLBody());
            html = html.Replace("[BASEHREF]", FileManager.getContent("$html"));
            htmlView.SetHTML(html.ToString());
        }

        private void htmlView_OnCommand(object sender, SokoSolve.UI.Controls.Web.UIBrowserEvent e)
        {
            if (e.Command == new Uri("app://Library"))
            {
                Form.InitLibrary(this.Current.Library);
                Form.Mode = FormMain.Modes.Library;
                return;      
            }

            if (e.Command == new Uri("app://Controller/Home"))
            {
                InitHTML();
                return;
            }

            if (e.Command == new Uri("app://Controller/Back"))
            {
                InitHTML();
                return;
            }

            if (e.Command == new Uri("app://HTML/About.html"))
            {
                htmlView.Navigate(FileManager.getContent("$html\\About.html"));
                return;
            }

            if (e.Command == new Uri("app://HTML/ReleaseNotes.html"))
            {
                htmlView.Navigate(FileManager.getContent("$html\\ReleaseNotes.html"));
                return;
            }

            if (e.Command == new Uri("app://HTML/HowToPlay.html"))
            {
                htmlView.Navigate(FileManager.getContent("$html\\HowToPlay.html"));
                return;
            }

            if (e.Command.ToString().StartsWith("app://puzzle/"))
            {
                Form.StartGame(Current, Current.MasterMap, FormMain.Modes.Welcome);
                return;
            }
        }
    }
}
