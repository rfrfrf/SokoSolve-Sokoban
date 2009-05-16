using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using SokoSolve.Core;
using SokoSolve.Core.Model;

namespace SokoSolve.UI.Controls.Web
{
    /// <summary>
    /// Simple HTML builder for consistency
    /// </summary>
    public class HtmlBuilder
    {
        public string GetHTMLPage()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(header, title, fileCSS);
            result.Append(htmlBody.ToString());
            result.Append(footer);
            return result.ToString();
        }

        public string GetHTMLBody()
        {
            return htmlBody.ToString();
        }

        public void Add(string fragment)
        {
            if (fragment == null) return;

            htmlBody.Append(fragment);
        }

        public void Add(HtmlBuilder body)
        {
            if (body == null) return;

            htmlBody.Append(body.GetHTMLBody());
        }

        public void Add(string fragmentFormat, params object[] parm)
        {
            if (string.IsNullOrEmpty(fragmentFormat)) return;
            Add(string.Format(fragmentFormat, parm));
        }

        public void AddLine(string fragmentFormat, params object[] parm)
        {
            if (string.IsNullOrEmpty(fragmentFormat)) return;
            Add(string.Format(fragmentFormat, parm)+"<br/>");
        }

        public void AddLabel(string label, string format, params object[] parm)
        {
            if (label == null) return;
            if (string.IsNullOrEmpty(format)) return;
            Add("<b>{0}</b>: {1}<br/>{2}", label, format == null ? "" : string.Format(format, parm), Environment.NewLine);
        }

        /// <summary>
        /// Add a temp image, by creating a temp image.
        /// </summary>
        /// <param name="Image"></param>
        public void Add(PuzzleMap PuzzleMap, Image Image, string style)
        {
            string tmpFile = ImageFileCache.Singleton.GetImageURL(PuzzleMap);
            if (tmpFile == null)
            {
                tmpFile = ImageFileCache.Singleton.AddSaveImage(PuzzleMap, Image);
            }
            Add("<img src=\"{0}\" style=\"{1}\" alt=\"SokoSolve\"/>", tmpFile, style);
        }

        public void AddSection(string title)
        {
            if (title != null) Add("<h{0}>{1}</h{0}>", currentDepth, title);

            currentDepth++;
        }

        public void EndSection()
        {
            currentDepth--;
        }

        public override string ToString()
        {
            return GetHTMLPage();
        }


        private string fileCSS = FileManager.GetContent("$html/style.css");
        private string header = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
	<head>
		<title>{0}</title>
		<link type=""text/css"" rel=""Stylesheet"" href=""{1}"" />
	</head>
	<body>";
        private string footer = @"	</body>
</html>";
        private string title = "SokoSolve";
        private StringBuilder htmlBody = new StringBuilder();
        private int currentDepth = 1;
    }
}


