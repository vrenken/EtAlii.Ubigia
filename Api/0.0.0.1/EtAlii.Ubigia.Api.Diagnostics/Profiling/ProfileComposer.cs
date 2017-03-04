namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System.Collections.ObjectModel;

    public class ProfileComposer : IProfileComposer
    {
        public ReadOnlyObservableCollection<ProfilingResult> Results => _results;

        private readonly ReadOnlyObservableCollection<ProfilingResult> _results;

        private readonly ObservableCollection<ProfilingResult> _items;

        private readonly IProfiler[] _profilers;

        public ProfileComposer(params IProfiler[] profilers)
        {
            _items = new ObservableCollection<ProfilingResult>();
            _results = new ReadOnlyObservableCollection<ProfilingResult>(_items);

            _profilers = profilers;

            for (int i = 0; i < _profilers.Length; i++)
            {
                if (i == 0)
                {
                    _profilers[i].ProfilingStarted += result => _items.Add(result);
                }
                else
                {
                    _profilers[i].SetPrevious(_profilers[i - 1]);
                }
            }
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}