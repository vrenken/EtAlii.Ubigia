namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;

    internal class SchemaExecutionPlanner : ISchemaExecutionPlanner
    {
        private readonly IQueryStructureProcessor _queryStructureProcessor;
        private readonly IMutationStructureProcessor _mutationStructureProcessor;
        private readonly IQueryValueProcessor _queryValueProcessor;
        private readonly IMutationValueProcessor _mutationValueProcessor;

        public SchemaExecutionPlanner(IQueryStructureProcessor queryStructureProcessor,
            IMutationStructureProcessor mutationStructureProcessor,
            IQueryValueProcessor queryValueProcessor,
            IMutationValueProcessor mutationValueProcessor)
        {
            _queryStructureProcessor = queryStructureProcessor;
            _mutationStructureProcessor = mutationStructureProcessor;
            _queryValueProcessor = queryValueProcessor;
            _mutationValueProcessor = mutationValueProcessor;
        }

        public FragmentExecutionPlan[] Plan(Schema schema)
        {
            var executionPlanQueue = new List<FragmentExecutionPlan>();

            var fragment = schema.Structure;
            GetPlansForFragment(fragment, executionPlanQueue);

            return executionPlanQueue.ToArray();
        }

        private FragmentMetadata GetPlansForFragment(Fragment fragment, List<FragmentExecutionPlan> executionPlanQueue)
        {
            var childMetaDatas = new List<FragmentMetadata>();

            FragmentExecutionPlan executionPlan;

            switch (fragment)
            {
                case ValueFragment valueQuery when valueQuery.Type == FragmentType.Query:
                    executionPlan = new FragmentExecutionPlan<ValueFragment>(valueQuery, _queryValueProcessor);
                    executionPlanQueue.Add(executionPlan);
                    break;

                case StructureFragment structureQuery when structureQuery.Type == FragmentType.Query:
                    executionPlan = new FragmentExecutionPlan<StructureFragment>(structureQuery, _queryStructureProcessor);
                    executionPlanQueue.Add(executionPlan);
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureQuery.Values);
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureQuery.Children);
                    break;

                case ValueFragment valueMutation when valueMutation.Type == FragmentType.Mutation:
                    executionPlan = new FragmentExecutionPlan<ValueFragment>(valueMutation, _mutationValueProcessor);
                    executionPlanQueue.Add(executionPlan);
                    break;

                case StructureFragment structureMutation when structureMutation.Type == FragmentType.Mutation:
                    executionPlan = new FragmentExecutionPlan<StructureFragment>(structureMutation, _mutationStructureProcessor);
                    executionPlanQueue.Add(executionPlan);
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureMutation.Values);
                    break;

                default:
                    var typeAsString = fragment != null ? fragment.GetType().ToString() : "NULL";
                    throw new SchemaProcessingException($"Unable to plan the specified fragment type: {typeAsString}");
            }

            var metadata = new FragmentMetadata(fragment, childMetaDatas.ToArray());
            executionPlan.SetMetaData(metadata);

            return metadata;

        }

        private void GetPlansForChildFragments<TFragment>(
            List<FragmentExecutionPlan> executionPlanQueue,
            List<FragmentMetadata> childMetaDatas,
            TFragment[] fragments)
            where TFragment: Fragment
        {
            foreach (var fragment in fragments)
            {
                var childMetadata = GetPlansForFragment(fragment, executionPlanQueue);
                childMetaDatas.Add(childMetadata);
            }
        }
    }
}
