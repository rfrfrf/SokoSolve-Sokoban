using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.IO
{
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

        public string ImporterName
        {
            get { return importerName; }
            set { importerName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public Exception LastError
        {
            get { return lastError; }
        }

        public List<string> ErrorReport
        {
            get { return errorReport; }
        }

        public override string ToString()
        {
            return ImporterName;
        }

        protected abstract Library ImportImplementation(string FileName);

        private string importerName;
        private string description;
        private Exception lastError;
        private List<string> errorReport;
    }

   
}
