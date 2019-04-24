namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    public class FunctionHandlerBase 
    {
        protected ISelector<object, Func<IFunctionContext, object, ExecutionScope, IObservable<Identifier>>> ToIdentifierConverterSelector { get; }

        public FunctionHandlerBase()
        {
            ToIdentifierConverterSelector = new Selector<object, Func<IFunctionContext, object, ExecutionScope, IObservable<Identifier>>>()
                .Register(i => i is PathSubject, (c, i, s) => ConvertPathToIds(c, (PathSubject)i, s))
                .Register(i => i is Identifier, (c, i, s) => Observable.Return((Identifier)i))
                .Register(i => i is IInternalNode, (c, i, s) => Observable.Return(((IInternalNode)i).Id))
                .Register(i => true, (c, i, s) => { throw new ScriptProcessingException("Unable to convert input for Function processing"); });
        }

        private IObservable<Identifier> ConvertPathToIds(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await context.PathProcessor.Process(pathSubject, scope, outputObserver);

                return Disposable.Empty;
            });

            return outputObservable
                .Select(context.ItemToIdentifierConverter.Convert)
                .ToHotObservable();
        }
    }
}