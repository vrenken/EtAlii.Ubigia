namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric;

    public interface IProfilingFabricContext : IFabricContext, IProfilingContext
    {
    }
}