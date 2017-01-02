namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System;
    using System.Linq;

    internal interface IConstantSubjectParser
    {
        LpsParser Parser { get; }
        ConstantSubject Parse(LpNode node);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after);
        bool CanValidate(ConstantSubject constantSubject);
    }
}
