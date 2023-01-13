// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.SignalRSystem;

public interface IUserClient
{
    string GetFirst();
    string GetSecond(string postfix);
}
