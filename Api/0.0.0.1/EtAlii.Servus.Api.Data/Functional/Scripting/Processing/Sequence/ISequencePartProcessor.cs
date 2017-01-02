namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    internal interface ISequencePartProcessor
    {
        object Process(ProcessParameters<SequencePart, SequencePart> parameters);
    }
}
