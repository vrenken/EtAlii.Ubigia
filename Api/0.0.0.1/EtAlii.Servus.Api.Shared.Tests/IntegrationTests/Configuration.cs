﻿using EtAlii.Servus.Infrastructure.Model;
namespace EtAlii.Servus.Api.Shared.Tests
{
    public class Configuration : IHostingConfiguration
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
