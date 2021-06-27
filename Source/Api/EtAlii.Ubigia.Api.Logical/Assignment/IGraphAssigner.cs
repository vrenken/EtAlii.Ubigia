// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    // Can the methods in IGraphAssigner be refactored to adhere to the IObserver<object> pattern?
    // More details can be found in the Github issue below:
    // https://github.com/vrenken/EtAlii.Ubigia/issues/73
    public interface IGraphAssigner
    {
        Task<IReadOnlyEntry> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope);

        Task<IReadOnlyEntry> AssignTag(Identifier location, string tag, ExecutionScope scope);

        Task<IReadOnlyEntry> AssignNode(Identifier location, INode node, ExecutionScope scope);

        Task<IReadOnlyEntry> AssignDynamic(Identifier location, object o, ExecutionScope scope);
    }
}
