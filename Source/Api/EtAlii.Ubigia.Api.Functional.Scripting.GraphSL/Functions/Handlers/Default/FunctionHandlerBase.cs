namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Collections.Generic;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

    public class FunctionHandlerBase 
    {
        protected IEnumerable<Identifier> ToIdentifierObservable(IFunctionContext context, object o, ExecutionScope scope)
        {
            return o switch
            {
                PathSubject pathSubject => ConvertPathToIds(context, pathSubject, scope),
                Identifier identifier => new [] { identifier },
                IInternalNode node => new [] { node.Id },
                _ => throw new ScriptProcessingException("Unable to convert input for Function processing")
            };
        }
        
        private IEnumerable<Identifier> ConvertPathToIds(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await context.PathProcessor.Process(pathSubject, scope, outputObserver).ConfigureAwait(false);

                return Disposable.Empty;
            });

            return outputObservable
                .Select(context.ItemToIdentifierConverter.Convert)
                .ToEnumerable();
        }
    }
}