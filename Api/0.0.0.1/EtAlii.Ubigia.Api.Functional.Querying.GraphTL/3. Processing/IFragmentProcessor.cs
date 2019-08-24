﻿namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal interface IFragmentProcessor<in TFragment>
        where TFragment: Fragment
    {
        Task Process(
            TFragment fragment,
            FragmentMetadata fragmentMetadata,
            SchemaExecutionScope executionScope, 
            IObserver<Structure> schemaOutput);

    }
}
