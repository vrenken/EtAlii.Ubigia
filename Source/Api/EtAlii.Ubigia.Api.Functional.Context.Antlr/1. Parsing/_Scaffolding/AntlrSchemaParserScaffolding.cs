// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrSchemaParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IContextValidator, ContextValidator>();
            container.Register<ISchemaParser, AntlrSchemaParser>();
        }
    }
}
