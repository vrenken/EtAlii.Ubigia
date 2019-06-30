namespace EtAlii.Ubigia.Api.Functional
{
    internal class QueryFragmentExecutionPlanner : IQueryFragmentExecutionPlanner
    {
        //private readonly IConstantSubjectProcessorSelector _processorSelector;

        public QueryFragmentExecutionPlanner()//IConstantSubjectProcessorSelector processorSelector)
        {
            //_processorSelector = processorSelector;
        }

        public FragmentExecutionPlan Plan(Fragment fragment)
        {
            var queryFragment = (QueryFragment)fragment;

            return null;
            //var processor = _processorSelector.Select(constantSubject);
            //return new ConstantSubjectExecutionPlan(constantSubject, processor);
        }
    }
}