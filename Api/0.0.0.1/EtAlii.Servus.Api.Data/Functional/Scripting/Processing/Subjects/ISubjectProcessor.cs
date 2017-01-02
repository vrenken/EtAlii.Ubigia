namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System;

    internal interface ISubjectProcessor
    {
        object Process(ProcessParameters<Subject, SequencePart> parameters);
    }
}
