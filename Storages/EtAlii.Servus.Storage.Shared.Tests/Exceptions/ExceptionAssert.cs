﻿namespace EtAlii.Servus.Tests
{
    using Xunit;
    using System;
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static partial class ExceptionAssert
    {
        public static void Throws<T>(Action task, string expectedMessage, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions) where T : Exception
        {
            try
            {
                task();
            }
            catch (Exception ex)
            {
                AssertExceptionType<T>(ex, inheritOptions);
                AssertExceptionMessage(ex, expectedMessage, messageOptions);
                return;
            }

            Assert.False(typeof(T) == new Exception().GetType(), "Expected exception but no exception was thrown.");
            throw new InvalidOperationException(string.Format("Expected exception of type {0} but no exception was thrown.", typeof(T)));
        }

        #region Overloaded methods

        public static void Throws<T>(this IAssertion assertion, Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            Throws<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static void Throws<T>(Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            Throws<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static void Throws<T>(this IAssertion assertion, Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            Throws<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static void Throws<T>(Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            Throws<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static void Throws<T>(this IAssertion assertion, Action task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            Throws<T>(task, expectedMessage, options, inheritOptions);
        }

        public static void Throws(this IAssertion assertion, Action task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            Throws<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static void Throws(Action task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            Throws<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static void Throws(this IAssertion assertion, Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            Throws<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static void Throws(Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            Throws<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static void Throws(this IAssertion assertion, Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            Throws<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static void Throws(Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            Throws<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        #endregion

        private static void AssertExceptionNotInherited<T>(Exception ex)
        {
            Assert.False(ex.GetType().IsSubclassOf(typeof(T)));
        }

        private static void AssertExceptionType<T>(Exception ex, ExceptionInheritanceOptions options)
        {
            Assert.IsType<T>(ex);//, "Expected exception type failed.");
            switch (options)
            {
                case ExceptionInheritanceOptions.Exact:
                    AssertExceptionNotInherited<T>(ex);
                    break;
                case ExceptionInheritanceOptions.Inherits:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("options");

            }
        }

        private static void AssertExceptionMessage(Exception ex, string expectedMessage, ExceptionMessageCompareOptions options)
        {
            if (!string.IsNullOrEmpty(expectedMessage))
            {
                switch (options)
                {
                    case ExceptionMessageCompareOptions.Exact:
                        Assert.Equal(ex.Message.ToUpper(), expectedMessage.ToUpper());//, "Expected exception message failed.");
                        break;
                    case ExceptionMessageCompareOptions.Contains:
                        Assert.True(ex.Message.Contains(expectedMessage), string.Format("Expected exception message does not contain <{0}>.", expectedMessage));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("options");
                }

            }
        }
    }
}
