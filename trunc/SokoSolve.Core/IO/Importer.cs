using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.IO
{
    /// <summary>
    /// Import a puzzle from a file
    /// </summary>
    public abstract class Importer
    {
        public Importer()
        {
            errorReport = new List<string>();
            lastError = null;
        }

        /// <summary>
        /// Import a file with common error protection
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public  Library Import(string FileName)
        {
            try
            {
                return ImportImplementation(FileName);
            }
            catch(Exception ex)
            {
                lastError = ex;
                errorReport.Add(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Descriptive short name
        /// </summary>
        public string ImporterName
        {
            get { return importerName; }
            set { importerName = value; }
        }

        /// <summary>
        /// Importer format description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Library details, author etc
        /// </summary>
        public GenericDescription Details
        {
            get { return details; }
            set { details = value; }
        }

        /// <summary>
        /// If the importer failed, this will be the cause exception
        /// </summary>
        public Exception LastError
        {
            get { return lastError; }
        }

        /// <summary>
        /// Error report
        /// </summary>
        public List<string> ErrorReport
        {
            get { return errorReport; }
        }

        /// <summary>
        /// Return the name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ImporterName;
        }

        /// <summary>
        /// Worker method, to be implemented
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        protected abstract Library ImportImplementation(string FileName);

        private string importerName;
        private string description;
        private Exception lastError;
        private List<string> errorReport;
        GenericDescription details;

    }

   
}
