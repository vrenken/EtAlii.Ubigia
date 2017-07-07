namespace EtAlii.Ubigia.Tests
{
    using Xunit;
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class ObservableExceptionAssert
    {
        public static async Task Throws<TException, TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions)
            where TException : Exception
        {
            try
            {
                await observable.ToArray();
            }
            //catch (AggregateException ex)
            //{
            //    AssertExceptionType<T>(ex., inheritOptions);
            //    AssertExceptionMessage(ex, expectedMessage, messageOptions);
            //    return;
            //}
            catch (Exception ex)
            {
                ExceptionAssert.AssertExceptionType<TException>(ex, inheritOptions);
                ExceptionAssert.AssertExceptionMessage(ex, expectedMessage, messageOptions);
                return;
            }

            Assert.False(typeof(TException) == new Exception().GetType(), "Expected exception but no exception was thrown.");
            throw new InvalidOperationException(
                $"Expected exception of type {typeof(TException)} but no exception was thrown.");
        }

        #region Overloaded methods

        public static async Task Throws<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
            where TException : Exception
        {
            await Throws<TException, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task Throws<TException, TObservable>(IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
            where TException : Exception
        {
            await Throws<TException, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task Throws<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
            where TException : Exception
        {
            await Throws<TException, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task Throws<TException, TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
            where TException : Exception
        {
            await Throws<TException, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task Throws<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
            where TException : Exception
        {
            await Throws<TException, TObservable>(observable, expectedMessage, options, inheritOptions);
        }

        public static async Task Throws<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await Throws<Exception, TObservable>(observable, expectedMessage, options, inheritOptions);
        }

        public static async Task Throws<TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await Throws<Exception, TObservable>(observable, expectedMessage, options, inheritOptions);
        }

        public static async Task Throws<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await Throws<Exception, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task Throws<TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await Throws<Exception, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task Throws<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await Throws<Exception, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task Throws<TObservable>(IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await Throws<Exception, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        #endregion
    }
}
