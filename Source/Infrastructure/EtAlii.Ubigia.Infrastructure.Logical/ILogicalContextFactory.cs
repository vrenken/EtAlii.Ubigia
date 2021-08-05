// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    public interface ILogicalContextFactory
    {
        ILogicalContext Create(ILogicalContextOptions options);
    }
}