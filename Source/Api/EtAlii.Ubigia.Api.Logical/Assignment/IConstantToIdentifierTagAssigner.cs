// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IConstantToIdentifierTagAssigner
    {
        Task<INode> Assign(string constant, Identifier id, ExecutionScope scope);
    }
}