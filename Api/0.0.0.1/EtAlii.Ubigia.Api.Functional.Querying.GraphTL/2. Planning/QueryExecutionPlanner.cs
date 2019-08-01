namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.Generic;

    internal class QueryExecutionPlanner : IQueryExecutionPlanner
    {
        private readonly IStructureQueryProcessor _structureQueryProcessor;
        private readonly IStructureMutationProcessor _structureMutationProcessor;
        private readonly IValueQueryProcessor _valueQueryProcessor;
        private readonly IValueMutationProcessor _valueMutationProcessor;

        public QueryExecutionPlanner(IStructureQueryProcessor structureQueryProcessor,
            IStructureMutationProcessor structureMutationProcessor,
            IValueQueryProcessor valueQueryProcessor,
            IValueMutationProcessor valueMutationProcessor)
        {
            _structureQueryProcessor = structureQueryProcessor;
            _structureMutationProcessor = structureMutationProcessor;
            _valueQueryProcessor = valueQueryProcessor;
            _valueMutationProcessor = valueMutationProcessor;
        }

        public FragmentExecutionPlan[] Plan(Query query)
        {
            var executionPlanQueue = new List<FragmentExecutionPlan>();
            
            var fragment = query.Structure;
            GetPlansForFragment(fragment, executionPlanQueue);

            return executionPlanQueue.ToArray();
        }

        private FragmentMetadata GetPlansForFragment(Fragment fragment, List<FragmentExecutionPlan> executionPlanQueue)
        {
            var childMetaDatas = new List<FragmentMetadata>();

            FragmentExecutionPlan executionPlan;
            
            switch (fragment)
            {
                case ValueQuery valueQuery:
                    executionPlan = new FragmentExecutionPlan<ValueQuery>(valueQuery, _valueQueryProcessor);
                    executionPlanQueue.Add(executionPlan);
                    break;
                
                case StructureQuery structureQuery:
                    executionPlan = new FragmentExecutionPlan<StructureQuery>(structureQuery, _structureQueryProcessor);
                    executionPlanQueue.Add(executionPlan);
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureQuery.Values);
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureQuery.Children);
                    break;
                
                case ValueMutation valueMutation:
                    executionPlan = new FragmentExecutionPlan<ValueMutation>(valueMutation, _valueMutationProcessor);
                    executionPlanQueue.Add(executionPlan);
                    break;
                
                case StructureMutation structureMutation:
                    executionPlan = new FragmentExecutionPlan<StructureMutation>(structureMutation, _structureMutationProcessor);
                    executionPlanQueue.Add(executionPlan);
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureMutation.Values);
                    break;
                
                default:
                    var typeAsString = fragment != null ? fragment.GetType().ToString() : "NULL";
                    throw new QueryProcessingException($"Unable to plan the specified fragment type: {typeAsString}");
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