namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using TaskAsyncHelper = SignalRTaskAsyncHelper;

    internal static class SignalRTaskAsyncHelper
    {
        private static Task Empty { get; } = (Task)MakeTask((object)null);

        public static Task<bool> True { get; } = MakeTask(true);

        public static Task<bool> False { get; } = MakeTask(false);

        private static Task<T> MakeTask<T>(T value)
        {
            return FromResult(value);
        }

        public static Task OrEmpty(this Task task)
        {
            return task ?? Empty;
        }

        public static Task<T> OrEmpty<T>(this Task<T> task)
        {
            return task ?? TaskCache<T>.Empty;
        }

        public static Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
        {
            try
            {
                return Task.Factory.FromAsync(beginMethod, endMethod, state);
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task<T> FromAsync<T>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, T> endMethod, object state)
        {
            try
            {
                return Task.Factory.FromAsync(beginMethod, endMethod, state);
            }
            catch (Exception ex)
            {
                return FromError<T>(ex);
            }
        }

        public static Task Series(Func<object, Task>[] tasks, object[] state)
        {
            Task task = Empty;
            for (int index = 0; index < tasks.Length; ++index)
                task = Then(task, tasks[index], state[index]);
            return task;
        }

        public static TTask Catch<TTask>(this TTask task) where TTask : Task
        {
            return Catch(task, ex => { });
        }

        private static TTask Catch<TTask>(this TTask task, Action<AggregateException, object> handler, object state) where TTask : Task
        {
            if (task != null && task.Status != TaskStatus.RanToCompletion)
            {
                if (task.Status == TaskStatus.Faulted)
                    ExecuteOnFaulted(handler, state, task.Exception);
                else
                    AttachFaultedContinuation(task, handler, state);
            }
            return task;
        }

        private static void AttachFaultedContinuation<TTask>(TTask task, Action<AggregateException, object> handler, object state) where TTask : Task
        {
            ContinueWithPreservedCulture(task, innerTask => ExecuteOnFaulted(handler, state, innerTask.Exception), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
        }

        private static void ExecuteOnFaulted(Action<AggregateException, object> handler, object state, AggregateException exception)
        {
            handler(exception, state);
        }

        private static TTask Catch<TTask>(this TTask task, Action<AggregateException> handler) where TTask : Task
        {
            return Catch(task, (ex, state) => ((Action<AggregateException>)state)(ex), handler);
        }

        public static Task ContinueWithNotComplete(this Task task, Action action)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return task;
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    try
                    {
                        action();
                        return task;
                    }
                    catch (Exception ex)
                    {
                        return FromError(ex);
                    }
                default:
                    TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
                    ContinueWithPreservedCulture(task, t =>
                    {
                        if (!t.IsFaulted)
                        {
                            if (!t.IsCanceled)
                            {
                                tcs.TrySetResult(null);
                                return;
                            }
                        }
                        try
                        {
                            action();
                            if (t.IsFaulted)
                                TrySetUnwrappedException(tcs, t.Exception);
                            else
                                tcs.TrySetCanceled();
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }, TaskContinuationOptions.ExecuteSynchronously);
                    return tcs.Task;
            }
        }

        public static void ContinueWithNotComplete(this Task task, TaskCompletionSource<object> tcs)
        {
            ContinueWithPreservedCulture(task, t =>
            {
                if (t.IsFaulted)
                {
                    SetUnwrappedException(tcs, t.Exception);
                }
                else
                {
                    if (!t.IsCanceled)
                        return;
                    tcs.SetCanceled();
                }
            }, TaskContinuationOptions.NotOnRanToCompletion);
        }

        public static Task ContinueWith(this Task task, TaskCompletionSource<object> tcs)
        {
            ContinueWithPreservedCulture(task, t =>
            {
                if (t.IsFaulted)
                    TrySetUnwrappedException(tcs, t.Exception);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(null);
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        public static void ContinueWith<T>(this Task<T> task, TaskCompletionSource<T> tcs)
        {
            ContinueWithPreservedCulture(task, t =>
            {
                if (t.IsFaulted)
                    TrySetUnwrappedException(tcs, t.Exception);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(t.Result);
            });
        }

        public static Task Then(this Task task, Action successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return RunTask(task, successor);
            }
        }

        public static Task<TResult> Then<TResult>(this Task task, Func<TResult> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);
                case TaskStatus.Canceled:
                    return Canceled<TResult>();
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);
                default:
                    return TaskRunners<object, TResult>.RunTask(task, successor);
            }
        }

        public static Task Then<T1>(this Task task, Action<T1> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return GenericDelegates<object, object, T1, object>.ThenWithArgs(task, successor, arg1);
            }
        }

        public static Task Then<T1, T2>(this Task task, Action<T1, T2> successor, T1 arg1, T2 arg2)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1, arg2);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return GenericDelegates<object, object, T1, T2>.ThenWithArgs(task, successor, arg1, arg2);
            }
        }

        private static Task Then<T1>(this Task task, Func<T1, Task> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return FastUnwrap(GenericDelegates<object, Task, T1, object>.ThenWithArgs(task, successor, arg1));
            }
        }

        public static Task Then<T1, T2>(this Task task, Func<T1, T2, Task> successor, T1 arg1, T2 arg2)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1, arg2);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return FastUnwrap(GenericDelegates<object, Task, T1, T2>.ThenWithArgs(task, successor, arg1, arg2));
            }
        }

        public static Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, Task<TResult>> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);
                case TaskStatus.Canceled:
                    return Canceled<TResult>();
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);
                default:
                    return FastUnwrap(TaskRunners<T, Task<TResult>>.RunTask(task, t => successor(t.Result)));
            }
        }

        public static Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, TResult> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);
                case TaskStatus.Canceled:
                    return Canceled<TResult>();
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);
                default:
                    return TaskRunners<T, TResult>.RunTask(task, t => successor(t.Result));
            }
        }

        public static Task<TResult> Then<T, T1, TResult>(this Task<T> task, Func<T, T1, TResult> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result, arg1);
                case TaskStatus.Canceled:
                    return Canceled<TResult>();
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);
                default:
                    return GenericDelegates<T, TResult, T1, object>.ThenWithArgs(task, successor, arg1);
            }
        }

        public static Task Then(this Task task, Func<Task> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return FastUnwrap(TaskRunners<object, Task>.RunTask(task, successor));
            }
        }

        public static Task<TResult> Then<TResult>(this Task task, Func<Task<TResult>> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);
                case TaskStatus.Canceled:
                    return Canceled<TResult>();
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);
                default:
                    return FastUnwrap(TaskRunners<object, Task<TResult>>.RunTask(task, successor));
            }
        }

        public static Task Then<TResult>(this Task<TResult> task, Action<TResult> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return TaskRunners<TResult, object>.RunTask(task, successor);
            }
        }

        public static Task Then<TResult>(this Task<TResult> task, Func<TResult, Task> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return FastUnwrap(TaskRunners<TResult, Task>.RunTask(task, t => successor(t.Result)));
            }
        }

        public static Task<TResult> Then<TResult, T1>(this Task<TResult> task, Func<Task<TResult>, T1, Task<TResult>> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task, arg1);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return FastUnwrap(GenericDelegates<TResult, Task<TResult>, T1, object>.ThenWithArgs(task, successor, arg1));
            }
        }

        public static Task Finally(this Task task, Action<object> next, object state)
        {
            try
            {
                switch (task.Status)
                {
                    case TaskStatus.RanToCompletion:
                        return FromMethod(next, state);
                    case TaskStatus.Canceled:
                    case TaskStatus.Faulted:
                        next(state);
                        return task;
                    default:
                        return RunTaskSynchronously(task, next, state, false);
                }
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task RunSynchronously(this Task task, Action successor)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    return task;
                default:
                    return RunTaskSynchronously(task, state => ((Action)state)(), successor, true);
            }
        }

        private static Task FastUnwrap(this Task<Task> task)
        {
            return (task.Status == TaskStatus.RanToCompletion ? task.Result : null) ?? TaskExtensions.Unwrap(task);
        }

        private static Task<T> FastUnwrap<T>(this Task<Task<T>> task)
        {
            return (task.Status == TaskStatus.RanToCompletion ? task.Result : null) ?? TaskExtensions.Unwrap(task);
        }

        public static Task Delay(TimeSpan timeOut)
        {
            TaskCompletionSource<object> completionSource = new TaskCompletionSource<object>();
            Timer timer = new Timer(new TimerCallback(completionSource.SetResult), null, timeOut, TimeSpan.FromMilliseconds(-1.0));
            return ContinueWithPreservedCulture(completionSource.Task, _ => timer.Dispose(), TaskContinuationOptions.ExecuteSynchronously);
        }

        private static Task FromMethod(Action func)
        {
            try
            {
                func();
                return Empty;
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        private static Task FromMethod<T1>(Action<T1> func, T1 arg)
        {
            try
            {
                func(arg);
                return Empty;
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        private static Task FromMethod<T1, T2>(Action<T1, T2> func, T1 arg1, T2 arg2)
        {
            try
            {
                func(arg1, arg2);
                return Empty;
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        private static Task FromMethod(Func<Task> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        private static Task<TResult> FromMethod<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        private static Task<TResult> FromMethod<TResult>(Func<TResult> func)
        {
            try
            {
                return FromResult(func());
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        private static Task FromMethod<T1>(Func<T1, Task> func, T1 arg)
        {
            try
            {
                return func(arg);
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        private static Task FromMethod<T1, T2>(Func<T1, T2, Task> func, T1 arg1, T2 arg2)
        {
            try
            {
                return func(arg1, arg2);
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        private static Task<TResult> FromMethod<T1, TResult>(Func<T1, Task<TResult>> func, T1 arg)
        {
            try
            {
                return func(arg);
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        private static Task<TResult> FromMethod<T1, TResult>(Func<T1, TResult> func, T1 arg)
        {
            try
            {
                return FromResult(func(arg));
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        private static Task<TResult> FromMethod<T1, T2, TResult>(Func<T1, T2, Task<TResult>> func, T1 arg1, T2 arg2)
        {
            try
            {
                return func(arg1, arg2);
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        private static Task<TResult> FromMethod<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            try
            {
                return FromResult(func(arg1, arg2));
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        private static Task<T> FromResult<T>(T value)
        {
            TaskCompletionSource<T> completionSource = new TaskCompletionSource<T>();
            completionSource.SetResult(value);
            return completionSource.Task;
        }

        private static Task FromError(Exception e)
        {
            return FromError<object>(e);
        }

        private static Task<T> FromError<T>(Exception e)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            SetUnwrappedException(tcs, e);
            return tcs.Task;
        }

        private static void SetUnwrappedException<T>(this TaskCompletionSource<T> tcs, Exception e)
        {
            AggregateException aggregateException = e as AggregateException;
            if (aggregateException != null)
                tcs.SetException(aggregateException.InnerExceptions);
            else
                tcs.SetException(e);
        }

        private static bool TrySetUnwrappedException<T>(this TaskCompletionSource<T> tcs, Exception e)
        {
            AggregateException aggregateException = e as AggregateException;
            if (aggregateException != null)
                return tcs.TrySetException(aggregateException.InnerExceptions);
            return tcs.TrySetException(e);
        }
//
//        private static Task Canceled()
//        {
//            TaskCompletionSource<object> completionSource = new TaskCompletionSource<object>();
//            completionSource.SetCanceled();
//            return (Task)completionSource.Task;
//        }

        private static Task<T> Canceled<T>()
        {
            TaskCompletionSource<T> completionSource = new TaskCompletionSource<T>();
            completionSource.SetCanceled();
            return completionSource.Task;
        }

        private static Task ContinueWithPreservedCulture(this Task task, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
        {
            CultureInfo preservedCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo preservedUICulture = Thread.CurrentThread.CurrentUICulture;
            return task.ContinueWith(t =>
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                try
                {
                    Thread.CurrentThread.CurrentCulture = preservedCulture;
                    Thread.CurrentThread.CurrentUICulture = preservedUICulture;
                    continuationAction(t);
                }
                finally
                {
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                }
            }, continuationOptions);
        }

        private static Task ContinueWithPreservedCulture<T>(this Task<T> task, Action<Task<T>> continuationAction, TaskContinuationOptions continuationOptions)
        {
            CultureInfo preservedCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo preservedUICulture = Thread.CurrentThread.CurrentUICulture;
            return task.ContinueWith(t =>
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                try
                {
                    Thread.CurrentThread.CurrentCulture = preservedCulture;
                    Thread.CurrentThread.CurrentUICulture = preservedUICulture;
                    continuationAction(t);
                }
                finally
                {
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                }
            }, continuationOptions);
        }

        private static Task<TResult> ContinueWithPreservedCulture<T, TResult>(this Task<T> task, Func<Task<T>, TResult> continuationAction, TaskContinuationOptions continuationOptions)
        {
            CultureInfo preservedCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo preservedUICulture = Thread.CurrentThread.CurrentUICulture;
            return task.ContinueWith((t =>
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                try
                {
                    Thread.CurrentThread.CurrentCulture = preservedCulture;
                    Thread.CurrentThread.CurrentUICulture = preservedUICulture;
                    return continuationAction(t);
                }
                finally
                {
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                }
            }), continuationOptions);
        }

        private static Task ContinueWithPreservedCulture(this Task task, Action<Task> continuationAction)
        {
            return ContinueWithPreservedCulture(task, continuationAction, TaskContinuationOptions.None);
        }

        private static Task ContinueWithPreservedCulture<T>(this Task<T> task, Action<Task<T>> continuationAction)
        {
            return ContinueWithPreservedCulture(task, continuationAction, TaskContinuationOptions.None);
        }

        internal static Task<TResult> ContinueWithPreservedCulture<T, TResult>(this Task<T> task, Func<Task<T>, TResult> continuationAction)
        {
            return ContinueWithPreservedCulture(task, continuationAction, TaskContinuationOptions.None);
        }

        private static Task RunTask(Task task, Action successor)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            ContinueWithPreservedCulture(task, t =>
            {
                if (t.IsFaulted)
                    SetUnwrappedException(tcs, t.Exception);
                else if (t.IsCanceled)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    try
                    {
                        successor();
                        tcs.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        SetUnwrappedException(tcs, ex);
                    }
                }
            });
            return tcs.Task;
        }

        private static Task RunTaskSynchronously(Task task, Action<object> next, object state, bool onlyOnSuccess = true)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            ContinueWithPreservedCulture(task, t =>
            {
                try
                {
                    if (t.IsFaulted)
                    {
                        if (!onlyOnSuccess)
                            next(state);
                        SetUnwrappedException(tcs, t.Exception);
                    }
                    else if (t.IsCanceled)
                    {
                        if (!onlyOnSuccess)
                            next(state);
                        tcs.SetCanceled();
                    }
                    else
                    {
                        next(state);
                        tcs.SetResult(null);
                    }
                }
                catch (Exception ex)
                {
                    SetUnwrappedException(tcs, ex);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        private static class TaskRunners<T, TResult>
        {
            internal static Task RunTask(Task<T> task, Action<T> successor)
            {
                TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
                ContinueWithPreservedCulture(task, t =>
                {
                    if (t.IsFaulted)
                        SetUnwrappedException(tcs, t.Exception);
                    else if (t.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        try
                        {
                            successor(t.Result);
                            tcs.SetResult(null);
                        }
                        catch (Exception ex)
                        {
                            SetUnwrappedException(tcs, ex);
                        }
                    }
                });
                return tcs.Task;
            }

            internal static Task<TResult> RunTask(Task task, Func<TResult> successor)
            {
                TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
                ContinueWithPreservedCulture(task, t =>
                {
                    if (t.IsFaulted)
                        SetUnwrappedException(tcs, t.Exception);
                    else if (t.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        try
                        {
                            tcs.SetResult(successor());
                        }
                        catch (Exception ex)
                        {
                            SetUnwrappedException(tcs, ex);
                        }
                    }
                });
                return tcs.Task;
            }

            internal static Task<TResult> RunTask(Task<T> task, Func<Task<T>, TResult> successor)
            {
                TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
                ContinueWithPreservedCulture(task, t =>
                {
                    if (task.IsFaulted)
                        SetUnwrappedException(tcs, t.Exception);
                    else if (task.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        try
                        {
                            tcs.SetResult(successor(t));
                        }
                        catch (Exception ex)
                        {
                            SetUnwrappedException(tcs, ex);
                        }
                    }
                });
                return tcs.Task;
            }
        }

        private static class GenericDelegates<T, TResult, T1, T2>
        {
            internal static Task ThenWithArgs(Task task, Action<T1> successor, T1 arg1)
            {
                return RunTask(task, () => successor(arg1));
            }

            internal static Task ThenWithArgs(Task task, Action<T1, T2> successor, T1 arg1, T2 arg2)
            {
                return RunTask(task, () => successor(arg1, arg2));
            }

//            internal static Task<TResult> ThenWithArgs(Task task, Func<T1, TResult> successor, T1 arg1)
//            {
//                return TaskRunners<object, TResult>.RunTask(task, (Func<TResult>)(() => successor(arg1)));
//            }
//
//            internal static Task<TResult> ThenWithArgs(Task task, Func<T1, T2, TResult> successor, T1 arg1, T2 arg2)
//            {
//                return TaskRunners<object, TResult>.RunTask(task, (Func<TResult>)(() => successor(arg1, arg2)));
//            }

            internal static Task<TResult> ThenWithArgs(Task<T> task, Func<T, T1, TResult> successor, T1 arg1)
            {
                return TaskRunners<T, TResult>.RunTask(task, t => successor(t.Result, arg1));
            }

            internal static Task<Task> ThenWithArgs(Task task, Func<T1, Task> successor, T1 arg1)
            {
                return TaskRunners<object, Task>.RunTask(task, () => successor(arg1));
            }

            internal static Task<Task> ThenWithArgs(Task task, Func<T1, T2, Task> successor, T1 arg1, T2 arg2)
            {
                return TaskRunners<object, Task>.RunTask(task, () => successor(arg1, arg2));
            }
//
//            internal static Task<Task<TResult>> ThenWithArgs(Task<T> task, Func<T, T1, Task<TResult>> successor, T1 arg1)
//            {
//                return TaskRunners<T, Task<TResult>>.RunTask(task, (Func<Task<T>, Task<TResult>>)(t => successor(t.Result, arg1)));
//            }

            internal static Task<Task<T>> ThenWithArgs(Task<T> task, Func<Task<T>, T1, Task<T>> successor, T1 arg1)
            {
                return TaskRunners<T, Task<T>>.RunTask(task, t => successor(t, arg1));
            }
        }

        private static class TaskCache<T>
        {
            public static readonly Task<T> Empty = MakeTask(default(T));
        }
    }
}
