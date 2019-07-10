namespace EtAlii.Ubigia.Api.Functional 
{
    internal class StructureMutationExecutionPlanner : IStructureMutationExecutionPlanner
    {
        private readonly IStructureMutationProcessor _mutationProcessor;

        public StructureMutationExecutionPlanner(IStructureMutationProcessor mutationProcessor)
        {
            _mutationProcessor = mutationProcessor;
        }

        public FragmentExecutionPlan Plan(Fragment fragment, FragmentMetadata fragmentMetadata)
        {
            var structureMutation = (StructureMutation)fragment;
            return new MutationFragmentExecutionPlan(structureMutation, fragmentMetadata, _mutationProcessor);
        }
    }
}