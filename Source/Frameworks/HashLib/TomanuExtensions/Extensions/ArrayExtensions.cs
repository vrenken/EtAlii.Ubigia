// ReSharper disable all

using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using TomanuExtensions.Utils;

namespace TomanuExtensions
{
    [DebuggerStepThrough]
    public static class ArrayExtensions
    {
        /// <summary>
        /// /// True if array are exactly the same.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool AreSame<T>(this T[] array1, T[] array2)
        {
            if (object.ReferenceEquals(array1, array2))
                return true;

            if (array1.Length != array2.Length)
                return false;

            for (var i = 0; i < array1.Length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// /// True if array are exactly the same.
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool AreSame(this byte[] array1, byte[] array2)
        {
            if (object.ReferenceEquals(array1, array2))
                return true;

            if (array1.Length != array2.Length)
                return false;

            for (var i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// True if array are exactly the same.
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool AreSame(this byte[,] array1, byte[,] array2)
        {
            if (object.ReferenceEquals(array1, array2))
                return true;

            if (array1.GetLength(0) != array2.GetLength(1))
                return false;

            for (var x = 0; x < array1.GetLength(0); x++)
            {
                for (var y = 0; y < array1.GetLength(1); y++)
                {
                    if (array1[x, y] != array2[x, y])
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// True if array are exactly the same.
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool AreSame(this ushort[] array1, ushort[] array2)
        {
            if (object.ReferenceEquals(array1, array2))
                return true;

            if (array1.Length != array2.Length)
                return false;

            for (var i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Return hash code for array. Result is xor sum of elements GetHashCode() functions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int GetHashCode<T>(T[] array)
        {
            var sum = 0;

            for (var i = 0; i < array.Length; i++)
                sum ^= array[i].GetHashCode();

            return sum;
        }

        /// <summary>
        /// Check that this is valid index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool InRange<T>(this T[] array, int index)
        {
            return (index >= array.GetLowerBound(0)) && (index <= array.GetUpperBound(0));
        }

        /// <summary>
        /// Clear array with zeroes.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Clear<T>(this T[] array, T value = default(T))
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = value;
        }

        /// <summary>
        /// Clear array with zeroes.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Clear<T>(this T[,] array, T value = default(T))
        {
            for (var x = 0; x < array.GetLength(0); x++)
            {
                for (var y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = value;
                }
            }
        }

        /// <summary>
        /// Return array stated from a_index and with a_count legth.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] array, int index, int count = -1)
        {
            if (count == -1)
                count = array.Length - index;

            var result = new T[count];
            Array.Copy(array, index, result, 0, count);
            return result;
        }

        /// <summary>
        /// Find index of a_element within a_array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns>
        /// Index of element or -1 if not find.
        /// </returns>
        public static int IndexOf<T>(this T[] array, T element)
        {
            for (var i = 0; i < array.Length; i++)
                if (object.ReferenceEquals(element, array[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// Return first occurence of a_sun_array in a_array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="subArray"></param>
        /// <returns></returns>
        public static int FindArrayInArray(this byte[] array, byte[] subArray)
        {
            int i, j;

            for (j = 0; j < array.Length - subArray.Length; j++)
            {
                for (i = 0; i < subArray.Length; i++)
                {
                    if (array[j + i] != subArray[i])
                        break;
                }

                if (i == subArray.Length)
                    return j;
            }

            return -1;
        }

        public static T[] Shuffle<T>(this T[] array)
        {
            return Shuffle(array, Environment.TickCount);
        }

        public static T[] Shuffle<T>(this T[] array, int seed)
        {
            var mt = new MersenneTwister((uint)seed);

            return (from gr in
                        from el in array
                        select new { index = mt.NextInt(), el }
                    orderby gr.index
                    select gr.el).ToArray();
        }

        public static void Fill<T>(this T[,] array, T value)
        {
            for (var x = 0; x < array.GetLength(0); x++)
            {
                for (var y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = value;
                }
            }
        }

        public static void Fill<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.GetLength(0); i++)
                array[i] = value;
        }

        public static IEnumerable<T> ToEnumerable<T>(this T[,] array)
        {
            foreach (var el in array)
                yield return el;
        }
    }
}