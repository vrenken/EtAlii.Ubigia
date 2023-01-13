// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

internal class AssignRootDefinitionToRootOperatorSubProcessor : IAssignRootDefinitionToRootOperatorSubProcessor
{
    private readonly IScriptProcessingContext _context;

    public AssignRootDefinitionToRootOperatorSubProcessor(
        IScriptProcessingContext context)
    {
        _context = context;
    }

    public Task Assign(OperatorParameters parameters)
    {
//            parameters.RightInput
//                .ToEnumerable()
//                .Cast<RootDefinitionSubject>()
//                .Single() // We do not support multiple definitions

        parameters.LeftInput
            .Cast<RootSubject>()
            .SubscribeAsync(
                onError: e => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async root =>
                {
                    try
                    {
                        var rootDefinition = (RootDefinitionSubject)parameters.RightSubject;
                        var createdRoot = await _context.Logical.Roots.Add(root.Name, rootDefinition.Type).ConfigureAwait(false);
                        parameters.Output.OnNext(createdRoot.Identifier);
                    }
                    catch (Exception e)
                    {
                        var message = $"Unable to assign definition to root: {root.Name}";
                        parameters.Output.OnError(new InvalidOperationException(message, e));
                    }
                });
        return Task.CompletedTask;
    }
}
