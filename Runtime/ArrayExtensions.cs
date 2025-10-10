using System;

namespace MbsCore.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// <para>Adds an item to the end of the array.</para>
        /// </summary>
        /// <param name="item">The item to add to the end of the array. (<paramref name="item" /> can be <see langword="null" /> if T is a reference type.)</param>
        public static T[] Add<T>(this T[] source, T item)
        {
            int length = source.Length + 1;
            var newArray = new T[length];
            for (int i = 0; i < length - 1; i++)
            {
                newArray[i] = source[i];
            }

            newArray[^1] = item;
            return newArray;
        }

        /// <summary>
        /// <para>Adds the elements of the specified collection to the end of the array.</para>
        /// </summary>
        /// <param name="collection">The collection whose elements are added to the end of the array.</param>
        public static T[] AddRange<T>(this T[] source, T[] items)
        {
            int sourceLength = source.Length;
            int length = sourceLength + items.Length;
            var newArray = new T[length];
            for (int i = 0; i < sourceLength; i++)
            {
                newArray[i] = source[i];
            }

            for (int i = sourceLength, j = 0; i < length; i++, j++)
            {
                newArray[i] = items[j];
            }

            return newArray;
        }

        /// <summary>
        /// <para>Determines whether the array contains a specific value.</para>
        /// </summary>
        /// <param name="item">The object to locate in the current collection. (<paramref name="item" /> can be <see langword="null" /> if T is a reference type.)</param>
        public static bool Contains<T>(this T[] source, T item) => source.IndexOf(item) > -1;

        /// <summary>
        /// <para>Searches for the specified object and returns the zero-based index of the first occurrence within the entire array.</para>
        /// </summary>
        /// <param name="item">To be added.</param>
        public static int IndexOf<T>(this T[] source, T item)
        {
            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (source[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// <para>Determines whether every element in the List matches the conditions defined by the specified predicate.</para>
        /// </summary>
        /// <param name="match">The predicate delegate that specifies the check against the elements.</param>
        public static bool TrueForAll<T>(this T[] source, Predicate<T> match)
        {
            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (!match.Invoke(source[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Return a new copy from source array.
        /// </summary>
        /// <returns></returns>
        public static T[] Copy<T>(this T[] source)
        {
            int length = source.Length;
            var newArray = new T[length];
            for (int i = 0; i < length; i++)
            {
                newArray[i] = source[i];
            }

            return newArray;
        }

        /// <summary>
        /// Return a new reverse copy from source array.
        /// </summary>
        /// <returns></returns>
        public static T[] Reverse<T>(this T[] source)
        {
            int length = source.Length;
            var newArray = new T[length];
            for (int i = 0, j = length; i < length; i++, j--)
            {
                newArray[i] = source[j];
            }

            return newArray;
        }
    }
}