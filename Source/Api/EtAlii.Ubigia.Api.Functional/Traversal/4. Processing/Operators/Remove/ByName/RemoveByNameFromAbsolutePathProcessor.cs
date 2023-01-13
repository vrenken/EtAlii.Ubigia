// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Threading.Tasks;

internal class RemoveByNameFromAbsolutePathProcessor : IRemoveByNameFromAbsolutePathProcessor
{
    public Task Process(OperatorParameters parameters)
    {
        return Task.FromException(new ScriptProcessingException("It is not possible to remove an absolute defined node from a space"));
    }
}
