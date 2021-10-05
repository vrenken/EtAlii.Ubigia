// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Threading.Tasks;

    public partial class ConsoleHost : HostBase
    {
        public ConsoleHost(HostOptions options)
            : base(options)
        {
        }

        protected override Task Started() => Task.CompletedTask;

        protected override Task Stopping() => Task.CompletedTask;
    }
}
