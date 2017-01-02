namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using EtAlii.Servus.Api.Data.Model;

    internal interface IPathSubjectPartProcessor
    {
        object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters);
    }
}
