namespace EtAlii.Ubigia.Api.Functional
{
    internal class MutationFragmentExecutionPlanner : IMutationFragmentExecutionPlanner
    {
        //private readonly IConstantSubjectProcessorSelector _processorSelector;

        public MutationFragmentExecutionPlanner()//IConstantSubjectProcessorSelector processorSelector)
        {
            //_processorSelector = processorSelector;
        }

        public FragmentExecutionPlan Plan(Fragment fragment)
        {
            var mutationFragment = (MutationFragment)fragment;

            return null;
            //var processor = _processorSelector.Select(constantSubject);
            //return new ConstantSubjectExecutionPlan(constantSubject, processor);
        }
    }
}