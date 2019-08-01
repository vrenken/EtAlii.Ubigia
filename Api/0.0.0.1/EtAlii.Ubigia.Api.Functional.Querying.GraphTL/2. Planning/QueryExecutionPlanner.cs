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

        public void Plan(Query query, out FragmentMetadata rootFragmentMetadata, out FragmentExecutionPlan[] fragmentExecutionPlans)
        {
            var executionPlanQueue = new List<FragmentExecutionPlan>();
            
            var fragment = query.Structure;
            GetPlansForFragment(fragment, null, out rootFragmentMetadata, executionPlanQueue);

            fragmentExecutionPlans = executionPlanQueue.ToArray();
        }

        private void GetPlansForFragment(Fragment fragment, FragmentMetadata parent, out FragmentMetadata metadata, List<FragmentExecutionPlan> executionPlanQueue)
        {
            // TODO: Remove to end and include the children. Remove the static FragmentMetadata.SetChildFragments method. 
            metadata = new FragmentMetadata(fragment, parent);

            switch (fragment)
            {
                case ValueQuery valueQuery:
                    executionPlanQueue.Add(new FragmentExecutionPlan<ValueQuery>(valueQuery, metadata, _valueQueryProcessor));
                    break;
                
                case StructureQuery structureQuery: 
                    executionPlanQueue.Add(new FragmentExecutionPlan<StructureQuery>(structureQuery, metadata, _structureQueryProcessor));
                    var subQueryValueMetaDatas = GetPlansForSubFragments(structureQuery.Values, executionPlanQueue, metadata);
                    metadata.AddChildFragments(subQueryValueMetaDatas);
                    var subQueryStructureMetaDatas = GetPlansForSubFragments(structureQuery.Children, executionPlanQueue, metadata);
                    metadata.AddChildFragments(subQueryStructureMetaDatas);
                    break;
                
                case ValueMutation valueMutation:
                    executionPlanQueue.Add(new FragmentExecutionPlan<ValueMutation>(valueMutation, metadata, _valueMutationProcessor));
                    break;
                
                case StructureMutation structureMutation:
                    executionPlanQueue.Add(new FragmentExecutionPlan<StructureMutation>(structureMutation, metadata, _structureMutationProcessor));
                    var subMutationValueMetaDatas = GetPlansForSubFragments(structureMutation.Values, executionPlanQueue, metadata);
                    metadata.AddChildFragments(subMutationValueMetaDatas);
                    break;
            }
        }

        private FragmentMetadata[] GetPlansForSubFragments<TFragment>(TFragment[] fragments, List<FragmentExecutionPlan> executionPlanQueue, FragmentMetadata metadata)
            where TFragment: Fragment
        {
            var result = new List<FragmentMetadata>();
            
            foreach (var fragment in fragments)
            {
                GetPlansForFragment(fragment, metadata, out var childFragmentMetadata, executionPlanQueue);
                
                result.Add(childFragmentMetadata);
            }

            return result.ToArray();
        }
    }
}