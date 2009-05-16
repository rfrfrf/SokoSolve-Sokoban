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

        public static GenericDescription Combine(GenericDescription lhs, GenericDescription rhs)
        {
            if (lhs == null && rhs == null) return null;
            if (lhs == null && rhs != null) return rhs;
            if (lhs != null && rhs == null) return lhs;

            GenericDescription res = new GenericDescription();
            res.Author = Combine(lhs.Author, rhs.Author);
            res.Comments = Combine(lhs.Comments, rhs.Comments);
            res.Date = Combine(lhs.Date, rhs.Date);
            res.DateSpecified = res.Date != DateTime.MinValue;
            res.Description = Combine(lhs.Description, rhs.Description);
            res.License = Combine(lhs.License, rhs.License);
            res.Name = Combine(lhs.Name, rhs.Name);
            return res;
        }

        public static GenericDescriptionAuthor Combine(GenericDescriptionAuthor lhs, GenericDescriptionAuthor rhs)
        {
            if (lhs == null && rhs == null) return null;
            if (lhs == null && rhs != null) return rhs;
            if (lhs != null && rhs == null) return lhs;

            GenericDescriptionAuthor res = new GenericDescriptionAuthor();
            res.Email = Combine(lhs.Email, rhs.Email);
            res.Homepage = Combine(lhs.Homepage, rhs.Homepage);
            res.Name = Combine(lhs.Name, rhs.Name);
            return res;
        }

        public static string Combine(string lhs, string rhs)
        {
            if (!string.IsNullOrEmpty(lhs)) return lhs;
            return rhs;
        }

        public static DateTime Combine(DateTime lhs, DateTime rhs)
        {
            if (lhs == DateTime.MinValue) return rhs;
            return lhs;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ...", Name, Description);
        }
    }
}
