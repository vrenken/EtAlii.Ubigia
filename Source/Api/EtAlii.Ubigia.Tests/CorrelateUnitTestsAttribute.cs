// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.Reflection;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Threading;
    using Serilog;
    using Xunit.Sdk;

    /// <summary>
    /// This attribute ensures that each unit test gets a unique correlation ID assigned in the structured logging output.
    /// </summary>
    public class CorrelateUnitTestsAttribute : BeforeAfterTestAttribute
    {
        private readonly ILogger _logger = Log.ForContext<CorrelateUnitTestsAttribute>();
        private readonly IContextCorrelator _contextCorrelator = new ContextCorrelator();
        private IDisposable _correlation;

        public override void Before(MethodInfo methodUnderTest)
        {
            _correlation = _contextCorrelator.BeginLoggingCorrelationScope(Correlation.UnitTestId, ShortGuid.New());
            _logger.Verbose("Correlating Unit test: {ClassName} {MethodName}", methodUnderTest.DeclaringType!.FullName, methodUnderTest.Name);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            _logger.Verbose("Ending correlation for Unit test: {ClassName} {MethodName}", methodUnderTest.DeclaringType!.FullName, methodUnderTest.Name);
            _correlation?.Dispose();
            _correlation = null;
        }
    }
}
