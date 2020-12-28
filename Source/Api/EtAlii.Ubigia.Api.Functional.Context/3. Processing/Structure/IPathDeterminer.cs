﻿namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IPathDeterminer
    {
        PathSubject Determine(FragmentMetadata fragmentMetadata, NodeAnnotation annotation, in Identifier id);
    }
}