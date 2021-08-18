// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;

    internal class TestSequencePartExecutionPlannerSelector
    {
        public static async Task<ISequencePartExecutionPlannerSelector> Create(ILogicalTestContext testContext)
        {
            var options = await new FunctionalOptions(testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDataConnectionToNewSpace(testContext, true)
                .ConfigureAwait(false);

            return Factory.Create<ISequencePartExecutionPlannerSelector, IFunctionalExtension>(options);
        }
    }
}
