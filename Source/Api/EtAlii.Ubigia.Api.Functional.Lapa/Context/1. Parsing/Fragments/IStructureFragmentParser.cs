// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal interface IStructureFragmentParser
    {
        string Id { get; }

        LpsParser Parser { get; }
        StructureFragment Parse(LpNode node, INodeValidator nodeValidator);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, ConstantSubject item, int itemIndex, SequencePart after);
        bool CanValidate(ConstantSubject item);

    }
}
