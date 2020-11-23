﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class RemoveByNameFromRelativePathProcessor : IRemoveByNameFromRelativePathProcessor
    {
        private readonly IRecursiveRemover _recursiveRemover;
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IItemToPathSubjectConverter _itemToPathSubjectConverter;

        public RemoveByNameFromRelativePathProcessor(
            IRecursiveRemover recursiveRemover,
            IItemToIdentifierConverter itemToIdentifierConverter,
            IItemToPathSubjectConverter itemToPathSubjectConverter)
        {
            _recursiveRemover = recursiveRemover;
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _itemToPathSubjectConverter = itemToPathSubjectConverter;
        }

        public async Task Process(OperatorParameters parameters)
        {
            var pathToRemove = await GetPathToRemove(parameters).ConfigureAwait(false);
            if (pathToRemove == null)
            {
                throw new ScriptProcessingException("The RemoveByNameFromRelativePathProcessor requires a path on the right side");
            }

            if (!pathToRemove.Parts.All(part => part is ConstantPathSubjectPart || part is ParentPathSubjectPart))
            {
                throw new ScriptProcessingException("The RemoveByNameFromRelativePathProcessor requires a constant, hierarchical path");
            }

            if (pathToRemove.Parts.OfType<ConstantPathSubjectPart>().Count() > 1)
            {
                throw new ScriptProcessingException("The RemoveByNameFromRelativePathProcessor requires a single constant path part");
            }

            parameters.LeftInput.SubscribeAsync(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: async o =>
                {
                    var leftId = _itemToIdentifierConverter.Convert(o);
                    await Remove(pathToRemove.Parts.OfType<ConstantPathSubjectPart>().Single(), leftId, parameters.Scope, parameters.Output).ConfigureAwait(false);
                });

            //if [leftResult = = null]
            //[
            //    throw new ScriptProcessingException("The RemoveByNameFromRelativePathProcessor requires path to start from")
            //]
            //if [leftIds = = null | | !leftIds.Any[]]
            //[
            //    throw new ScriptProcessingException("The RemoveByNameFromRelativePathProcessor requires queryable ids from the previous path part")
            //]
        }

        private async Task<PathSubject> GetPathToRemove(OperatorParameters parameters)
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


        private async Task Remove(
            ConstantPathSubjectPart pathPartToRemove,
            Identifier id,
            ExecutionScope scope,
            IObserver<object> output)
        {
            var parentId = id;
            var removeResult = await _recursiveRemover.Remove(parentId, pathPartToRemove, scope).ConfigureAwait(false);
            var result = new DynamicNode((IReadOnlyEntry)removeResult.NewEntry);
            output.OnNext(result);
        }
    }
}
