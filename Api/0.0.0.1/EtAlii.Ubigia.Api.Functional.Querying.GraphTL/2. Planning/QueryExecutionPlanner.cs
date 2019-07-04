namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.xTechnology.Collections;

    internal class QueryExecutionPlanner : IQueryExecutionPlanner
    {
        private readonly IFragmentExecutionPlannerSelector _fragmentExecutionPlannerSelector;

        public QueryExecutionPlanner(IFragmentExecutionPlannerSelector fragmentExecutionPlannerSelector)
        {
            _fragmentExecutionPlannerSelector = fragmentExecutionPlannerSelector;
        }

        public FragmentExecutionPlan[] Plan(Query query, out ObservableCollection<Structure> rootStructures)
        {
            var fragment = query.Structure;
            return GetPlansForFragment(fragment, out rootStructures);
        }

        private FragmentExecutionPlan[] GetPlansForFragment(Fragment fragment, out ObservableCollection<Structure> structures)
        {
            // SOMEWHERE
            var children = new ObservableCollection<Structure>();
            var values = new ObservableCollection<Value>();
            structures = new ObservableCollection<Structure>();

            var fragmentContext = new FragmentContext(structures, children, values);

            var result = new List<FragmentExecutionPlan>();
            var fragmentExecutionPlanner = _fragmentExecutionPlannerSelector.Select(fragment);
            var plan = fragmentExecutionPlanner.Plan(fragment, fragmentContext);

            //structure = new Structure(fragment.Name, new ReadOnlyObservableCollection<Structure>(children), new ReadOnlyObservableCollection<Value>(values));
            
            result.Add(plan);
            
            switch(fragment)
            {
                case StructureQuery structureQuery:
                    AddStructures(structureQuery.Values, children, result);
                    break;
                case StructureMutation structureMutation: 
                    AddStructures(structureMutation.Values, children, result);
                    break;
            }

            return result.ToArray();
        }

        private void AddStructures<TFragment>(TFragment[] fragments, ObservableCollection<Structure> children, List<FragmentExecutionPlan> result)
            where TFragment: Fragment
        {
            foreach (var fragment in fragments)
            {
                if (fragment is StructureQuery || fragment is StructureMutation)
                {
                    var childPlans = GetPlansForFragment(fragment, out var childStructure);
                    result.AddRange(childPlans);
                    children.AddRange(childStructure);
                }
            }
        }
    }
}