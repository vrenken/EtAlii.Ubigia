namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Transport;

    public interface IProfilingDataConnection : IDataConnection, IProfilingContext
    {
    }
}