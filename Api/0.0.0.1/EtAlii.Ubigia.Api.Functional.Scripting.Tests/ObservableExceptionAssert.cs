namespace EtAlii.Ubigia.Tests
{
    using Xunit;
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class ObservableExceptionAssert
    {
        public static async Task Throws<TException, TObservable>(IObservable<TObservable> observable, string expectedMessage = null)//, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions)
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
                Assert.IsAssignableFrom<TException>(ex);
                if(expectedMessage != null)
                {
                    Assert.Equal(ex.Message.ToUpper(), expectedMessage.ToUpper());
                }
                return;
            }

            Assert.False(typeof(TException) == new Exception().GetType(), "Expected exception but no exception was thrown.");
            throw new InvalidOperationException(
                $"Expected exception of type {typeof(TException)} but no exception was thrown.");
        }
    }
}
