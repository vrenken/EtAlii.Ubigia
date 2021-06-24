// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal interface IPathSubjectPartToGraphPathPartsConverter
    {
        Task<GraphPathPart[]> Convert(
            PathSubjectPart pathSubjectPart,
            int pathSubjectPartPosition,
            PathSubjectPart previousPathSubjectPart,
            PathSubjectPart nextPathSubjectPart,
            ExecutionScope scope);
    }
}
