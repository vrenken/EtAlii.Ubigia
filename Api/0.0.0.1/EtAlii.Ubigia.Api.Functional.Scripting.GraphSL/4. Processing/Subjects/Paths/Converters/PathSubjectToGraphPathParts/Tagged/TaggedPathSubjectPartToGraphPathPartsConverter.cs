namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class TaggedPathSubjectPartToGraphPathPartsConverter : ITaggedPathSubjectPartToGraphPathPartsConverter
    {
        private readonly IProcessingContext _context;

        public TaggedPathSubjectPartToGraphPathPartsConverter(IProcessingContext context)
        {
            _context = context;
        }

        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.Run(() =>
            {
                var taggedPathSubjectPart = (TaggedPathSubjectPart) pathSubjectPart;

                return new GraphPathPart[] { new GraphTaggedNode(taggedPathSubjectPart.Name, taggedPathSubjectPart.Tag),  };
            });
        }
    }
}