// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests;

public class ModelCount : IModelCount
{
    public int FirstChildConstructorCount { get; set; }
    public int FirstChildInitializeCount { get; set; }

    public int SecondChildConstructorCount { get; set; }
    public int SecondChildInitializeCount { get; set; }

    public int ParentConstructorCount { get; set; }
    public int ParentInitializeCount { get; set; }

}
