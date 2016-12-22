namespace EtAlii.xTechnology.Threading
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Parallel
    {
        public static Task ForEachAsync<T>(
            this IEnumerable<T> source,
            int degreeOfParallelism,
            Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(degreeOfParallelism)
                select Task.Run(async delegate
                {
                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            await body(partition.Current).ContinueWith(t =>
                            {
                                //observe exceptions
                            });
                        }
                    }
                }));
        }

        public static Task ForAsync<T>(
            this IEnumerable<T> source,
            int degreeOfParallelism,
            Func<T, long, Task> body)
        {
            return Task.WhenAll(
                from partition in new SingleElementOrderablePartitioner<T>(source).GetOrderablePartitions(degreeOfParallelism)
                select Task.Run(async delegate
                {
                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            await body(partition.Current.Value, partition.Current.Key).ContinueWith(t =>
                            {
                                //observe exceptions
                            });
                        }
                    }
                }));
        }

    }
}
