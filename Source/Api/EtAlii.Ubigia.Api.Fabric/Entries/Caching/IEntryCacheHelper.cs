// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    public interface IEntryCacheHelper
    {
        IReadOnlyEntry Get(in Identifier identifier, ExecutionScope scope);

        void Store(IReadOnlyEntry entry, ExecutionScope scope);

        bool ShouldStore(IReadOnlyEntry entry);

        void InvalidateRelated(IReadOnlyEntry entry, ExecutionScope scope);
    }
}
