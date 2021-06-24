// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

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
            //[
            //    AssertExceptionType<T>(ex., inheritOptions)
            //    AssertExceptionMessage(ex, expectedMessage, messageOptions)
            //    return
            //]
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

        #region Overloaded methods

        //public static async Task Throws<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //    where TException : Exception
        //[
        //    await Throws<TException, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions)
        //]
        //public static async Task Throws<TException, TObservable>(IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //    where TException : Exception
        //[
        //    await Throws<TException, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions)
        //]
        //public static async Task Throws<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //    where TException : Exception
        //[
        //    await Throws<TException, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions)
        //]
        //public static async Task Throws<TException, TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //    where TException : Exception
        //[
        //    await Throws<TException, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions)
        //]
        //public static async Task Throws<TException, TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //    where TException : Exception
        //[
        //    await Throws<TException, TObservable>(observable, expectedMessage, options, inheritOptions)
        //]
        //public static async Task Throws<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //[
        //    await Throws<Exception, TObservable>(observable, expectedMessage, options, inheritOptions)
        //]
        //public static async Task Throws<TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //[
        //    await Throws<Exception, TObservable>(observable, expectedMessage, options, inheritOptions)
        //]
        //public static async Task Throws<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //[
        //    await Throws<Exception, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions)
        //]
        //public static async Task Throws<TObservable>(IObservable<TObservable> observable, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //[
        //    await Throws<Exception, TObservable>(observable, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions)
        //]
        //public static async Task Throws<TObservable>(this IAssertion assertion, IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //[
        //    await Throws<Exception, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions)
        //]
        //public static async Task Throws<TObservable>(IObservable<TObservable> observable, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        //[
        //    await Throws<Exception, TObservable>(observable, null, ExceptionMessageCompareOptions.None, inheritOptions)
        //]
        #endregion
    }
}
