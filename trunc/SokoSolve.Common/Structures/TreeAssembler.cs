using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common;

namespace SokoSolve.Common.Structures
{
	public static class TreeAssembler
	{
		
		/// <summary>
		/// Assemble a tree from ID and REF parent identifiers
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="GetID"></param>
		/// <param name="GetREF"></param>
		/// <returns></returns>
		public static Tree<T> Create<T>(IEnumerable<T> source, ToString<T> GetID, ToString<T> GetREF)
		{
			Tree<T> result = new Tree<T>();

			Dictionary<string, T> nodelist = new Dictionary<string, T>();

			T root = default(T);
			foreach(T item in source)
			{
				string ID = GetID(item);
				if (ID == null) throw new ArgumentNullException("ID");

				string parentID = GetREF(item);
				if (parentID == null)
				{
					// Parent
					if (root != null) throw new ArgumentException("Cannot have two roots");
					root = item;
				}

				if (nodelist.ContainsKey(ID)) throw new ArgumentException(string.Format("Duplicate Key : {0}", ID));

				nodelist.Add(ID, item);
			}

			result.Root.Data = root;

			AddChildren(result.Root, GetID, GetREF, source);

			return result;
		}

		private static void AddChildren<T>(TreeNode<T> current, ToString<T> GetID, ToString<T> GetREF, IEnumerable<T> source)
		{
			string currentID = GetID(current.Data); 
			foreach(T item in source)
			{
				if(GetREF(item) == currentID) 
				{
					// Found child
					current.Add(item);
				}
			}

            if (current.HasChildren)
            {
                foreach (TreeNode<T> kid in current.Children)
                {
                    AddChildren<T>(kid, GetID, GetREF, source);
                }
            }
		}

		
	}
}
