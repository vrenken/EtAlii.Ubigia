// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;

    // Source: https://stackoverflow.com/questions/4681949/use-linq-to-group-a-sequence-of-numbers-with-no-gaps
    public static class EnumerableGroupAdjacentByExtension
    {
        public static IEnumerable<IEnumerable<T>> GroupAdjacentBy<T>(this IEnumerable<T> source, Func<T, T, bool> predicate)
        {
            using var e = source.GetEnumerator();
            if (!e.MoveNext()) yield break;
            
            var list = new List<T> { e.Current };
            var pred = e.Current;
            while (e.MoveNext())
            {
                if (predicate(pred, e.Current))
                {
                    list.Add(e.Current);
                }
                else
                {
                    yield return list;
                    list = new List<T> { e.Current };
                }
                pred = e.Current;
            }
            yield return list;
        }
    }
}
