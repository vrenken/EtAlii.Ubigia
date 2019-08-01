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
            
            switch (fragment)
            {
                case ValueQuery valueQuery:
                    executionPlanQueue.Add(new FragmentExecutionPlan<ValueQuery>(valueQuery, _valueQueryProcessor));
                    break;
                
                case StructureQuery structureQuery: 
                    executionPlanQueue.Add(new FragmentExecutionPlan<StructureQuery>(structureQuery, _structureQueryProcessor));
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureQuery.Values);
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureQuery.Children);
                    break;
                
                case ValueMutation valueMutation:
                    executionPlanQueue.Add(new FragmentExecutionPlan<ValueMutation>(valueMutation, _valueMutationProcessor));
                    break;
                
                case StructureMutation structureMutation:
                    executionPlanQueue.Add(new FragmentExecutionPlan<StructureMutation>(structureMutation, _structureMutationProcessor));
                    GetPlansForChildFragments(executionPlanQueue, childMetaDatas, structureMutation.Values);
                    break;
            }

            var metadata = new FragmentMetadata(fragment, childMetaDatas.ToArray());
            fragment.SetMetaData(metadata);

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