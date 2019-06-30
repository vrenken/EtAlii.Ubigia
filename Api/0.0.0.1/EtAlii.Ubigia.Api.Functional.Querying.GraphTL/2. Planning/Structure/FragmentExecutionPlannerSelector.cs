namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class FragmentExecutionPlannerSelector : IFragmentExecutionPlannerSelector
    {
        private readonly ISelector<Fragment, IFragmentExecutionPlanner> _selector;

        public FragmentExecutionPlannerSelector(
            IQueryFragmentExecutionPlanner queryFragmentExecutionPlanner,
            IMutationFragmentExecutionPlanner mutationFragmentExecutionPlanner)
        {
            _selector = new Selector<Fragment, IFragmentExecutionPlanner>()
                .Register(fragment => fragment is MutationFragment, mutationFragmentExecutionPlanner)
                .Register(fragment => fragment is QueryFragment, queryFragmentExecutionPlanner);
        }

        public IFragmentExecutionPlanner Select(Fragment fragment)
        {
            return _selector.Select(fragment);
        }
    }
}