namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class IdentifierPathSubjectPartToGraphPathPartsConverter : IIdentifierPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.Run(() =>
            {
                GraphPathPart[] result = {null};
                if (pathSubjectPartPosition == 0 ||
                    previousPathSubjectPart is IsParentOfPathSubjectPart && pathSubjectPartPosition == 1)
                {
                    var startIdentifier = ((IdentifierPathSubjectPart) pathSubjectPart).Identifier;
                    result = new GraphPathPart[] {new GraphIdentifiersStartNode(startIdentifier)};
                }
                else
                {
                    throw new ScriptProcessingException(
                        "The IdentifierPathSubjectPartProcessor should always be the first path part");
                }
                return result;
            });
        }
    }
}