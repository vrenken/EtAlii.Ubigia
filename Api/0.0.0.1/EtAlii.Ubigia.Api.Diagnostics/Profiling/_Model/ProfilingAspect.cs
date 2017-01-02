namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;

    public partial class ProfilingAspect
    {
        public ProfilingLayer Layer => _layer;
        private readonly ProfilingLayer _layer;

        public string Id => _id;
        private readonly string _id;

        public ProfilingAspect(ProfilingLayer layer, string id)
        {
            _layer = layer;
            _id = id;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", _layer, _id);
        }
    }
}