namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Threading.Tasks;

    internal class FragmentExecutionPlan<TFragment> : ExecutionPlan
        where TFragment: Fragment
    {
        private readonly IFragmentProcessor<TFragment> _processor;
        private readonly TFragment _fragment;

        public override Type OutputType { get; } = typeof(TFragment);

        public FragmentExecutionPlan(
            TFragment fragment,
            IFragmentProcessor<TFragment> processor)
        {
            _processor = processor;
            _fragment = fragment;
        }

        internal override async Task Execute(SchemaExecutionScope executionScope)
        {
            await _processor
                .Process(_fragment, ResultSink, executionScope)
                .ConfigureAwait(false);
        }

        public override string ToString()
        {
            return _fragment.ToString();
        }
    }
}
