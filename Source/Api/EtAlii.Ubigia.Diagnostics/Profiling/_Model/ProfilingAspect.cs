// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public sealed partial class ProfilingAspect
    {
        public ProfilingLayer Layer { get; }

        public string Id { get; }

        public ProfilingAspect(ProfilingLayer layer, string id)
        {
            Layer = layer;
            Id = id;
        }

        public override string ToString()
        {
            return $"{Layer} - {Id}";
        }
    }
}