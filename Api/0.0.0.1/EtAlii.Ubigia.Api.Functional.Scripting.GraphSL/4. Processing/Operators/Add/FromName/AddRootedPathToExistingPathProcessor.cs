﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddRootedPathToExistingPathProcessor : AddToExistingPathProcessorBase, IAddRootedPathToExistingPathProcessor
    {
        private readonly IScriptProcessingContext _processingContext;

        public AddRootedPathToExistingPathProcessor(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IItemToIdentifierConverter itemToIdentifierConverter, 
            IScriptProcessingContext processingContext)
            : base(itemToPathSubjectConverter, itemToIdentifierConverter)
        {
            _processingContext = processingContext;
        }

        protected override async Task Add(Identifier id, PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output)
        {
            var rootedPathSubjectToAdd = (RootedPathSubject )pathToAdd;
            var inputObservable = Observable.Create<object>(async observer =>
            {
                await _processingContext.RootedPathSubjectProcessor.Process(rootedPathSubjectToAdd, scope, observer);

                return Disposable.Empty;
            });

            // IMPORTANT: This method needs to wait until the observable is finished.
            // Else we get race conditions and very weird situations through all scripts being executed.
            var nodesToAdd = await inputObservable
                .Cast<INode>()
                .ToArray();
            foreach (INode nodeToAdd in nodesToAdd)
            {
                var identifierToAdd = nodeToAdd.Id;
                var newEntry = await _processingContext.Logical.Nodes.Add(id, identifierToAdd, scope);
                var result = new DynamicNode(newEntry);
                output.OnNext(result);
            }
        }
    }
}