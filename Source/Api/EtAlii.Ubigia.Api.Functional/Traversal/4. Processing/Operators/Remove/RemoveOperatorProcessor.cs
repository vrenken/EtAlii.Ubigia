// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class RemoveOperatorProcessor : IRemoveOperatorProcessor
{
    private readonly IRemoveByNameFromAbsolutePathProcessor _removeByNameFromAbsolutePathProcessor;
    private readonly IRemoveByNameFromRelativePathProcessor _removeByNameFromRelativePathProcessor;

    public RemoveOperatorProcessor(
        IRemoveByNameFromAbsolutePathProcessor removeByNameFromAbsolutePathProcessor,
        IRemoveByNameFromRelativePathProcessor removeByNameFromRelativePathProcessor)
    {
        _removeByNameFromAbsolutePathProcessor = removeByNameFromAbsolutePathProcessor;
        _removeByNameFromRelativePathProcessor = removeByNameFromRelativePathProcessor;
    }

    public Task Process(OperatorParameters parameters)
    {
        return parameters switch
        {
            {LeftSubject: EmptySubject} => _removeByNameFromAbsolutePathProcessor.Process(parameters),
            {LeftSubject: not EmptySubject} => _removeByNameFromRelativePathProcessor.Process(parameters),
            _ => throw new NotSupportedException($"Cannot determine remove path processor for: {parameters?.ToString() ?? "NULL"}")
        };
    }
}
