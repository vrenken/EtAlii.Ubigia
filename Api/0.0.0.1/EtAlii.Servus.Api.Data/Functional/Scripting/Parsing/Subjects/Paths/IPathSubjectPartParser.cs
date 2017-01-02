namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using EtAlii.Servus.Api.Data.Model;

    internal interface IPathSubjectPartParser
    {
        LpsParser Parser { get; }
        bool CanParse(LpNode node);
        PathSubjectPart Parse(LpNode node);

        bool CanValidate(PathSubjectPart part);
        void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after);

    }
}