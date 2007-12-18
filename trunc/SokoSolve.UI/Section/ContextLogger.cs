using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.UI.Section.Library;

namespace SokoSolve.UI.Section
{
    public class ContextLogger
    {
        public ContextLogger()
        {
            currentDepth = 0;
            maxListSize = 100;

            items = new List<ContextLoggerEntry>(maxListSize);
        }

        /// <summary>
        /// Add new context item/entry
        /// </summary>
        public void Add(ContextLoggerEntry newItem)
        {
            items.Add(newItem);

            if (items.Count > maxListSize) items.RemoveAt(0);
        }

        /// <summary>
        /// Overload
        /// </summary>
        public void Add(Type source, string description)
        {
            Add(new ContextLoggerEntry(description, source, currentDepth));
        }

        /// <summary>
        /// Overload
        /// </summary>
        public void Add(Type source, string description, params object[] parms)
        {
            Add(source, string.Format(description, parms));
        }

        /// <summary>
        /// Overload
        /// </summary>
        public void Add(object  source, string description)
        {
            Add(new ContextLoggerEntry(description, source.GetType(), currentDepth));
        }

        /// <summary>
        /// Overload
        /// </summary>
        public void Add(object  source, string description, params object[] parms)
        {
            Add(source, string.Format(description, parms));
        }

        /// <summary>
        /// Start a new section. Increase indenting
        /// </summary>
        public void StartSection()
        {
            currentDepth++;
        }

        /// <summary>
        /// Start a new section. Decrease indenting
        /// </summary>
        public void EndSection()
        {
            currentDepth--;
        }

        public string CreateReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (ContextLoggerEntry item in items)
            {
                sb.Append(Report(item));
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        static public string Report(ContextLoggerEntry entry)
        {
            StringBuilder sbofs = new StringBuilder();
            for (int cc = 0; cc < entry.Depth; cc++ )
            {
                sbofs.Append('\t');
            }

            return string.Format("{2}{0} === {1}", entry.Source.Name.PadRight(15), entry.Message, sbofs);
        }

        private int currentDepth;
        private int maxListSize;
        private List<ContextLoggerEntry> items;
    }
}
