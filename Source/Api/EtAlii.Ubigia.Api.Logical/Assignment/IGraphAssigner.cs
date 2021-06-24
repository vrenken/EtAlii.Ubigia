// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphAssigner
    {
        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<IReadOnlyEntry> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope);

        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<IReadOnlyEntry> AssignTag(Identifier location, string tag, ExecutionScope scope);

        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<IReadOnlyEntry> AssignNode(Identifier location, INode node, ExecutionScope scope);

        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<IReadOnlyEntry> AssignDynamic(Identifier location, object o, ExecutionScope scope);
    }
}
