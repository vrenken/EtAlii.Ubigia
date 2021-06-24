// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaExecutionPlanningScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ISchemaExecutionPlanner, SchemaExecutionPlanner>();
        }
    }
}
