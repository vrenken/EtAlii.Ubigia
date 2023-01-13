// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests;

public interface IThirdParent
{
    object Instance { get; }
    void Initialize(object instance);
}
