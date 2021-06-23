// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System.Collections.ObjectModel;

    public class ProfileComposer : IProfileComposer
    {
        public ReadOnlyObservableCollection<ProfilingResult> Results { get; }

        private readonly ObservableCollection<ProfilingResult> _items;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IProfiler[] _profilers;

        public ProfileComposer(IProfiler[] profilers)
        {
            _items = new ObservableCollection<ProfilingResult>();
            Results = new ReadOnlyObservableCollection<ProfilingResult>(_items);

            _profilers = profilers;

            for (var i = 0; i < _profilers.Length; i++)
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