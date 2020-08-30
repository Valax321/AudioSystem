using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valax321.AudioSystem.Utility
{
    /// <summary>
    /// A pool manages a pool of objects of a given type.
    /// </summary>
    /// <typeparam name="T">The type of object this pool manages.</typeparam>
    public class Pool<T> : IEnumerable<T>
    {
        private bool allowPoolAutoExpand;
        private List<T> objects = new List<T>();
        private Queue<T> freeObjects = new Queue<T>();
        private Func<T> allocFunction;

        public int Count => objects.Count;
        
        /// <summary>
        /// Creates a new Pool object.
        /// </summary>
        /// <param name="initialPoolSize">The initial size of the object pool.</param>
        /// <param name="allocFunction">The function used to allocate pool objects. This should return a <see cref="T"/> instance.</param>
        /// <param name="allowPoolAutoExpand">Is this pool allowed to expand itself automatically? If not it will throw an exception when full.</param>
        /// <exception cref="ArgumentNullException">Thrown if allocFunction is null.</exception>
        public Pool(int initialPoolSize, Func<T> allocFunction, bool allowPoolAutoExpand = true)
        {
            if (allocFunction is null)
                throw new ArgumentNullException(nameof(allocFunction));

            this.allocFunction = allocFunction;
            this.allowPoolAutoExpand = allowPoolAutoExpand;
            for (int i = 0; i < initialPoolSize; i++)
            {
                var obj = allocFunction();
                objects.Add(obj);
                freeObjects.Enqueue(obj);
            }
        }

        /// <summary>
        /// Get an object from the pool.
        /// </summary>
        /// <returns>The first available object from the pool.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the pool is exhausted but it is not allowed to expand. See <seealso cref="allowPoolAutoExpand"/></exception>
        public T Get()
        {
            // No free object!
            if (freeObjects.Count == 0)
            {
                if (!allowPoolAutoExpand)
                    throw new InvalidOperationException($"Pool<{typeof(T).Name}> exhausted but it is not allowed to auto expand");
                
                var obj = allocFunction();
                objects.Add(obj);
                freeObjects.Enqueue(obj);
            }

            return freeObjects.Dequeue();
        }

        /// <summary>
        /// Frees an object in the pool when it can be recycled.
        /// </summary>
        /// <param name="obj">The object to free.</param>
        /// <exception cref="InvalidOperationException">Thrown if obj is not a part of this pool.</exception>
        public void Free(T obj)
        {
            if (!objects.Contains(obj))
                throw new InvalidOperationException("The given object is not a part of this pool");
            
            freeObjects.Enqueue(obj);
        }

        /// <summary>
        /// Adds expandCount new items to the pool.
        /// </summary>
        /// <param name="expandCount">The number of new items to add to the pool.</param>
        public void Expand(int expandCount)
        {
            for (int i = 0; i < expandCount; i++)
            {
                var obj = allocFunction();
                objects.Add(obj);
                freeObjects.Enqueue(obj);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) objects).GetEnumerator();
        }
    }
}
