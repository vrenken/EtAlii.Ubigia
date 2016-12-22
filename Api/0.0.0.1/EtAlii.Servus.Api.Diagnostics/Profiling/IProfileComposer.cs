namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System.Collections.ObjectModel;

    public interface IProfileComposer
    {
        ReadOnlyObservableCollection<ProfilingResult> Results { get; }
        void Clear();
    }
}