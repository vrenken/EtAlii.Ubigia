// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

internal class RemoveByIdFromRelativePathProcessor : IRemoveByIdFromRelativePathProcessor
{
    private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
    private readonly IScriptProcessingContext _context;

    public RemoveByIdFromRelativePathProcessor(
        IScriptProcessingContext context,
        IItemToIdentifierConverter itemToIdentifierConverter)
    {
        _itemToIdentifierConverter = itemToIdentifierConverter;
        _context = context;
    }
    public async Task Process(OperatorParameters parameters)
    {
        var rightResult = await parameters.RightInput.ToArray();
        var rightId = _itemToIdentifierConverter.Convert(rightResult);
        if (rightId == Identifier.Empty)
        {
            throw new ScriptProcessingException("The RemoveByIdFromRelativePathProcessor requires a identifier to add");
        }

        parameters.LeftInput.SubscribeAsync(
            onError: parameters.Output.OnError,
            onCompleted: parameters.Output.OnCompleted,
            onNext: async o =>
            {
                try
                {
                    var leftId = _itemToIdentifierConverter.Convert(o);
                    await Remove(leftId, rightId, parameters.Scope, parameters.Output).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    var message = "Unable to remove from relative path by ID";
                    parameters.Output.OnError(new InvalidOperationException(message, e));
                }
            });

        //if [leftIds = = null | | !leftIds.Any[]]
        //[
        //    throw new ScriptProcessingException("The RemoveByIdFromRelativePathProcessor requires queryable ids from the previous path part")
        //]
    }

    private async Task Remove(
        Identifier id,
        Identifier identifierToRemove,
        ExecutionScope scope,
        IObserver<object> output)
    {
        var newEntry = await _context.Logical.Nodes.Remove(id, identifierToRemove, scope).ConfigureAwait(false);
        var result = new Node(newEntry);
        output.OnNext(result);
    }
}
