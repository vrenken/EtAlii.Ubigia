namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System;

    internal interface IOperatorProcessor
    {
        object Process(ProcessParameters<Operator, SequencePart> parameters);
    }
}
