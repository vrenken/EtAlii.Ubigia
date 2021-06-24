// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphAssigner
    {
        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions. 
        Task<INode> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope);

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions. 
        Task<INode> AssignTag(Identifier location, string tag, ExecutionScope scope);

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions. 
        Task<INode> AssignNode(Identifier location, INode node, ExecutionScope scope);

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions. 
        Task<INode> AssignDynamic(Identifier location, object o, ExecutionScope scope);
    }
}