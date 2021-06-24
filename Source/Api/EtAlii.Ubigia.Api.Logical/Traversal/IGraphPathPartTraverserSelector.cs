// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    public interface IGraphPathPartTraverserSelector
    {
        IGraphPathPartTraverser Select(GraphPathPart part);
    }
}
