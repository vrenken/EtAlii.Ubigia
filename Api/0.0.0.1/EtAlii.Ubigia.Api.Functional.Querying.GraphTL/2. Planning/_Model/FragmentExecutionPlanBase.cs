namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal abstract class FragmentExecutionPlanBase<TFragment> : FragmentExecutionPlan
        where TFragment : Fragment
    {
        public TFragment Fragment { get; }

        private readonly FragmentMetadata _fragmentMetadata;

        public override Type OutputType => _outputType.Value;
        private readonly Lazy<Type> _outputType;

        protected FragmentExecutionPlanBase(TFragment fragment, 
            FragmentMetadata fragmentMetadata)
        {
            Fragment = fragment;
            _fragmentMetadata = fragmentMetadata;
            _outputType = new Lazy<Type>(GetOutputType);
        }

        protected abstract Type GetOutputType();

        internal override Task<IObservable<Structure>> Execute(QueryExecutionScope executionScope)
        {
            var outputObservable = Observable.Create<Structure>(async outputObserver =>
            {
                await Execute(_fragmentMetadata, executionScope, outputObserver);

                outputObserver.OnCompleted();
                
                return Disposable.Empty;
            }).ToHotObservable();

            return Task.FromResult(outputObservable);
        }
        protected abstract Task Execute(FragmentMetadata fragmentMetadata, QueryExecutionScope executionScope, IObserver<Structure> output);
    }
}