// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;

    public struct PortRegistration
    {
        public ushort Port { get; init; }
        public DateTime RegisteredAt { get; init; }
    }
}
