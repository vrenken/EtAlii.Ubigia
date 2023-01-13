namespace EtAlii.Ubigia.Tests;

using System;
using System.Threading.Tasks;
using Xunit.Sdk;

public static class ActionAssert
{
    public static RaisedAction<T> Raises<T>(
        Action<Action<T>> attach,
        Action<Action<T>> detach,
        Action testCode)
    {
        var raisedAction = RaisesInternal(attach, detach, testCode);
        if (raisedAction == null)
            throw new RaisesException(typeof (T));
        return raisedAction.Argument.GetType() == typeof (T) ? raisedAction : throw new RaisesException(typeof (T), raisedAction.Argument.GetType());
    }

    private static RaisedAction<T> RaisesInternal<T>(
        Action<Action<T>> attach,
        Action<Action<T>> detach,
        Action testCode)
    {
        var raisedAction = (RaisedAction<T>) null;
        var handler = (Action<T>) (args => raisedAction = new RaisedAction<T>(args));
        attach(handler);
        testCode();
        detach(handler);
        return raisedAction;
    }

    public static async Task<RaisedAction<T>> RaisesAsync<T>(
        Action<Action<T>> attach,
        Action<Action<T>> detach,
        Func<Task> testCode)
    {
        var raisedAction = await RaisesAsyncInternal(attach, detach, testCode).ConfigureAwait(false);
        if (raisedAction == null)
        {
            throw new RaisesException(typeof (T));
        }
        return raisedAction.Argument.GetType() == typeof (T)
            ? raisedAction
            : throw new RaisesException(typeof (T), raisedAction.Argument.GetType());
    }

    private static async Task<RaisedAction<T>> RaisesAsyncInternal<T>(
        Action<Action<T>> attach,
        Action<Action<T>> detach,
        Func<Task> testCode)
    {
        var raisedEvent = (RaisedAction<T>) null;
        var handler = (Action<T>) (args => raisedEvent = new RaisedAction<T>(args));
        attach(handler);
        await testCode().ConfigureAwait(false);
        detach(handler);
        return raisedEvent;
    }


    /// <summary>Represents a raised event after the fact.</summary>
    /// <typeparam name="T">The type of the event arguments.</typeparam>
    public class RaisedAction<T>
    {
        /// <summary>The event arguments.</summary>
        public T Argument { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="T:Xunit.Assert.RaisedEvent`1" /> class.
        /// </summary>
        /// <param name="args">The event arguments</param>
        public RaisedAction(T args)
        {
            Argument = args;
        }
    }
}
