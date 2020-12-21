namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class TaggedPathSubjectPartToGraphPathPartsConverter : ITaggedPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            var taggedPathSubjectPart = (TaggedPathSubjectPart) pathSubjectPart;

            var result = new GraphPathPart[] { new GraphTaggedNode(taggedPathSubjectPart.Name, taggedPathSubjectPart.Tag),  };
            return Task.FromResult(result);
        }
    }
}
