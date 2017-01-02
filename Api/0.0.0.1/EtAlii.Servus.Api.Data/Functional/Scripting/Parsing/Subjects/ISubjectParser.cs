namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System;

    internal interface ISubjectParser
    {
        LpsParser Parser { get; }
        Subject Parse(LpNode node);
        bool CanParse(LpNode node);
        void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after);
        bool CanValidate(Subject subject);
    }
}
