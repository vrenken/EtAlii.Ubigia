namespace EtAlii.xTechnology.Hosting
{
    using System;

    public interface IHostConfiguration
    {
        IHostExtension[] Extensions { get; }
        IHostConfiguration Use(params IHostExtension[] extensions);

        Type[] Services { get; }
        IHostConfiguration Use(params Type[] hostServices);

        IHostCommand[] Commands { get; }
        IHostConfiguration Use(params IHostCommand[] commands);
    }
}
