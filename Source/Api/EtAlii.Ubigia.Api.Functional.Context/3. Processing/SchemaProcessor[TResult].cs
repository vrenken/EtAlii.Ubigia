namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal partial class SchemaProcessor
    {
        // protected SchemaProcessor(ISchemaExecutionPlanner schemaExecutionPlanner)
        // {
        //     _schemaExecutionPlanner = schemaExecutionPlanner;
        // }
        //

        public async Task<TResult> ProcessSingle<TResult>(Schema schema)
        {
            return await ProcessMultiple<TResult>(schema)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<TResult> ProcessMultiple<TResult>(Schema schema)
        {
            // We need to create execution plans for all of the sequences.
            var executionPlans = _schemaExecutionPlanner.Plan(schema);
            var rootMetadata = executionPlans?.FirstOrDefault()?.Metadata ?? new FragmentMetadata(null, Array.Empty<FragmentMetadata>());

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

            foreach (var item in rootMetadata.Items)
            {
                yield return (TResult)(object)item;
            }
        }
    }
}
