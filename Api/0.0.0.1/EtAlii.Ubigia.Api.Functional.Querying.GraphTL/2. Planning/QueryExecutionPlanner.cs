namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.Generic;

    internal class QueryExecutionPlanner : IQueryExecutionPlanner
    {
        private readonly IFragmentExecutionPlannerSelector _fragmentExecutionPlannerSelector;

        public QueryExecutionPlanner(IFragmentExecutionPlannerSelector fragmentExecutionPlannerSelector)
        {
            _fragmentExecutionPlannerSelector = fragmentExecutionPlannerSelector;
        }

        public void Plan(Query query, out FragmentMetadata rootFragmentMetadata, out FragmentExecutionPlan[] fragmentExecutionPlans)
        {
            var fragment = query.Structure;
            GetPlansForFragment(fragment, null, out rootFragmentMetadata, out fragmentExecutionPlans);
        }

        private void GetPlansForFragment(Fragment fragment, FragmentMetadata parent, out FragmentMetadata fragmentMetadata, out FragmentExecutionPlan[] fragmentExecutionPlans)
        {
            // TODO: Remove to end and include the children. Remove the static FragmentMetadata.SetChildFragments method. 
            fragmentMetadata = new FragmentMetadata(fragment, parent);

            var result = new List<FragmentExecutionPlan>();
            var fragmentExecutionPlanner = _fragmentExecutionPlannerSelector.Select(fragment);
            var plan = fragmentExecutionPlanner.Plan(fragment, fragmentMetadata);
            
            result.Add(plan);
            
            switch(fragment)
            {
                case StructureQuery structureQuery:
                    AddStructures(structureQuery.Values, result, fragmentMetadata);
                    AddStructures(structureQuery.Children, result, fragmentMetadata);
                    break;
                case StructureMutation structureMutation: 
                    AddStructures(structureMutation.Values, result, fragmentMetadata);
                    break;
            }

            fragmentExecutionPlans = result.ToArray();
        }

        private void AddStructures<TFragment>(TFragment[] fragments, List<FragmentExecutionPlan> result, FragmentMetadata fragmentMetadata)
            where TFragment: Fragment
        {
            var childFragmentMetadatas = new List<FragmentMetadata>();
            
            foreach (var fragment in fragments)
            {
                GetPlansForFragment(fragment, fragmentMetadata, out var childFragmentMetadata, out var childPlans);
                
                childFragmentMetadatas.Add(childFragmentMetadata);
                result.AddRange(childPlans);
            }

            fragmentMetadata.AddChildFragments(childFragmentMetadatas.ToArray());
        }
    }
}