// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IDynamicObjectToIdentifierAssigner
    {
        Task<INode> Assign(object dynamicObject, Identifier id, ExecutionScope scope);
    }
}