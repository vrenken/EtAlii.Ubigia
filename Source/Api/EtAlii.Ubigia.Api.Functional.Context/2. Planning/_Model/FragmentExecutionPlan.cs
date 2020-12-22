namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

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


        internal override Task<IObservable<Structure>> Execute(SchemaExecutionScope executionScope)
        {
            var outputObservable = Observable.Create<Structure>(async outputObserver =>
            {
                await Execute(executionScope, outputObserver).ConfigureAwait(false);

                outputObserver.OnCompleted();

                return Disposable.Empty;
            }).ToHotObservable();

            return Task.FromResult(outputObservable);
        }
        protected Task Execute(SchemaExecutionScope executionScope, IObserver<Structure> schemaOutput)
        {
            return _processor.Process(Fragment, Metadata, executionScope, schemaOutput);
        }

        public override string ToString()
        {
            return Fragment.ToString();
        }
    }
}
