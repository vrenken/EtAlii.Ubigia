// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class LapaSchemaParserComponentTestFactory
    {
        public async Task<(TFirst, TSecond)> Create<TFirst, TSecond>(TraversalUnitTestContext testContext)
        {
            var container = new Container();

            var options = await new FunctionalOptions(testContext.ClientConfiguration)
                .UseLapaParsing()
                .UseDataConnectionToNewSpace(testContext, true)
                .ConfigureAwait(false);
            new LapaParserExtension(options).Initialize(container);

            return (container.GetInstance<TFirst>(), container.GetInstance<TSecond>());
        }

        public async Task<T> Create<T>(TraversalUnitTestContext testContext)
        {
            var container = new Container();

            var options = await new FunctionalOptions(testContext.ClientConfiguration)
                .UseLapaParsing()
                .UseDataConnectionToNewSpace(testContext, true)
                .ConfigureAwait(false);
            new LapaParserExtension(options).Initialize(container);

            return container.GetInstance<T>();
        }
    }
}
