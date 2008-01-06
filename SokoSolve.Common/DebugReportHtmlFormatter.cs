using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common
{
    /// <summary>
    /// Format a calculation report into an HTML report
    /// <see cref="DebugReport"/>
    /// </summary>
	public class DebugReportHtmlFormatter : DebugReportFormatter
	{
        /// <summary>
        /// Header for the calculation report
        /// </summary>
        /// <param name="report">context</param>
        /// <returns>HTML</returns>
		public override string Header(DebugReport report)
		{
			return @"<html xmlns=""http://www.w3.org/1999/xhtml"" >
<head>
    <title>Untitled Page</title>
</head>
<body style=""font-family: Arial; font-size: 10pt;"">";
		}

        /// <summary>
        /// Footer for the calculation report
        /// </summary>
        /// <param name="report">context</param>
        /// <returns>HTML</returns>
		public override string Footer(DebugReport report)
		{
			return @"</body>
</html>";
		}

        /// <summary>
        /// Helper Genric. Return the previous item in the list or null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
		public static T Previous<T>(IList<T> source, T item)
		{
			if (source == null || source.Count == 0) return default(T);
			int idx = source.IndexOf(item);
			if (idx == 0) return default(T);
			return source[idx - 1];
		}

        /// <summary>
        /// Helper Generic. Return the next item in the list or null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
		public static T Next<T>(IList<T> source, T item)
		{
			if (source == null || source.Count == 0) return default(T);
			int idx = source.IndexOf(item);
			if (idx >= source.Count-1) return default(T);
			return source[idx + 1];
		}

        /// <summary>
        /// Render a report line as HTML
        /// </summary>
        /// <param name="report">context</param>
        /// <param name="line">current line to render</param>
        /// <returns>HTML</returns>
		public override string SetMarkUp(DebugReport report, DebugReport.ReportLine line)
		{
			if (string.IsNullOrEmpty(line.Text))
			{
				line.TextMarkUp = "<br/>";
				return line.TextMarkUp;
			}

			if (HasHint(line, "H1"))
			{
				line.TextMarkUp = string.Format("<h1>{0}</h1>{1}", line.Text, Environment.NewLine);
				return line.TextMarkUp;
			}

			if (HasHint(line, "H2"))
			{
				line.TextMarkUp = string.Format("<h2>{0}</h2>{1}", line.Text, Environment.NewLine);
				return line.TextMarkUp;
			}

			if (HasHint(line, "H3"))
			{
				line.TextMarkUp = string.Format("<h3>{0}</h3>{1}", line.Text, Environment.NewLine);
				return line.TextMarkUp;
			}

			if (HasHint(line, "H4"))
			{
				line.TextMarkUp = string.Format("<h4>{0}</h4>{1}", line.Text, Environment.NewLine);
				return line.TextMarkUp;
			}

			if (HasHint(line, "LI"))
			{
				string final = "";
				
				DebugReport.ReportLine before = Previous<DebugReport.ReportLine>(report.Report, line);
				if (!HasHint(before, "LI")) final = string.Format("<ul  style=\"text-indent: {0}px;\">", line.Depth * pixelIndent) + Environment.NewLine;


				final += string.Format("	<li>{0}</li>{1}", line.Text, Environment.NewLine);

				DebugReport.ReportLine after = Next<DebugReport.ReportLine>(report.Report, line);
				if (!HasHint(after, "LI")) final += "</ul>" + Environment.NewLine;

				return final;
			}
			

			if (HasHint(line, "LB"))
			{
				int idx = line.Text.IndexOf(": ");
				string label = line.Text.Substring(0, idx);
				string txt = line.Text.Substring(idx + 2, line.Text.Length - idx - 2);
				line.TextMarkUp = string.Format("<p style=\"text-indent: {3}px;\"><b>{0}</b> : {1}</p>{2}", label, txt, Environment.NewLine, line.Depth * pixelIndent);
				return line.TextMarkUp;
			}

			if (HasHint(line, "W"))
			{
				line.TextMarkUp = string.Format("<p style=\"color:red;\">WARNING</p>{0}{1}",  line.Text, Environment.NewLine);
				return line.TextMarkUp;
			}

			line.TextMarkUp = string.Format("<p style=\"text-indent: {2}px;\">{0}</p>{1}", line.Text, Environment.NewLine, line.Depth * pixelIndent);
			return line.TextMarkUp;
		}

        private int pixelIndent = 15;	
	}
}
