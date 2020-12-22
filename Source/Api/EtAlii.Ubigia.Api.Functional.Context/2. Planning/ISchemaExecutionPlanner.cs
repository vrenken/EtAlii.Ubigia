namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal interface ISchemaExecutionPlanner
    {
        FragmentExecutionPlan[] Plan(Schema schema);
    }
}
