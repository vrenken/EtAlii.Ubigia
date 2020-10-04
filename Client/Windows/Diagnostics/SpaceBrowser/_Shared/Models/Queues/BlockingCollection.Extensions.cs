namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Concurrent;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Threading.Tasks;

    public static class ConcurrentQueueExtensions
    {
        // TODO: devise a way to avoid problems if collection gets too big (produced faster than consumed)
        public static IObservable<T> AsRateLimitedObservable<T>(this BlockingCollection<T> sequence, int items, TimeSpan timePeriod, CancellationToken producerToken)
        {
            var subject = new Subject<T>();

            // this is a dummyToken just so we can recreate the TokenSource
            // which we will pass the proxy class so it can cancel the task
            // on disposal
            var dummyToken = new CancellationToken();
            var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(producerToken, dummyToken);

            var consumingTask = new Task(() =>
            {
                using (var throttle = new Throttle(items, timePeriod))
                {
                    while (!sequence.IsCompleted)
                    {
                        try
                        {
                            var item = sequence.Take(producerToken);
                            throttle.WaitToProceed();
                            try
                            {
                                subject.OnNext(item);
                            }
                            catch (Exception ex)
                            {
                                subject.OnError(ex);
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                    }
                    subject.OnCompleted();
                }
            }, TaskCreationOptions.LongRunning);

            return new TaskAwareObservable<T>(subject, consumingTask, tokenSource);
        }
    }
}
