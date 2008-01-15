using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;
using SokoSolve.Common;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.Core.UI;

namespace SokoSolve.Core.Reporting
{
    public class LibraryReport : ReportXHTML
    {
        private string directory = @"C:\junk\";
        private Library library;
        StaticImage staticImage;

        public LibraryReport(Library library, StaticImage drawing) : base(library.Details.Name)
        {
            this.library = library;
            staticImage = drawing;
        }

        public void BuildReport()
        {
            Body.AppendChild(CreateContentTag("h1", library.Details.Name));

            Body.AppendChild(CreateGenericDescriptionTable(library.Details, false));

            foreach (Category category in library.Categories.Nodes)
            {
                List<Puzzle> puzzles = category.GetPuzzles(library);
                Body.AppendChild(CreateContentTag("h2", category.Details.Name));

                foreach (Puzzle puzzle in puzzles)
                {
                    Body.AppendChild(CreatePuzzle(puzzle));
                }
            }

            AddFooter();
        }

       

        private XmlElement CreatePuzzle(Puzzle puzzle)
        {
            XmlElement xml = report.CreateElement("div");

            xml.AppendChild(CreateContentTag("h3", string.Format("#{0} - {1}", puzzle.Order, puzzle.Details.Name)));

            //=======================
            XmlElement table = report.CreateElement("table");
            table.SetAttribute("class", "tableinfo");

            if (puzzle.Details.DateSpecified)
                table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Created", puzzle.Details.Date);

            if (!string.IsNullOrEmpty(puzzle.Details.Description))
            table.InnerXml +=
                string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Description", puzzle.Details.Description);

            if (puzzle.Details.Author != null)
            {
                if (!string.IsNullOrEmpty(puzzle.Details.Author.Name))
                table.InnerXml +=
                    string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Author", puzzle.Details.Author.Name);
            if (!string.IsNullOrEmpty(puzzle.Details.Author.Email))
                table.InnerXml +=
                    string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Email", puzzle.Details.Author.Email);
            if (!string.IsNullOrEmpty(puzzle.Details.Author.Homepage))
                table.InnerXml +=
                    string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Web", puzzle.Details.Author.Homepage);
            }
            if (!string.IsNullOrEmpty(puzzle.Details.License))
            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "License", puzzle.Details.License);
        if (!string.IsNullOrEmpty(puzzle.Details.Comments))
            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Comments", puzzle.Details.Comments);

            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Rating", puzzle.Rating);

            table.InnerXml += string.Format("<tr><td colspan=\"2\"><pre>{0}</pre></td></tr>", puzzle.MasterMap.Map.ToString());
           
            //=======================

            Image pic = staticImage.Draw(puzzle.MasterMap.Map);
            string picFileName = string.Format("{0}-{1}.png", library.LibraryID, puzzle.PuzzleID);
            string picFullFileName = directory + picFileName;
            pic.Save(picFullFileName);

            XmlElement img = report.CreateElement("img");
            img.SetAttribute("src", picFileName);
            img.SetAttribute("alt", "Puzzle" + puzzle.Details.Name);
            

            XmlElement tableformat = report.CreateElement("table");
            tableformat.SetAttribute("class", "tableformat");
            tableformat.InnerXml = "<tr><td></td><td></td></tr>";
            tableformat.ChildNodes[0].ChildNodes[0].AppendChild(table);
            tableformat.ChildNodes[0].ChildNodes[1].AppendChild(img);
            xml.AppendChild(tableformat);

            if (puzzle.MasterMap.HasSolution)
            {
                foreach (Solution solution in puzzle.MasterMap.Solutions)
                {
                    xml.AppendChild(CreateSolution(solution));
                }
            }

            if (puzzle.HasAlternatives)
            {
                xml.AppendChild(CreateContentTag("h4", "Alternatives"));
            }

            return xml;
        }


        private XmlElement CreateSolution(Solution solution)
        {
            XmlElement xml = report.CreateElement("p");
            xml.AppendChild(xml.AppendChild(CreateContentTag("h4", string.Format("{0} steps -- {1}", solution.Steps.Length, solution.Details.Name))));
            if (solution.Details != null)
            {
                xml.AppendChild(CreateGenericDescriptionTable(solution.Details, false));
            }
            XmlElement para = CreateContentTag("code", "");
            para.InnerXml = solution.ToStringDisplay("<br/>");
            xml.AppendChild(para);

            return xml;
        }

        public XmlElement CreateGenericDescriptionTable(GenericDescription desc, bool displayname)
        {
            XmlElement table = report.CreateElement("table");
            table.SetAttribute("class", "tableinfo");

            if (displayname && !string.IsNullOrEmpty(desc.Name))
            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Name", desc.Name);

            if (desc.DateSpecified)
            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Created", desc.Date);

            if (!string.IsNullOrEmpty(desc.Description))
            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Description", desc.Description);
            if (desc.Author != null)
            {
                if (!string.IsNullOrEmpty(desc.Author.Name))
                table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Author", desc.Author.Name);
            if (!string.IsNullOrEmpty(desc.Author.Email ))
                table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Email", desc.Author.Email);
            if (!string.IsNullOrEmpty(desc.Author.Homepage))
                table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Web", desc.Author.Homepage);
            }
            if (!string.IsNullOrEmpty(desc.License))
            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "License", desc.License);
        if (!string.IsNullOrEmpty(desc.Comments))
            table.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Comments", desc.Comments);
            return table;
        }
    }

    public class ReportXHTML
    {
        protected XmlDocument report;

        public ReportXHTML(string title)
        {
            report = new XmlDocument();
            report.LoadXml(
                string.Format(
                    @"<html xmlns=""http://www.w3.org/1999/xhtml"">
	<head>
		<title>{0}</title>
		<link type=""text/css"" rel=""Stylesheet"" href=""style.css"" />
	</head>
	<body>
	    
	</body>
</html>",
                    title));
        }

        protected XmlElement Body
        {
            get { return report.DocumentElement["body"]; }
        }

        protected void AddFooter()
        {
            XmlElement footer = report.CreateElement("div");
            footer.SetAttribute("class", "footer");

            string html = 
                string.Format(
@"Produced by <a href=""http://sokosolve.sourceforge.net"">SokoSolve Sokoban</a> version {0}<br/>
<a href=""http://sourceforge.net""> 
    <img src=""http://sourceforge.net/sflogo.php?group_id=85742&amp;type=5"" width=""210"" height=""62""  alt=""SourceForge.net Logo"" />
</a>", ProgramVersion.VersionString);

            footer.InnerXml = html;

            Body.AppendChild(footer);
        }

        protected XmlElement CreateContentTag(string tagName, string innerText)
        {
            XmlElement xml = report.CreateElement(tagName);
            xml.InnerText = innerText;
            return xml;
        }

        public virtual void Save(string filename)
        {
            report.Save(filename);
        }
    }
}