﻿namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal interface IStructureFragmentParser
    {
        string Id { get; }

        LpsParser Parser { get; }
        StructureFragment Parse(LpNode node);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, ConstantSubject item, int itemIndex, SequencePart after);
        bool CanValidate(ConstantSubject item);

    }
}