// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class IdentifierPathSubjectPartToGraphPathPartsConverter : IIdentifierPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            GraphPathPart[] result;
            if (pathSubjectPartPosition == 0 ||
                previousPathSubjectPart is ParentPathSubjectPart && pathSubjectPartPosition == 1)
            {
                var startIdentifier = ((IdentifierPathSubjectPart) pathSubjectPart).Identifier;
                result = new GraphPathPart[] {new GraphIdentifiersStartNode(startIdentifier)};
            }
            else
            {
                throw new ScriptProcessingException(
                    "The IdentifierPathSubjectPartProcessor should always be the first path part");
            }
            return Task.FromResult(result);
        }
    }
}
