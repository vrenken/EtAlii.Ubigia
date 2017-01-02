namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System;
    using System.Linq;

    internal interface IConstantSubjectProcessor
    {
        object Process(ProcessParameters<ConstantSubject, SequencePart> parameters);
    }
}
