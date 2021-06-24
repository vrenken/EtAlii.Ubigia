// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public delegate TOut Operation<in TIn, out TOut>(TIn input);

    public delegate void Operation<in TValue>(TValue input);
}