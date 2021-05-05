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
