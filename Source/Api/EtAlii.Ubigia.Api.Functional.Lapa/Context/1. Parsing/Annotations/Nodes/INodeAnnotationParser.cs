// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Moppet.Lapa;

    internal interface INodeAnnotationParser
    {
        string Id { get; }

        LpsParser Parser { get; }
        NodeAnnotation Parse(LpNode node);

        bool CanParse(LpNode node);

        void Validate(StructureFragment parent, StructureFragment self, NodeAnnotation annotation, int depth);
        bool CanValidate(NodeAnnotation annotation);
    }
}
