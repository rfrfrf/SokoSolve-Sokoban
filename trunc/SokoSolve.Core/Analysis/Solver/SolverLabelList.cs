using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// A simple list of name/value human readable values
    /// </summary>
    public class SolverLabelList : List<SolverLabel>
    {
        public SolverLabel Add(string name, string value)
        {
            SolverLabel txt = new SolverLabel(name, value, null);
            Add(txt);
            return txt;
        }

        public SolverLabel Add(string name, float value)
        {
            SolverLabel txt = new SolverLabel(name);
            txt.SetValue(value);
            Add(txt);
            return txt;
        }

        public SolverLabel Add(string name, bool value)
        {
            return Add(name, value ? "Yes" : "No");
        }

        public SolverLabel Add(string name, DateTime value)
        {
            SolverLabel txt = new SolverLabel(name);
            txt.SetValue(value);
            Add(txt);
            return txt;
        }

        public SolverLabel Add(string name, TimeSpan value)
        {
            SolverLabel txt = new SolverLabel(name);
            txt.SetValue(value);
            Add(txt);
            return txt;
        }

        public SolverLabel AddFormat(string name, string valueFormat, params object[] frmtValues)
        {
            SolverLabel txt = new SolverLabel(name, string.Format(valueFormat, frmtValues), null);
            Add(txt);
            return txt;
        }

        public void AddListSummary<T>(string name, List<T> list, ToString<T> convert, string sep)
        {
            Add(name,StringHelper.Join(list, convert, sep)); 
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SolverLabel label in this)
            {
                sb.Append(label.Name);
                sb.Append(" : ");
                if (label.Value != null) sb.Append(label.Value);
                if (label.UnitOfMeasure!= null)
                {
                    sb.Append(" ");
                    sb.Append(label.UnitOfMeasure);
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public string ToHTMLDocument()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body >");
            sb.Append(ToHTML());
            sb.Append("</body></html>");
            return sb.ToString();
        }

        public string ToHTML()
        {
            return ToHTML(null, null);
        }

        public string ToHTML(string tableStyle, string tableClass)
        {
            StringBuilder sb = new StringBuilder();

            if (tableStyle != null)
            {


                sb.AppendFormat("<table style=\"{0}\">\n", tableStyle);
            }
            else
            {
                if (tableClass == null) tableClass = "tableinfo";
                sb.AppendFormat("<table class=\"{0}\">\n", tableClass);   
            }
            
            foreach (SolverLabel label in this)
            {
                sb.Append("<tr><th>\n");
                sb.Append(label.Name);
                sb.Append("</th><td>\n");
                if (label.Value != null) sb.Append(label.Value);
                if (label.UnitOfMeasure != null)
                {
                    sb.Append(" ");
                    sb.Append(label.UnitOfMeasure);
                }
                sb.Append("</td></tr>\n");
                sb.Append(Environment.NewLine);
            }
            sb.Append("</table>\n");
            
            return sb.ToString();
        }
    }
}
