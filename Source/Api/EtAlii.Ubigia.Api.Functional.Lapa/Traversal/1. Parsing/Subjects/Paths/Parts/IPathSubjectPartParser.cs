// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal interface IPathSubjectPartParser
{
    LpsParser Parser { get; }
    bool CanParse(LpNode node);
    PathSubjectPart Parse(LpNode node);
}
