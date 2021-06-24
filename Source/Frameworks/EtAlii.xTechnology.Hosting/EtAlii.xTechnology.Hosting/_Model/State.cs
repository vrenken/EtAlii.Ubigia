// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public enum State
    {
        Shutdown = 0,
        Starting,
        Running,
        Stopping,
        Stopped,
        Error,
    }
}