// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface INodeToIdentifierAssigner 
    {
        Task<INode> Assign(INode node, Identifier id, ExecutionScope scope);
    }
}