// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;

public class ServicesTestConfiguration
{
    public required IConfigurationRoot ConfigurationRoot { get; init; }
    public required IService[] Services { get; init; }

    public required ISystemStatusChecker AlternativeSystemStatusChecker { get; init; }
}
