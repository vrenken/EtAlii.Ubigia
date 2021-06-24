// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System.Collections.ObjectModel;

    public interface IProfileComposer
    {
        ReadOnlyObservableCollection<ProfilingResult> Results { get; }
        void Clear();
    }
}
