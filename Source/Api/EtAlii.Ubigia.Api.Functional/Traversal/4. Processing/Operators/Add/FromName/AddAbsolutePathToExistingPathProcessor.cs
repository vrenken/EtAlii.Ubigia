// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddAbsolutePathToExistingPathProcessor : AddToExistingPathProcessorBase, IAddAbsolutePathToExistingPathProcessor
    {
        private readonly IScriptProcessingContext _processingContext;

        public AddAbsolutePathToExistingPathProcessor(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IItemToIdentifierConverter itemToIdentifierConverter,
            IScriptProcessingContext processingContext)
            : base(itemToPathSubjectConverter, itemToIdentifierConverter)
        {
            _processingContext = processingContext;
        }

        protected override async Task Add(Identifier id, PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output)
        {
            var absolutePathSubjectToAdd = (AbsolutePathSubject )pathToAdd;
            var inputObservable = Observable.Create<object>(async observer =>
            {
                await _processingContext.AbsolutePathSubjectProcessor.Process(absolutePathSubjectToAdd, scope, observer).ConfigureAwait(false);

                return Disposable.Empty;
            });

            // IMPORTANT: This method needs to wait until the observable is finished.
            // Else we get race conditions and very weird situations through all scripts being executed.
            var nodesToAdd = await inputObservable
                .Cast<INode>()
                .ToArray();
            foreach (var nodeToAdd in nodesToAdd)
            {
                var identifierToAdd = nodeToAdd.Id;
                var newEntry = await _processingContext.Logical.Nodes.Add(id, identifierToAdd, scope).ConfigureAwait(false);
                var result = new DynamicNode(newEntry);
                output.OnNext(result);
            }
        }
    }
}
