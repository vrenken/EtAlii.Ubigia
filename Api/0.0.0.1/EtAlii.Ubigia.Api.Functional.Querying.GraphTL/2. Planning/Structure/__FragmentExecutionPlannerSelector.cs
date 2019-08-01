//namespace EtAlii.Ubigia.Api.Functional 
//{
//    using EtAlii.xTechnology.Structure;
//
//    internal class FragmentExecutionPlannerSelector : IFragmentExecutionPlannerSelector
//    {
//        private readonly ISelector<Fragment, IFragmentExecutionPlanner> _selector;
//
//        public FragmentExecutionPlannerSelector(
//            IValueQueryExecutionPlanner valueQueryExecutionPlanner,
//            IStructureQueryExecutionPlanner structureQueryExecutionPlanner,
//            IValueMutationExecutionPlanner valueMutationExecutionPlanner,
//            IStructureMutationExecutionPlanner structureMutationExecutionPlanner)
//        {
//            _selector = new Selector<Fragment, IFragmentExecutionPlanner>()
//                .Register(fragment => fragment is StructureQuery, structureQueryExecutionPlanner)
//                .Register(fragment => fragment is ValueQuery, valueQueryExecutionPlanner)
//                .Register(fragment => fragment is StructureMutation, structureMutationExecutionPlanner)
//                .Register(fragment => fragment is ValueMutation, valueMutationExecutionPlanner);
//        }
//
//        public IFragmentExecutionPlanner Select(Fragment fragment)
//        {
//            return _selector.Select(fragment);
//        }
//    }
//}