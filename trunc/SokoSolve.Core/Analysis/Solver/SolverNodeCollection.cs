using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SokoSolve.Core.Analysis.Solver
{

    /// <summary>
    /// Provide advanced (high-speed) thread safe acess to large number of SolverNodes.
    /// This class will be used to drive the duplicate and forward/reverse chain checks.
    /// This class must be thread safe.
    /// </summary>
    /// <remarks>
    /// Implemented a HashTable lookup which allows multiple items per hash value.
    /// A very fast collection. 
    /// Not sorted.
    /// </remarks>
    public class SolverNodeCollection : ICollection<SolverNode>
    {
        public SolverNodeCollection()
        {
            items = new Dictionary<int, LinkedHashedItem>(40000);
        }

        private readonly object locker = new object();
        private int count;
        private bool isReadOnly;
        Dictionary<int, LinkedHashedItem> items;

        /// <summary>
        /// Allow duplicates in the hashtable
        /// </summary>
        class LinkedHashedItem
        {
            public LinkedHashedItem(SolverNode node)
            {
                Node = node;
            }


            public LinkedHashedItem(SolverNode node, LinkedHashedItem next)
            {
                Node = node;
                Next = next;
            }

            public SolverNode Node;
            public LinkedHashedItem Next;
        }

        /// <summary>
        /// Get all node with this hashcode
        /// </summary>
        /// <param name="HashCode"></param>
        /// <returns></returns>
        public List<SolverNode>  this[int HashCode]
        {
            get
            {
                if (!items.ContainsKey(HashCode)) return null;
                List<SolverNode> list = new List<SolverNode>();
                LinkedHashedItem linked = items[HashCode];
                while (linked != null)
                {
                    list.Add(linked.Node);
                    linked = linked.Next;
                }
                return list;
            }
        }

        /// <summary>
        /// Find a match (SolverNode.Equals(...)) in the list
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public List<SolverNode> GetMatch(SolverNode rhs)
        {
            int hash = rhs.GetHashCode();
            if (!items.ContainsKey(hash)) return null;
            List<SolverNode> list = new List<SolverNode>();
            LinkedHashedItem linked = items[hash];
            while (linked != null)
            {
                if (!object.ReferenceEquals(rhs, linked.Node) && rhs.Equals(linked.Node))
                {
                    // Match!
                    list.Add(linked.Node);
                }
                
                linked = linked.Next;
            }
            return list;
        }

        #region ICollection<SolverNode> Members

        ///<summary>
        ///Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<param name="newNode">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public void Add(SolverNode newNode)
        {
            if (isReadOnly) throw new InvalidOperationException("Collection is readonly");
            if (newNode == null) throw new ArgumentNullException("newNode");

            lock(locker)
            {
                int hash = newNode.GetHashCode();

                List<SolverNode> hits = this[hash];
                if (hits != null)
                {
                    // Duplicate?
                    
                    foreach (SolverNode hit in hits)
                    {
                        if (hit == null) throw new InvalidDataException("Cannot be null");
                        if (hit.Equals(newNode)) throw new InvalidDataException("Duplicates not allowed in list");
                    }

                    // Add it
                    LinkedHashedItem existing = items[hash];
                    while (existing.Next != null) existing = existing.Next;

                    existing.Next = new LinkedHashedItem(newNode);
                }
                else
                {
                    // New
                    items.Add(hash, new LinkedHashedItem(newNode));
                }

                count++;
            }
        }

        ///<summary>
        ///Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only. </exception>
        public void Clear()
        {
            if (isReadOnly) throw new InvalidOperationException("Collection is readonly");

            lock (locker)
            {
                count = 0;

                items.Clear();
            }
        }

        ///<summary>
        ///Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        ///</summary>
        ///
        ///<returns>
        ///true if item is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        ///</returns>
        ///
        ///<param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        public bool Contains(SolverNode item)
        {
            if (!items.ContainsKey(item.GetHashCode())) return false;

            List<SolverNode> matches = GetMatch(item);
            return (matches != null && matches.Count > 0);
        }

        ///<summary>
        ///Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        ///</summary>
        ///
        ///<param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        ///<param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ///<exception cref="T:System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        ///<exception cref="T:System.ArgumentNullException">array is null.</exception>
        ///<exception cref="T:System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        public void CopyTo(SolverNode[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<returns>
        ///true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</returns>
        ///
        ///<param name="removeNode">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public bool Remove(SolverNode removeNode)
        {
            if (isReadOnly) throw new InvalidOperationException("Collection is readonly");
            if (removeNode == null) throw new ArgumentNullException("removeNode");

            lock(locker)
            {
                int hash = removeNode.GetHashCode();
                if (items.ContainsKey(hash))
                {
                    LinkedHashedItem current = items[hash];
                    if (object.ReferenceEquals(current.Node, removeNode))
                    {
                        // remove first
                        items.Remove(hash);

                        items.Add(hash, current.Next);
                    }
                    else
                    {
                        LinkedHashedItem prev = current;
                        current = current.Next;
                        while (current != null && !object.ReferenceEquals(current.Node, removeNode))
                        {
                            prev = current;
                            current = current.Next;
                        }
                        if (current == null) throw new InvalidDataException("Cannot find node");

                        prev.Next = current.Next;
                    }
                }
                else
                {
                    throw new ArgumentException("Does not exist in list", "removeNode");
                }


                count--;
                return true;
            }
        }

        ///<summary>
        ///Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<returns>
        ///The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</returns>
        ///
        public int Count
        {
            get { return count; }
        }

        ///<summary>
        ///Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        ///</summary>
        ///
        ///<returns>
        ///true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.
        ///</returns>
        ///
        public bool IsReadOnly
        {
            get { return isReadOnly; }
        }

        #endregion

        #region IEnumerable<SolverNode> Members

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        IEnumerator<SolverNode> IEnumerable<SolverNode>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<SolverNode>) this).GetEnumerator();
        }

        #endregion
    }
}
