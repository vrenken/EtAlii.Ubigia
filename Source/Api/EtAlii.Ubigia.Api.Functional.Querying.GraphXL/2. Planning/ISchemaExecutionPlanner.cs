namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface ISchemaExecutionPlanner
    {
        FragmentExecutionPlan[] Plan(Schema schema);
    }
}
