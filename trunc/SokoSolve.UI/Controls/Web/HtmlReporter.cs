using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.UI;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Controls.Web;

namespace SokoSolve.UI.Controls.Web
{
	public static class HtmlReporter
	{

		public static HtmlBuilder Report(GenericDescription desc)
		{
            if (desc == null) return null;

		    HtmlBuilder sb = new HtmlBuilder();

		    if (desc.Name != null) sb.AddSection(desc.Name);

		    sb.AddLine(desc.Description);
            if (desc.DateSpecified) sb.AddLabel("Created", desc.Date.ToString());
            if (desc.License != null) sb.AddLabel("License", desc.License);
			if (desc.Author != null)
			{
                if (desc.Author.Name != null) sb.AddLabel("Author", desc.Author.Name);
                if (desc.Author.Email != null) sb.AddLabel("Email", desc.Author.Email);
                if (desc.Author.Homepage != null) sb.AddLabel("Web", string.Format("<a href=\"{0}\">{0}</a>", desc.Author.Homepage));
			}
			sb.AddLine(desc.Comments);
            if (desc.Name != null)  sb.EndSection();

		    return sb;
		}

        public static HtmlBuilder Report(HtmlBuilder Builder, GenericDescription desc)
        {
            if (desc == null) return Builder;

            HtmlBuilder sb = Builder;

            if (desc.Name != null) sb.AddSection(desc.Name);

            sb.AddLine(desc.Description);
            if (desc.DateSpecified) sb.AddLabel("Created", desc.Date.ToString());
            if (desc.License != null) sb.AddLabel("License", desc.License);
            if (desc.Author != null)
            {
                if (desc.Author.Name != null) sb.AddLabel("Author", desc.Author.Name);
                if (desc.Author.Email != null) sb.AddLabel("Email", desc.Author.Email);
                if (desc.Author.Homepage != null) sb.AddLabel("Web", string.Format("<a href=\"{0}\">{0}</a>", desc.Author.Homepage));
            }
            sb.AddLine(desc.Comments);
            if (desc.Name != null) sb.EndSection();

            return sb;
        }

		public static HtmlBuilder Report(Puzzle puzzle, StaticImage drawing)
		{
		    HtmlBuilder sb = new HtmlBuilder();

		    sb.AddSection(puzzle.Details.Name);
		    sb.Add("<table class=\"tableformat\"><tr><td>");
		    
            GenericDescription desc = new GenericDescription(puzzle.Details);
		    desc.Name = null; // so that the H1 tag is not generated here
            sb.Add(Report(desc));
            if (puzzle.Category != null) sb.AddLabel("Category", puzzle.Category.Details.Name);
            sb.AddLabel("Order", puzzle.Order.ToString());
            sb.AddLabel("Rating", puzzle.Rating);
            sb.AddLabel("PuzzleID", puzzle.PuzzleID);
            sb.AddLabel("Size", "{0}x{1}, {2} crates, {3} floor space", 
                puzzle.MasterMap.Map.Size.X, 
                puzzle.MasterMap.Map.Size.Y,
                puzzle.MasterMap.Map.Count(Cell.Crate),
                puzzle.MasterMap.Map.Count(Cell.Floor)
                );

            if (puzzle.HasAlternatives)
            {
                sb.AddLabel("Alternatives", puzzle.Alternatives.Count.ToString());
            }
            
            sb.Add("</td><td>");
            
            if (drawing != null)
            {
                sb.Add(puzzle, drawing.Draw(puzzle.MasterMap.Map), null);    
            }
            
            sb.Add("</td></tr></table>");
		  
		    
			return sb;
		}

        public static HtmlBuilder Report(PuzzleMap item)
        {
            HtmlBuilder sb = new HtmlBuilder();
            if (item != item.Puzzle.MasterMap)
            {
                // Not the master map
                sb.Add(Report(item.Details));
            }

            if (item.HasSolution)
            {
                sb.AddLabel("Solutions", item.Solutions.Count.ToString());
            }

           
            return sb;
        }

	    public static HtmlBuilder Report(Library library)
	    {
            HtmlBuilder report = Report(library.Details);
	        report.AddLabel("Rating", library.Rating);
            report.AddLabel("Puzzles", library.Puzzles.Count.ToString());

	        foreach (Category category in library.Categories.Top.ToList())
	        {
	            report.Add(Report(category, library));
	        }
	        return report;
	    }

        public static HtmlBuilder Report(Category category, Library helpLib)
	    {
            HtmlBuilder report = Report(category.Details);

            List<Puzzle> puz = category.GetPuzzles(helpLib);
            report.AddLabel("Puzzles", puz.Count.ToString());
            StaticImage img = new StaticImage(ResourceFactory.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16));

            int puzPerLine = 4;
            int counter = 1;
            report.AddLine("<table class=\"tableformat\"><tr><td>");
            foreach (Puzzle puzzle in puz)
            {
                report.Add(puzzle.Details.Name);
                report.Add("<br/>");
                report.Add("<a href=\"app://puzzle/{0}\">", puzzle.PuzzleID);
                report.Add(puzzle, img.Draw(puzzle.MasterMap.Map), "width:100px; height:80px");
                report.Add("</a>");

                if (counter % puzPerLine == 0)
                {
                    // Row break
                    report.AddLine("</td></tr><tr><td>");
                }
                else
                {
                    // Next cell
                    report.AddLine("</td><td>");
                }

                counter++;
            }
            report.AddLine("</td></tr></table>");
            return report;
	    }
	}


	
	
	




}
