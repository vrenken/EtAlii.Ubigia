using System;

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections;

    public interface IHostConfiguration
    {
        IHostExtension[] Extensions { get; }

        IHostService[] Services { get; }
        IHostConfiguration Use(params IHostService[] services);
        IHostConfiguration Use(params IHostExtension[] extensions);
    }
}
