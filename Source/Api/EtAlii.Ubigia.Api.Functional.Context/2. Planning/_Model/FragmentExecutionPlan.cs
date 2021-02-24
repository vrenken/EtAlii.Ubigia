namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Threading.Tasks;

    internal class FragmentExecutionPlan<TFragment> : FragmentExecutionPlan
        where TFragment: Fragment
    {
        private readonly IFragmentProcessor<TFragment> _processor;

        public TFragment Fragment { get; }

        public override Type OutputType { get; } = typeof(TFragment);

        public FragmentExecutionPlan(
            TFragment fragment,
            IFragmentProcessor<TFragment> processor)
        {
            _processor = processor;
            Fragment = fragment;
        }


        internal override async Task Execute(SchemaExecutionScope executionScope)
        {
            await _processor
                .Process(Fragment, Metadata, executionScope)
                .ConfigureAwait(false);
        }

        public override string ToString()
        {
            return Fragment.ToString();
        }
    }
}
