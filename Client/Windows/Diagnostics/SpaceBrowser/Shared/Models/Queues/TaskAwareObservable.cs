namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TaskAwareObservable<T> : IObservable<T>, IDisposable
    {
        private readonly Task task;
        private readonly Subject<T> subject;
        private readonly CancellationTokenSource taskCancellationTokenSource;

        public TaskAwareObservable(Subject<T> subject, Task task, CancellationTokenSource tokenSource)
        {
            this.task = task;
            this.subject = subject;
            taskCancellationTokenSource = tokenSource;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var disposable = subject.Subscribe(observer);
            if (task.Status == TaskStatus.Created)
                task.Start();
            return disposable;
        }

        public void Dispose()
        {
            // cancel consumption and wait task to finish
            taskCancellationTokenSource.Cancel();
            task.Wait();

            // dispose tokenSource and task
            taskCancellationTokenSource.Dispose();
            task.Dispose();

            // dispose subject
            subject.Dispose();
        }
    }
}
