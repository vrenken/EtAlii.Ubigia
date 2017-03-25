namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;

    public partial class ProfilingAspect
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