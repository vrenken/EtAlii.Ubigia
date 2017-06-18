namespace EtAlii.Ubigia.Tests
{
    using Xunit;
    using System;
    using System.Threading.Tasks;

    public static partial class ExceptionAssert
    {
        public static async Task ThrowsAsync<T>(Task task, string expectedMessage, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions) where T : Exception
        {
            try
            {
                await task;
            }
            //catch (AggregateException ex)
            //{
            //    AssertExceptionType<T>(ex., inheritOptions);
            //    AssertExceptionMessage(ex, expectedMessage, messageOptions);
            //    return;
            //}
            catch (Exception ex)
            {
                AssertExceptionType<T>(ex, inheritOptions);
                AssertExceptionMessage(ex, expectedMessage, messageOptions);
                return;
            }

            Assert.False(typeof(T) == new Exception().GetType(), "Expected exception but no exception was thrown.");
            throw new InvalidOperationException($"Expected exception of type {typeof(T)} but no exception was thrown.");
        }

        #region Overloaded methods

        public static async Task ThrowsAsync<T>(this IAssertion assertion, Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            await ThrowsAsync<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task ThrowsAsync<T>(Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            await ThrowsAsync<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task ThrowsAsync<T>(this IAssertion assertion, Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            await ThrowsAsync<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsAsync<T>(Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            await ThrowsAsync<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsAsync<T>(this IAssertion assertion, Task task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            await ThrowsAsync<T>(task, expectedMessage, options, inheritOptions);
        }

        public static async Task ThrowsAsync(this IAssertion assertion, Task task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsAsync<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static async Task ThrowsAsync(Task task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsAsync<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static async Task ThrowsAsync(this IAssertion assertion, Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsAsync<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsAsync(Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsAsync<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static async Task ThrowsAsync(this IAssertion assertion, Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsAsync<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static async Task ThrowsAsync(Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            await ThrowsAsync<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        #endregion
    }
}
