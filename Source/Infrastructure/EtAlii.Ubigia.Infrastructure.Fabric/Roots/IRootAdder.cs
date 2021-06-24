// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Threading.Tasks;

    public interface IRootAdder
    {
        Task<Root> Add(Guid spaceId, Root root);
    }
}