using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common
{
	public static class ListHelper
	{
		static private Random random = new Random();

        /// <summary>
        /// Helper to resize an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source"></param>
        /// <param name="NewItem"></param>
        /// <returns></returns>
        public static T[] AddToArray<T>(T[] Source, T NewItem)
        {
            if (Source == null || Source.Length == 0)
            {
                T[] newresult = new T[1];
                newresult[0] = NewItem;
                return newresult;
            }

            T[] result = new T[Source.Length+1];
            Source.CopyTo(result, 0);
            result[result.Length - 1] = NewItem;
            return result;
        }


		/// <summary>
		/// Select a single item randomly from a list
		/// </summary>
		public static T RandomSelection<T>(IList<T> list)
		{
			return list[random.Next(0, list.Count)];
		}

		/// <summary>
		/// Return the first item from a list (null if not present)
		/// </summary>
		public static T First<T>(IList<T> list)
		{
			if (list == null || list.Count == 0) return default(T);
			return list[0];
		}

		/// <summary>
		/// Return the last item from a list (null if not present)
		/// </summary>
		public static T Last<T>(IList<T> list)
		{
			if (list == null || list.Count == 0) return default(T);
			return list[list.Count - 1];
		}

		/// <summary>
		/// Select a number of items from a list, randomly, without duplicates
		/// </summary>
		public static List<T> RandomSubSet<T>(IList<T> source, int count)
		{
			List<T> current = new List<T>(source);
			List<T> results = new List<T>();

			for (int cc = 0; cc < count; cc++)
			{
				if (current.Count == 0) return results;

				T selection = current[random.Next(0, current.Count)];
				current.Remove(selection);
				results.Add(selection);
			}
			return results;
		}

		/// <summary>
		/// See if a list has duplicates (order will be sorted)
		/// </summary>
		public static bool HasDuplicates<T>(List<T> inputList)
		{
			// No duplicates
			if (inputList == null || inputList.Count == 1) return false;

			// Sort, then see if any two object in sequence are equal.
			inputList.Sort();
			for (int cc = 0; cc < inputList.Count - 1; cc++)
			{
				if (inputList[cc].Equals(inputList[cc + 1]))
				{
					// Two object in sequnce are equal -> duplicates exist
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Count the number of items which return true to a predicate.
		/// </summary>
		public static int Count<T>(IList<T> inputList, Predicate<T> predicate)
		{
			if (inputList == null) return 0;

			int count = 0;
			foreach (T item in inputList)
			{
				if (predicate(item)) count++;
			}
			return count;
		}
	}
}
