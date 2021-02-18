﻿namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Moppet.Lapa;

    internal interface IValueAnnotationsParser
    {
        string Id { get; }

        LpsParser Parser { get; }
        ValueAnnotation Parse(LpNode node);

        bool CanParse(LpNode node);

        void Validate(StructureFragment parent, StructureFragment self, ValueAnnotation annotation, int depth);
        bool CanValidate(Annotation annotation);
    }
}