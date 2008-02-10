using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SokoSolve.Common
{
	/// <summary>
	/// The class encapsultes logic to create a 'calculation report' for a continous stream of logic.
	/// It has a hierarchy to make procedural logic more clear.
	/// </summary>
	public class DebugReport
	{
		private DebugReportFormatter formatter;
		private List<ReportLine> report;
		private int currentDepth;
		private bool enabled;


	    public DebugReport() : this(new DebugReportFormatter())
	    {
	    }

	    /// <summary>
		/// Default Constructor
		/// </summary>
		public DebugReport(DebugReportFormatter formatter)
		{
            this.formatter = formatter;
			report = new List<ReportLine>(200);
			currentDepth = 0;
			enabled = true;
		}

		/// <summary>
		/// Current indent depth
		/// </summary>
		public int CurrentDepth
		{
			get { return currentDepth; }
			set { currentDepth = value; }
		}

		/// <summary>
		/// Disable/Enable adding new lines to the report
		/// </summary>
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		/// <summary>
		/// Clear the report.
		/// </summary>
		public void Clear()
		{
			report.Clear();
		}

		#region Headings

		public void AppendHeadingOne(string format, params object[] args)
		{
			AppendHeading(1, format, args);
			
		}

		public void AppendHeadingTwo(string format, params object[] args)
		{
			AppendHeading(2, format, args);
		}

		public void AppendHeadingThree(string format, params object[] args)
		{
			AppendHeading(3, format, args);
		}

		public void AppendHeading(int Level, string format, params object[] args)
		{
			if (!enabled) return;

			ReportLine newLine = AddLine(format, args);
			newLine.SetHint("H" + Level.ToString());

			StartSection();
		}

		#endregion Headings


		#region Primitives
		
		public void Append(string line)
		{
			if (!enabled) return;
			if (line == null) return;
			AddLine(line);
		}

	
		public void Append(string format, params object[] args)
		{
			if (!enabled) return;
			if (format == null) return;
			AddLine(string.Format(format, args));
		}

		/// <summary>
		/// Append to the last line of the report without a line break (do not add a new report line)
		/// </summary>
		/// <param name="format">Text in the stype of string.format</param>
		public void AppendPartial(string format, params object[] args)
		{
			if (!enabled) return;
			if (format == null) return;
			string line = string.Format(format, args);
			if (report.Count == 0)
			{
				AddLine(line);
				return;
			}
			report[0].Text = report[0].Text + line;
		}

		#endregion Primitives


		#region ContentFormatters

        public void AppendException(Exception ex)
        {
            Append(StringHelper.Report(ex));
        }

        public void AppendTimeStamp(string format, params object [] args)
        {
            if (string.IsNullOrEmpty(format)) return;

            Append("{0} [{1}] {2}", DateTime.Now.ToString("s"), KeepLength(Thread.CurrentThread.Name, 3), string.Format(format, args));
        }

        string KeepLength(string source, int length)
        {
            if (source.Length == length) return source;
            if (source.Length > length) return source.Substring(0, length);
            return source.PadRight(length, ' ');
        }

		public void AppendTable(string [,] table)
		{
			throw new NotImplementedException();
		}

		public void AppendLabel(string label, string format, params object[] args)
		{
			if (!enabled) return;
			string text = "";
			if (format != null) text = string.Format(format, args);
			ReportLine newLine = AddLine(label + ": " + text);
			newLine.SetHint("LB");
		}

		public void AppendWarning(string format, params object[] args)
		{
			if (!enabled) return;
			if (format == null) return;
			ReportLine newLine = AddLine(format, args);
			newLine.SetHint("W");
		}

		public void AppendListItem(string format, params object[] args)
		{
			if (!enabled) return;
			ReportLine newLine = AddLine(format, args);
			newLine.SetHint("LI");
		}

		#endregion ContentFormatters


		#region IndentControls

		public void CompleteSection()
		{
			if (!enabled) return;
			currentDepth--;
		}

		public void StartSection()
		{
			if (!enabled) return;
			currentDepth++;
		}

		#endregion IndentControls

		/// <summary>
		/// Append an existing nested report, and the current depth.
		/// </summary>
		/// <param name="nested">Nexted report to append</param>
		public void Append(DebugReport nested)
		{
			foreach(ReportLine nestedLine in nested.Report)
			{
				ReportLine newLine = new ReportLine();
				newLine.Text = nestedLine.Text;
				newLine.Depth = currentDepth + nestedLine.Depth;
				newLine.Hints = nestedLine.Hints;
				newLine.TextMarkUp = null;
				report.Add(newLine);
			}
		}

		/// <summary>
		/// Output the entire report as a single large string, with line breaks
		/// </summary>
		/// <returns>string report</returns>
		public override string ToString()
		{
            return ToString(formatter);
		}

		/// <summary>
		/// Output the entire report as a single large string, with line breaks
		/// </summary>
		/// <returns>string report</returns>
		public virtual string ToString(DebugReportFormatter formatter)
		{
			StringBuilder sb = new StringBuilder(report.Count * 80);
			sb.Append(formatter.Header(this));
			foreach (ReportLine line in report)
			{
				sb.Append(formatter.SetMarkUp(this, line));
			}
			sb.Append(formatter.Footer(this));
			return sb.ToString();
		}

		/// <summary>
		/// Allow the formatters access to the inner report lines list
		/// </summary>
		protected internal List<ReportLine> Report
		{
			get { return report; }
		}

		#region PrivateHelpers

		/// <summary>
		/// Common add line method
		/// </summary>
		/// <param name="Text"></param>
		ReportLine AddLine(string Text)
		{
			if (!enabled) return null;
		
		    string[] split = StringHelper.Split(Text, Environment.NewLine);
		    ReportLine first = null;
		    foreach (string s in split)
		    {
                ReportLine newLine = new ReportLine();
                newLine.Depth = currentDepth;
                newLine.Text = s;
                newLine.TextMarkUp = null;
		        report.Add(newLine);
                if (first == null) first = newLine;
		    }


            return first;
		}

		/// <summary>
		/// Common add line method
		/// </summary>
		ReportLine AddLine(string format, params object[] args)
		{
			if (!enabled) return null;
			return AddLine(string.Format(format, args));
		}

		#endregion PrivateHelpers

		public class ReportLine
		{
			private int depth;
			private string text;
			private string textMarkUp;
			private string hints;


			public int Depth
			{
				get { return depth; }
				set { depth = value; }
			}

			public string Text
			{
				get { return text; }
				set { text = value; }
			}

			public string Hints
			{
				get { return hints; }
				set { hints = value; }
			}

			public string TextMarkUp
			{
				get { return textMarkUp; }
				set { textMarkUp = value; }
			}

			public void SetHint(string hint)
			{
				if (hint == null) return;
				if (hints == null)
				{
					hints = hint;
					return;
				}
				if (hints.IndexOf(hint) < 0) hints += ' ' + hint; 
			}
		}
	}
}
