// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
	public class InfrastructureCumulatedTests
	{
        /// <summary>
        /// The test below is setup to troubleshoot multi-unit test failure situations. There are
        /// situations where unit tests on themselves run properly, but when run sequentially across class contexts fail.
        /// The current setup takes three methods from two different classes and runs them within one single xUnit unit test method,
        /// </summary>
		[Fact]
		public async Task InfrastructureCumulatedTests_Run_Sequence_Of_Tests_Including_Contexts()
        {
            var testContext = new InfrastructureUnitTestContext();
            await testContext.InitializeAsync().ConfigureAwait(false);
            var firstTestClass = new InfrastructureAuthenticationTests(testContext);

            await firstTestClass.Infrastructure_Get_Authentication_Url_Admin_Admin().ConfigureAwait(false);

            await testContext.DisposeAsync().ConfigureAwait(false);
            testContext = new InfrastructureUnitTestContext();
            await testContext.InitializeAsync().ConfigureAwait(false);
            var secondTestClass = new InfrastructureStorageTests(testContext);

            await secondTestClass.Infrastructure_Get_Storage_Local_Admin_Admin_With_Authentication().ConfigureAwait(false);

            await testContext.DisposeAsync().ConfigureAwait(false);
            testContext = new InfrastructureUnitTestContext();
            await testContext.InitializeAsync().ConfigureAwait(false);
            secondTestClass = new InfrastructureStorageTests(testContext);

            await secondTestClass.Infrastructure_Get_Storage_Local_Admin_System_With_Authentication().ConfigureAwait(false);

            await testContext.DisposeAsync().ConfigureAwait(false);
        }
    }
}
