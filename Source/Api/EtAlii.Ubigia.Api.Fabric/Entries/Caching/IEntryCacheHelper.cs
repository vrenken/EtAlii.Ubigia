// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    public interface IEntryCacheHelper
    {
        IReadOnlyEntry Get(in Identifier identifier);

        void Store(IReadOnlyEntry entry);

        bool ShouldStore(IReadOnlyEntry entry);

        void InvalidateRelated(IReadOnlyEntry entry);
    }
}