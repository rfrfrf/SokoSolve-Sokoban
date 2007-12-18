using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace SokoSolve.Common
{
    /// <summary>
    /// This basic delegate allows the return of a string from an object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public delegate string ToString<T>(T item);


    /// <summary>
    /// Rudimentary string helper functions
    /// </summary>
    public static class StringHelper
    {
        
        /// <summary>
        /// Find the total number of occurances of achar in source
        /// </summary>
        public static int Count(string source, char achar)
        {
            int cc = 0;
            foreach (char c in source)
            {
                if (c == achar) cc++;
            }
            return cc;
        }

        /// <summary>
        /// Get a substring based on two index (normal is one index and one count)
        /// </summary>
        public static string SubStringOrdinal(string source, int startPos, int endPos)
        {
            if (endPos < 0) endPos = source.Length;
            if (endPos > source.Length) endPos = source.Length;
            return source.Substring(startPos, endPos - startPos);
        }

        /// <summary>
        /// Seperate a string based on a seperator
        /// </summary>
        public static string[] Split(string source, string sep)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Join a list of string using a seperator
        /// </summary>
        public static string Join(IList<string> source, string sep)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in source)
            {
                if (!object.ReferenceEquals( s,  source[0]))
                {
                    sb.Append(sep);
                }
                sb.Append(s);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Join a list of string using a seperator
        /// </summary>
        public static string Join<T>(IList<T> source, ToString<T> toStringDelegate, string sep)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in source)
            {
                if (!object.ReferenceEquals(item, source[0]))
                {
                    sb.Append(sep);
                }
                if (toStringDelegate != null)
                {
                    string result = toStringDelegate(item);
                    if (result != null) sb.Append(result);
                }
                else
                {
                    sb.Append(item.ToString());
                }
            }
            return sb.ToString();
        }

		/// <summary>
		/// Create a string report for an Exception. Supports nesting.
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static string Report(Exception ex)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Exception: {0}{1}", ex.GetType(), Environment.NewLine);
			sb.AppendFormat("Message: {0}{1}", ex.Message, Environment.NewLine);
			sb.AppendFormat("Stack:{1} {0}{1}", ex.StackTrace, Environment.NewLine);
			if (ex.InnerException != null)
			{
				sb.Append(Report(ex.InnerException).Replace(Environment.NewLine, Environment.NewLine + "\t"));
			}
			return sb.ToString();
		}

        /// <summary>
        /// Use reflection to retrieve all properties and show them as strings
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Report(object obj)
        {
            StringBuilder sb = new StringBuilder();

            Type objType = obj.GetType();
            PropertyInfo[] props = objType.GetProperties();
            foreach(PropertyInfo prop in props)
            {
                sb.Append(prop.Name);
                string value = "";

            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Remove all occurances on char from Source
        /// </summary>
        public static string RemoveCharsIn(string Source, string RemoveChars)
        {
            StringBuilder sb = new StringBuilder(Source);
            int pos = 0;
            while(pos < sb.Length)
            {
                while (pos < sb.Length && RemoveChars.IndexOf(sb[pos]) >= 0)
                {
                    sb.Remove(pos, 1);
                }
                pos++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Remove from Source all chars that are not contained in KeepChars
        /// </summary>
        public static string RemoveCharsNotIn(string Source, string KeepChars)
        {
            StringBuilder sb = new StringBuilder(Source);
            int pos = 0;
            while(pos < sb.Length)
            {
                while (pos < sb.Length && KeepChars.IndexOf(sb[pos]) < 0)
                {
                    sb.Remove(pos, 1);
                }
                pos++;
            }
            return sb.ToString();
        }


	
    }
}
