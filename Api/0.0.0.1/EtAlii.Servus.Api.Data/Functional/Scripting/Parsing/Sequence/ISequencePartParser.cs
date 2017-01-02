namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    internal interface ISequencePartParser
    {
        LpsParser Parser { get; }
        SequencePart Parse(LpNode node);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, SequencePart part, int partIndex, SequencePart after);
        bool CanValidate(SequencePart part);

    }
}
