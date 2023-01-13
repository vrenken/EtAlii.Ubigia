// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

internal class AssignEmptyToRootOperatorSubProcessor : IAssignEmptyToRootOperatorSubProcessor
{
    private readonly IScriptProcessingContext _context;

    public AssignEmptyToRootOperatorSubProcessor(
        IScriptProcessingContext context)
    {
        _context = context;
    }

    public Task Assign(OperatorParameters parameters)
    {
//            parameters.RightInput
//                .ToEnumerable()
//                .Cast<RootDefinitionSubject>()
//                .SingleOrDefault() // We do not support multiple definitions, should be null

        parameters.LeftInput
            .Cast<RootSubject>()
            .SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async (root) =>
                {
                    try
                    {
                        var r = await _context.Logical.Roots.Get(root.Name).ConfigureAwait(false);
                        await _context.Logical.Roots.Remove(r.Id).ConfigureAwait(false);
                        parameters.Output.OnNext(root.Name);
                    }
                    catch (Exception e)
                    {
                        var message = $"Unable to assign empty to root: {root.Name}";
                        parameters.Output.OnError(new InvalidOperationException(message, e));
                    }
                });
        return Task.CompletedTask;
    }
}
