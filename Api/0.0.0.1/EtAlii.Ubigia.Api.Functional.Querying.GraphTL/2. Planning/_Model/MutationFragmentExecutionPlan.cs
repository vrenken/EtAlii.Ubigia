namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class MutationFragmentExecutionPlan : FragmentExecutionPlanBase<MutationFragment>
    {
        private readonly IMutationFragmentProcessor _processor;

        public MutationFragmentExecutionPlan(
            MutationFragment fragment,
            FragmentMetadata fragmentMetadata,
            IMutationFragmentProcessor processor)
            : base(fragment, fragmentMetadata)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(QueryFragment);
        }

        protected override Task Execute(FragmentMetadata fragmentMetadata, QueryExecutionScope executionScope, IObserver<Structure> output)
        {
            return _processor.Process(this, executionScope, fragmentMetadata, output);
        }

        public override string ToString()
        {
            return Fragment.ToString();
        }
    }
}