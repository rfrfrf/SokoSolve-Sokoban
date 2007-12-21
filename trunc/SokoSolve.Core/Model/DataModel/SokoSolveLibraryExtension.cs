using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Model.DataModel
{
    partial class GenericDescription
    {
        public GenericDescription()
        {
            
        }

        public GenericDescription(GenericDescription copy)
        {
            if (copy == null) return;

            this.Name = copy.Name;
            this.Description = copy.Description;
            this.Author = copy.Author;
            this.Date = copy.Date;
            this.DateSpecified = copy.DateSpecified;
            this.License = copy.License;
            this.Comments = copy.Comments;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ...", Name, Description);
        }
    }
}
