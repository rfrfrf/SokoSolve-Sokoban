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
            this.Name = copy.Name;
            this.Description = copy.Description;
            this.Author = copy.Author;
            this.Date = copy.Date;
            this.DateSpecified = copy.DateSpecified;
            this.License = copy.License;
            this.Comments = copy.Comments;
        }
    }
}
