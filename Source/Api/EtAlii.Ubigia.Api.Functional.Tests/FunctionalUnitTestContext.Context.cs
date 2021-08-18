// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public partial class FunctionalUnitTestContext
    {
        public ISchemaProcessor CreateSchemaProcessor(IDataConnection dataConnection)
        {
            var options = new FunctionalOptions(ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .Use(dataConnection);
            return CreateSchemaProcessor(options);
        }

        public ISchemaProcessor CreateSchemaProcessor(IFunctionalOptions options)
        {
            var container = new Container();

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return  container.GetInstance<ISchemaProcessor>();
        }

        public ISchemaParser CreateSchemaParser()
        {
            var options = new FunctionalOptions(ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics();

            var container = new Container();

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return  container.GetInstance<ISchemaParser>();
        }
    }
}
