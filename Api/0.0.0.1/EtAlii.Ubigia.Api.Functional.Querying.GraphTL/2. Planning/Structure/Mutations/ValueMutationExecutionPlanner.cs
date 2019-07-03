namespace EtAlii.Ubigia.Api.Functional 
{
    internal class ValueMutationExecutionPlanner : IValueMutationExecutionPlanner
    {
        private readonly IValueMutationProcessor _mutationProcessor;

        public ValueMutationExecutionPlanner(IValueMutationProcessor mutationProcessor)
        {
            _mutationProcessor = mutationProcessor;
        }

        public FragmentExecutionPlan Plan(Fragment fragment, FragmentContext fragmentContext)
        {
            var valueMutation = (ValueMutation)fragment;
            return new MutationFragmentExecutionPlan(valueMutation, fragmentContext, _mutationProcessor);
        }
    }
}