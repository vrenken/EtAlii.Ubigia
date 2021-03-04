namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SchemaProcessor : ISchemaProcessor
    {
        private readonly ISchemaExecutionPlanner _schemaExecutionPlanner;

        public SchemaProcessor(ISchemaExecutionPlanner schemaExecutionPlanner)
        {
            _schemaExecutionPlanner = schemaExecutionPlanner;
        }

        public async IAsyncEnumerable<Structure> Process(Schema schema)
        {
            // We need to create execution plans for all of the sequences.
            var executionPlans = _schemaExecutionPlanner.Plan(schema);
            var rootResult = executionPlans?.FirstOrDefault()?.ResultSink ?? new ExecutionPlanResultSink(null, Array.Empty<ExecutionPlanResultSink>());

            try
            {
                var executionScope = new SchemaExecutionScope();

                foreach (var executionPlan in executionPlans!)
                {
                    await executionPlan
                        .Execute(executionScope)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                while (e is AggregateException aggregateException)
                {
                    if (aggregateException.InnerException != null)
                    {
                        e = aggregateException.InnerException;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            foreach (var item in rootResult.Items)
            {
                yield return item;
            }
        }
    }
}
