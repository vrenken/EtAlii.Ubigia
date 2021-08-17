// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.MicroContainer;

    internal class TestSequencePartExecutionPlannerSelector
    {
        public static async Task<ISequencePartExecutionPlannerSelector> Create(ILogicalTestContext testContext)
        {
            var container = new Container();

            var options = await new FunctionalOptions(testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDataConnectionToNewSpace(testContext, true)
                .ConfigureAwait(false);

            foreach (var extension in ((IExtensible)options).Extensions)
            {
                extension.Initialize(container);
            }
            return container.GetInstance<ISequencePartExecutionPlannerSelector>();
        }
    }
}
