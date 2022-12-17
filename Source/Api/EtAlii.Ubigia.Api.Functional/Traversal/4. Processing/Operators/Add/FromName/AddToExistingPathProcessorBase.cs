// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal abstract class AddToExistingPathProcessorBase
    {
        private readonly IItemToPathSubjectConverter _itemToPathSubjectConverter;
        protected IItemToIdentifierConverter ItemToIdentifierConverter { get; }

        protected AddToExistingPathProcessorBase(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IItemToIdentifierConverter itemToIdentifierConverter)
        {
            _itemToPathSubjectConverter = itemToPathSubjectConverter;
            ItemToIdentifierConverter = itemToIdentifierConverter;
        }

        public async Task Process(OperatorParameters parameters)
        {
            var pathToAdd = await GetPathToAdd(parameters).ConfigureAwait(false);
            if (pathToAdd == null)
            {
                throw new ScriptProcessingException($"The {GetType().Name} requires a path on the right side");
            }

            if (!pathToAdd.Parts.All(part => part is ConstantPathSubjectPart || part is ParentPathSubjectPart))
            {
                throw new ScriptProcessingException($"The {GetType().Name} requires a constant, hierarchical path");
            }

            if (pathToAdd.Parts.Any(part => part is ConstantPathSubjectPart constantPathSubjectPart && string.IsNullOrWhiteSpace(constantPathSubjectPart.Name)))
            {
                throw new ScriptProcessingException($"The {GetType().Name} cannot handle empty parts");
            }

            parameters.LeftInput.SubscribeAsync(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: async o =>
                {
                    try
                    {
                        var leftId = ItemToIdentifierConverter.Convert(o);
                        await Add(leftId, pathToAdd, parameters.Scope, parameters.Output).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        var message = "Unable to add to existing path";
                        parameters.Output.OnError(new InvalidOperationException(message, e));
                    }
                });
        }

        private async Task<PathSubject> GetPathToAdd(OperatorParameters parameters)
        {
            PathSubject pathToAdd = null;

            if (parameters.RightSubject is PathSubject pathSubject)
            {
                pathToAdd = pathSubject;
            }
            else
            {
                var rightResult = await parameters.RightInput.SingleOrDefaultAsync();

                _itemToPathSubjectConverter.TryConvert(rightResult, out pathToAdd);
            }

            return pathToAdd;
        }

        protected abstract Task Add(Identifier id, PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output);
    }
}
