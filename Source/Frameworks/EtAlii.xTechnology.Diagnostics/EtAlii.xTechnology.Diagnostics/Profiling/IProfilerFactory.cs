// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics
{
    public interface IProfilerFactory
    {
        IProfiler Create(string name, string category);
    }
}
