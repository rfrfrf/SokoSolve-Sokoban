using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common
{

    /// <summary>
    /// Format a calculation report into a fixed-width debug style text-only report.
    /// <see cref="DebugReport"/>
    /// </summary>
	public class DebugReportFormatter
	{		
        /// <summary>
        /// Render a line as final markup
        /// </summary>
        /// <param name="report">Report source</param>
        /// <param name="line">The current line to format</param>
        /// <returns>final string including markup</returns>
		public virtual string SetMarkUp(DebugReport report, DebugReport.ReportLine line)
		{
			if (HasHint(line, "H1"))
			{
				line.TextMarkUp = MakeLineMulti(line, new object[] {"", "=========================================", line.Text, "=========================================" });
				return line.TextMarkUp;
			}

			if (HasHint(line, "H2"))
			{
                line.TextMarkUp = MakeLineMulti(line, new object[] { "", "----------------------------------------------------------------------------", line.Text, "----------------------------------------------------------------------------" });
				return line.TextMarkUp;
			}

			if (HasHint(line, "H3"))
			{
				line.TextMarkUp = MakeLine(line, "====== {0} ======", line.Text);
				return line.TextMarkUp;
			}

			if (HasHint(line, "H4"))
			{
				line.TextMarkUp = MakeLine(line, "---- {0} ----", line.Text);
				return line.TextMarkUp;
			}

			if (HasHint(line, "LI"))
			{
				line.TextMarkUp = MakeLine(line, "{0} o {1}", indent, line.Text);
				return line.TextMarkUp;
			}

			if (HasHint(line, "LB"))
			{
				line.TextMarkUp = MakeLine(line, line.Text);
				return line.TextMarkUp;
			}

			if (HasHint(line, "W"))
			{
				line.TextMarkUp = MakeLine(line, "*** WARNING *** {0}", line.Text);
				return line.TextMarkUp;
			}

			return MakeLine(line, line.Text);
		}

        /// <summary>
        /// Content block (markup) at the top of the report
        /// </summary>
        /// <param name="report">context</param>
        /// <returns>markup</returns>
        public virtual string Header(DebugReport report)
        {
            return string.Empty;
        }

        /// <summary>
        /// Content block (markup) at the foot of the report
        /// </summary>
        /// <param name="report">context</param>
        /// <returns>markup</returns>
        public virtual string Footer(DebugReport report)
        {
            return string.Empty;
        }


        /// <summary>
        /// Helper function
        /// </summary>
        /// <param name="reportLine">line in question</param>
        /// <param name="format">string.format literal</param>
        /// <param name="args">parameters</param>
        /// <returns>markup text</returns>
		string MakeLine(DebugReport.ReportLine reportLine, string format, params  object [] args)
		{
			return MakeLineMulti(reportLine, format == null ? new object[] { "" } : new object[] { string.Format(format, args) } );
		}

        /// <summary>
        /// Helper funcation
        /// </summary>
        /// <param name="reportLine">line in question</param>
        /// <param name="parm">a numer of lines to add as a single line</param>
        /// <returns>markup text</returns>
		string MakeLineMulti(DebugReport.ReportLine reportLine,  object [] parm)
		{
			StringBuilder sb = new StringBuilder();
			foreach(object line in parm)
			{
				// Add indent
				for (int cc = 0; cc < reportLine.Depth; cc++) sb.Append(indent);

				// Add text
				if (line != null) sb.Append(line.ToString());

				// Add linebreak
				//if (parm.Length > 0 && line != parm[parm.Length-1])
				
				sb.Append(Environment.NewLine);
				
			}
			return sb.ToString();
		}

        /// <summary>
        /// Helper Method. Is there an additional 'hint' to provide information outside of the scope of the simple debug report content model.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="hint">apply a string.IndexOf(...)</param>
        /// <returns></returns>
		protected bool HasHint(DebugReport.ReportLine line, string hint)
		{
			if (line == null) return false;
			if (line.Hints == null) return false;
			return line.Hints.IndexOf(hint) >= 0;
		}

        /// <summary>
        /// Literal to use for 1xindent
        /// </summary>
        private string indent = "   ";
	}
}
