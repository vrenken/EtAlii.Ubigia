using System.Collections.ObjectModel;

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

        private readonly FragmentContext _fragmentContext;

        public override Type OutputType => _outputType.Value;
        private readonly Lazy<Type> _outputType;

        protected FragmentExecutionPlanBase(TFragment fragment, 
            FragmentContext fragmentContext)
        {
            Fragment = fragment;
            _fragmentContext = fragmentContext;
            _outputType = new Lazy<Type>(GetOutputType);
        }

        protected abstract Type GetOutputType();

        public override Task<IObservable<Structure>> Execute(QueryExecutionScope executionScope)
        {
            var outputObservable = Observable.Create<Structure>(async outputObserver =>
            {
                await Execute(_fragmentContext, executionScope, outputObserver);

                outputObserver.OnCompleted();
                
                return Disposable.Empty;
            }).ToHotObservable();

            return Task.FromResult(outputObservable);
        }
        protected abstract Task Execute(FragmentContext fragmentContext, QueryExecutionScope executionScope, IObserver<Structure> output);
    }
}