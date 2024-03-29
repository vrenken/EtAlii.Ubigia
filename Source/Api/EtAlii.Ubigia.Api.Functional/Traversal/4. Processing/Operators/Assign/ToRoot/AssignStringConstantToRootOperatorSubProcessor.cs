// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

internal class AssignStringConstantToRootOperatorSubProcessor : IAssignStringConstantToRootOperatorSubProcessor
{
    private readonly IScriptProcessingContext _context;

    public AssignStringConstantToRootOperatorSubProcessor(
        IScriptProcessingContext context)
    {
        _context = context;
    }

    public Task Assign(OperatorParameters parameters)
    {
        // ReSharper disable once UnusedVariable
//            var definition = parameters.RightInput
//                .ToEnumerable()
//                .Cast<string>()
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
                        var existingRoot = await _context.Logical.Roots.Get(root.Name).ConfigureAwait(false);
                        if (existingRoot == null)
                        {
                            var rootType = new RootType(((StringConstantSubject)parameters.RightSubject).Value);
                            await _context.Logical.Roots.Add(root.Name, rootType).ConfigureAwait(false);
                        }
                        else
                        {
                            var rootType = new RootType(((StringConstantSubject)parameters.RightSubject).Value);
                            await _context.Logical.Roots.Change(existingRoot.Id, root.Name, rootType).ConfigureAwait(false);
                        }
                        parameters.Output.OnNext(root.Name);
                    }
                    catch (Exception e)
                    {
                        var message = $"Unable to assign string constant to root: {root.Name}";
                        parameters.Output.OnError(new InvalidOperationException(message, e));
                    }
                });
        return Task.CompletedTask;
    }
}
