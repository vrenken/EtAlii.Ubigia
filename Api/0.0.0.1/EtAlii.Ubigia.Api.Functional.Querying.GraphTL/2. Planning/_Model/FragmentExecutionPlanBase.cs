namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal abstract class FragmentExecutionPlanBase : IFragmentExecutionPlan
    {
        public Fragment Fragment { get; }
        
        public Type OutputType => _outputType.Value;
        private readonly Lazy<Type> _outputType;

        protected FragmentExecutionPlanBase(Fragment fragment)
        {
            Fragment = fragment;
            _outputType = new Lazy<Type>(GetOutputType);
        }

        protected abstract Type GetOutputType();

        public Task<IObservable<object>> Execute(QueryExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await Execute(scope, outputObserver);

                return Disposable.Empty;
            }).ToHotObservable();

            return Task.FromResult(outputObservable);
        }
        protected abstract Task Execute(QueryExecutionScope scope, IObserver<object> output);
    }
}