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

            StringBuilder html = new StringBuilder(File.ReadAllText(FileManager.getContent("$html/Welcome.html")));

            XmlProvider prov = new XmlProvider();
            Core.Model.Library lib = prov.Load(ProfileController.Current.LibraryLastFile);

            string detailsHTML = "";
            Puzzle lastPuz = lib.GetPuzzleByID(ProfileController.Current.LibraryLastPuzzle);

            StaticImage drawing = new StaticImage(ResourceFactory.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16));
            if (lastPuz != null)
            {
                detailsHTML = HtmlReporter.Report(lastPuz, drawing).GetHTMLBody();
            }
            else
            {
                if (lib.Puzzles.Count > 0)
                {
                    ProfileController.Current.LibraryLastPuzzle = lib.Puzzles[0].PuzzleID;
                    detailsHTML = HtmlReporter.Report(lib.Puzzles[0], drawing).GetHTMLBody();
                }
            }

            html = html.Replace("[Details]", detailsHTML);
            html = html.Replace("[BASEHREF]", FileManager.getContent("$html"));
            htmlView.SetHTML(html.ToString());
        }

        private void htmlView_OnCommand(object sender, SokoSolve.UI.Controls.Web.UIBrowserEvent e)
        {
            if (e.Command == new Uri("app://Library"))
            {
                FormMain main = FindForm() as FormMain;
                if (main != null)
                {
                    main.Mode = FormMain.Modes.Library;
                }    
            }
        }
    }
}
