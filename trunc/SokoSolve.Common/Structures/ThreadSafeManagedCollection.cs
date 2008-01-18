using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.Structures
{
    public class ThreadSafeManagedCollection<T> : ManagedCollection<T>, IEnumerable<T>
    {
        private readonly object locker = new object();

        public ThreadSafeManagedCollection(ManagedCollectionNotification<T> notification)
            : base(notification)
        {
        }

        #region IEnumerable<T> Members

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (locker)
            {
                List<T> buffer = new List<T>(inner);
                return buffer.GetEnumerator();
            }
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override IEnumerator GetEnumerator()
        {
            lock (locker)
            {
                T[] buffer = inner.ToArray();
                return buffer.GetEnumerator();
            }
        }

        #endregion

        ///<summary>
        ///Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"></see> at the specified index.
        ///</summary>
        ///
        ///<param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
        ///<param name="index">The zero-based index at which item should be inserted.</param>
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
        ///<exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
        public override void Insert(int index, T item)
        {
            lock (locker)
            {
                base.Insert(index, item);
            }
        }

        ///<summary>
        ///Removes the <see cref="T:System.Collections.Generic.IList`1"></see> item at the specified index.
        ///</summary>
        ///
        ///<param name="index">The zero-based index of the item to remove.</param>
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
        ///<exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
        public override void RemoveAt(int index)
        {
            lock (locker)
            {
                base.RemoveAt(index);
            }
        }

        ///<summary>
        ///Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public override void Add(T item)
        {
            lock (locker)
            {
                base.Add(item);
            }
        }


        ///<summary>
        ///Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only. </exception>
        public override void Clear()
        {
            lock (locker)
            {
                base.Clear();
            }
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
        public override void CopyTo(T[] array, int arrayIndex)
        {
            lock (locker)
            {
                base.CopyTo(array, arrayIndex);
            }
        }

        ///<summary>
        ///Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<returns>
        ///true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</returns>
        ///
        ///<param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public override bool Remove(T item)
        {
            lock (locker)
            {
                return base.Remove(item);
            }
        }
    }

}
