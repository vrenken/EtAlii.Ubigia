namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class MutationFragmentExecutionPlan : FragmentExecutionPlanBase<MutationFragment>
    {
        private readonly IMutationFragmentProcessor _processor;
        //public ObservableCollection<Structure> ParentChildren { get; } = new ObservableCollection<Structure>();
        //public ObservableCollection<Value> ParentValues { get; } = new ObservableCollection<Value>();

        public MutationFragmentExecutionPlan(
            MutationFragment fragment,
            FragmentContext fragmentContext,
            IMutationFragmentProcessor processor)
            : base(fragment, fragmentContext)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(QueryFragment);
        }

        protected override Task Execute(FragmentContext fragmentContext, QueryExecutionScope executionScope, IObserver<Structure> output)
        {
            return _processor.Process(this, executionScope, fragmentContext, output);
        }

        public override string ToString()
        {
            return Fragment.ToString();
        }
    }
}