// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Threading.Tasks;

internal interface IRecursiveRemover
{
    Task<RecursiveRemoveResult> Remove(
        Identifier parentId,
        ConstantPathSubjectPart part,
        ExecutionScope scope);
}
