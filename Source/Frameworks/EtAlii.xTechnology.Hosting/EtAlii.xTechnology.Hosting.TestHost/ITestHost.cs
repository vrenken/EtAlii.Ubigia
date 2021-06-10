// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;

    public interface ITestHost : IHost
    {
        HttpMessageHandler CreateHandler();
        HttpClient CreateClient();
    }
}
