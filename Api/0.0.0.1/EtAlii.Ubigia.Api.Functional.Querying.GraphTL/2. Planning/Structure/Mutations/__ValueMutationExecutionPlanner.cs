//namespace EtAlii.Ubigia.Api.Functional 
//{
//    internal class ValueMutationExecutionPlanner : IValueMutationExecutionPlanner
//    {
//        private readonly IValueMutationProcessor _mutationProcessor;
//
//        public ValueMutationExecutionPlanner(IValueMutationProcessor mutationProcessor)
//        {
//            _mutationProcessor = mutationProcessor;
//        }
//
//        public FragmentExecutionPlan Plan(Fragment fragment, FragmentMetadata fragmentMetadata)
//        {
//            var valueMutation = (ValueMutation)fragment;
//            return new FragmentExecutionPlan<ValueMutation>(valueMutation, fragmentMetadata, _mutationProcessor);
//        }
//    }
//}