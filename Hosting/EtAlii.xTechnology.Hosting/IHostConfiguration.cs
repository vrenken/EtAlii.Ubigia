using System;

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections;

    public interface IHostConfiguration
    {
        IHostExtension[] Extensions { get; }
        IHostConfiguration Use(params IHostExtension[] extensions);

        Type[] Services { get; }
        IHostConfiguration Use(params Type[] hostServices);
    }
}
