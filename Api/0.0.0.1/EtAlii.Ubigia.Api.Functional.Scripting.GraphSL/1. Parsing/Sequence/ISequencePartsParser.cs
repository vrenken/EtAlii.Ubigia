﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface ISequencePartsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        SequencePart Parse(LpNode node);
        void Validate(SequencePart before, SequencePart part, int partIndex, SequencePart after);
    }
}