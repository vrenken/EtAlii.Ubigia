// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public interface IOperation<in TIn, out TOut>
    {
        TOut Process(TIn input);
    }

    public interface IOperation<in TIn>
    {
        void Process(TIn input);
    }
}