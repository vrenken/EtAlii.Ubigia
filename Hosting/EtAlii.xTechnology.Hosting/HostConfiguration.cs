namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public class HostConfiguration : IHostConfiguration
    {
        public string EnabledImage { get; private set; }
        public string ErrorImage { get; private set; }
        public string DisabledImage { get; private set; }
        public IHostCommand[] Commands { get; private set; }

        public string HostTitle { get; private set; }
        public string ProductTitle { get; private set; }

        public Func<IHost> HostFactory { get; private set; }
        public Type[] Services { get; private set; }

        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public Action<string> Output { get; private set; }

        public IHostExtension[] Extensions { get; private set; }

        public HostConfiguration()
        {
            Extensions = new IHostExtension[0];
        }

        public IHostConfiguration Use(string enabledImage, string errorImage, string disabledImage)
        {
            EnabledImage = enabledImage;
            ErrorImage = errorImage;
            DisabledImage = disabledImage;

            return this;
        }

        public IHostConfiguration Use(string productTitle = "EtAlii.xTechnology.Hosting", string hostTitle = "Host")
        {
            ProductTitle = productTitle;
            HostTitle = hostTitle;

            return this;
        }

        public IHostConfiguration Use(params IHostCommand[] commands)
        {
            Commands = Commands
                .Concat(commands)
                .Distinct()
                .ToArray();

            return this;
        }

        public IHostConfiguration Use(params Type[] hostServices)
        {
            Services = Services
                .Concat(hostServices)
                .Distinct()
                .ToArray();

            return this;
        }

        public IHostConfiguration Use(Func<IHost> hostFactory)
        {
            HostFactory = hostFactory;
            return this;
        }
        public IHostConfiguration Use(IDiagnosticsConfiguration diagnostics)
        {
            Diagnostics = diagnostics;
            return this;
        }

        public IHostConfiguration Use(Action<string> output)
        {
            Output = output;
            return this;
        }

        public IHostConfiguration Use(params IHostExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }
    }
}