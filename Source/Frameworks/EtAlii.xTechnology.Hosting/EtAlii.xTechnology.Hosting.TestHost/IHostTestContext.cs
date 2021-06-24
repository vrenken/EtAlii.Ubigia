// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHostTestContext
    {
        Task Start(PortRange portRange);

        Task Stop();

        HttpMessageHandler CreateHandler();
        HttpClient CreateClient();
    }
}
