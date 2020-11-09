// ReSharper disable all

using System;
using System.Diagnostics;

namespace HashLib
{
    [DebuggerStepThrough]
    internal static class ArrayExtensions
    {
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
        public static T[] SubArray<T>(this T[] array, int index, int count)
        {
            var result = new T[count];
            Array.Copy(array, index, result, 0, count);
            return result;
        }
    }
}