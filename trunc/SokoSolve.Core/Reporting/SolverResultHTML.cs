using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SokoSolve.Common;
using SokoSolve.Core.Analysis.Solver;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.Reporting
{
    public class SolverResultHTML: ReportXHTML
    {
        public SolverResultHTML(List<SolverResult> results, string inlineCSS) : base("SokoSolve | Automated Solver Report")
        {
            if (inlineCSS != null) SetCSSInline(inlineCSS);
                else SetCSSLink("style.css");

            this.results = results;
        }

        public void BuildReport()
        {
            Body.AppendChild(CreateContentTag("h1", "SokoSolve | Automated Solver Report"));

            Body.AppendChild(CreateContentTag("p", 
                string.Format("SokoSolve Automated Solver Report, created {0:R} by SokoSolve v{1} on a {2}", 
                    DateTime.Now, ProgramVersion.VersionString, DebugHelper.GetCPUDescription())));


            XmlElement table = report.CreateElement("table");
            table.SetAttribute("class", "tableinfo");
            table.InnerXml += string.Format("<tr><th>Puzzle</th><th>Status</th><th>Summary</th></tr>");
            foreach (SolverResult result in results)
            {
                if (result == null) continue;
                if (result.Map ==null) continue;
                if (result.Map.Puzzle == null) continue;
                if (result.Map.Puzzle.Details == null) continue;
                table.InnerXml += string.Format("<tr><td>({3}) {0}</td><td>{1}</td><td>{2}</td></tr>", 
                    result.Map.Puzzle.Details.Name, 
                    result.StatusString, 
                    result.Summary,
                    result.Map.Puzzle.PuzzleID);
            }

            Body.AppendChild(table);

             foreach (SolverResult result in results)
             {
                 if (result == null) continue;
                 if (result.Map == null) continue;
                 if (result.Map.Puzzle == null) continue;
                 if (result.Map.Puzzle.Details == null) continue;

                 Body.AppendChild(CreateContentTag("h2", string.Format("({0}) {1}", result.Map.Puzzle.PuzzleID, result.Map.Puzzle.Details.Name)));

                 XmlElement tabledtl = report.CreateElement("table");
                 tabledtl.SetAttribute("class", "tableinfo");


                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Summary", result.Summary);
                 if (result.Exception != null)
                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Error", StringHelper.Report(result.Exception));

                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Status", result.StatusString);
                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Controller Status", result.ControllerResult);
                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Total Seconds", result.Info.TotalSecond);
                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Total Nodes", result.Info.TotalNodes);
                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Machine", result.Info.Machine);
                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Puzzle Rating", result.Info.RatingScore);

                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td><pre>{1}</pre></td></tr>", "Puzzle", result.Map.Map.ToString());

                 StringBuilder sb = new StringBuilder();
                 foreach (var s in result.Info.InfoValues)
                 {
                     sb.AppendFormat("{0}: {1}<br/>", s.Name, s.Value);
                 }
                 tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", "Misc Details", sb);

                 if (result.HasSolution)
                 {
                     foreach (Solution solution in result.Solutions)
                     {
                         tabledtl.InnerXml += string.Format("<tr><th>{0}</th><td><pre>{1}</pre></td></tr>", "Solution", solution.ToStringDisplay("<br/>"));
                     }
                 }
             

             
             

                 Body.AppendChild(tabledtl);
             }

             AddFooter();
        }

        List<SolverResult> results;
    }
}
