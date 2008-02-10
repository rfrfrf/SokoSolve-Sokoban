using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SokoSolve.Common;

namespace SokoSolve.Core.Reporting
{
    /// <summary>
    /// An XHTML Helper class to make well-formed simpleXHTML content
    /// </summary>
    public class ReportXHTML
    {
        protected XmlDocument report;
        private XmlElement xmlHEAD;

       

        public ReportXHTML(string title)
        {

            report = new XmlDocument();
            XmlElement xmlHTML = report.CreateElement("html");
            xmlHTML.SetAttribute("xmlns", "http://www.w3.org/1999/xhtml");
            report.AppendChild(xmlHTML);

            xmlHEAD = report.CreateElement("head");
            xmlHTML.AppendChild(xmlHEAD);

            XmlElement xmlTITLE = report.CreateElement("title");
            xmlTITLE.InnerText = title;
            xmlHEAD.AppendChild(xmlTITLE);

            xmlLINK = report.CreateElement("link");
            xmlLINK.SetAttribute("type", "text/css");
            xmlLINK.SetAttribute("rel", "Stylesheet");
            xmlLINK.SetAttribute("href", "style.css");
            xmlHEAD.AppendChild(xmlLINK);

            xmlSTYLE = report.CreateElement("style");
            xmlSTYLE.SetAttribute("type", "text/css");
            xmlHEAD.AppendChild(xmlSTYLE);

            xmlBODY = report.CreateElement("body");
            xmlHTML.AppendChild(xmlBODY);

            xmlBODYBODY = report.CreateElement("div");
            xmlBODY.AppendChild(xmlBODYBODY);

            xmlFOOTER = report.CreateElement("div");
            xmlFOOTER.SetAttribute("class", "footer");
            xmlBODY.AppendChild(xmlFOOTER);

        }

        XmlElement xmlBODY;
        XmlElement xmlBODYBODY;
        XmlElement xmlFOOTER;
        XmlElement xmlSTYLE;
        XmlElement xmlLINK;

        public void Add(XmlElement addInBody)
        {
            xmlBODYBODY.AppendChild(addInBody);
        }

        public void Add(string addInBody)
        {
            XmlElement xml = report.CreateElement("div");
            xml.InnerXml = addInBody;
            Add(xml);
        }

        public XmlElement Body
        {
            get { return xmlBODYBODY; }
        }

        public void SetCSSLink(string linkURL)
        {
            xmlLINK.SetAttribute("href", linkURL);
            xmlHEAD.RemoveChild(xmlSTYLE);
        }

        public void SetCSSInline(string fileName)
        {
            xmlSTYLE.InnerText = File.ReadAllText(fileName);
            xmlHEAD.RemoveChild(xmlLINK);
        }

        public void AddFooter()
        {

            string html =
                string.Format(
@"Produced by <a href=""http://sokosolve.sourceforge.net"">SokoSolve Sokoban</a> version {0}<br/>
<a href=""http://sourceforge.net""> 
    <img src=""http://sourceforge.net/sflogo.php?group_id=85742&amp;type=5"" width=""210"" height=""62""  alt=""SourceForge.net Logo"" />
</a>", ProgramVersion.VersionString);

            xmlFOOTER.InnerXml = html;


        }

        public XmlElement CreateContentTag(string tagName, string innerText)
        {
            XmlElement xml = report.CreateElement(tagName);
            xml.InnerText = innerText;
            return xml;
        }

        public virtual void Save(string filename)
        {


            report.Save(filename);
        }

        public override string ToString()
        {
            return report.OuterXml;
        }
    }
}
