namespace EtAlii.Servus.Tests
{
    using Xunit;
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static partial class ExceptionAssert
    {
        public static async Task ThrowsObservable<TException, TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions) 
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
                AssertExceptionType<TException>(ex, inheritOptions);
                AssertExceptionMessage(ex, expectedMessage, messageOptions);
                return;
            }

            Assert.False(typeof(TException) == new Exception().GetType(), "Expected exception but no exception was thrown.");
            throw new InvalidOperationException(string.Format("Expected exception of type {0} but no exception was thrown.", typeof(TException)));
        }

        #region Overloaded methods

        public static async Task ThrowsObservable<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) 
            where TException : Exception
        {
            await ThrowsObservable<TException, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task ThrowsObservable<TException, TObservable>(IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) 
            where TException : Exception
        {
            await ThrowsObservable<TException, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task ThrowsObservable<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) 
            where TException : Exception
        {
            await ThrowsObservable<TException, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsObservable<TException, TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) 
            where TException : Exception
        {
            await ThrowsObservable<TException, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsObservable<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) 
            where TException : Exception
        {
            await ThrowsObservable<TException, TObservable>(observable, expectedMessage, options, inheritOptions);
        }

        public static async Task ThrowsObservable<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsObservable<Exception, TObservable>(observable, expectedMessage, options, inheritOptions);
        }

        public static async Task ThrowsObservable<TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsObservable<Exception, TObservable>(observable, expectedMessage, options, inheritOptions);
        }

        public static async Task ThrowsObservable<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsObservable<Exception, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsObservable<TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsObservable<Exception, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsObservable<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsObservable<Exception, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task ThrowsObservable<TObservable>(IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsObservable<Exception, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        #endregion
    }
}
