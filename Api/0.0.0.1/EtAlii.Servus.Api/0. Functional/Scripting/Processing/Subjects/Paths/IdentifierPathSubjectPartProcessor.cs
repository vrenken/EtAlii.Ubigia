namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;
    using EtAlii.Servus.Api.Logical;

    internal class IdentifierPathSubjectPartProcessor : IPathSubjectPartProcessor
    {
        private readonly ProcessingContext _context;

        public IdentifierPathSubjectPartProcessor(ProcessingContext context)
        {
            _context = context;
        }

        public object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters)
        {
            object result = null;
            if (parameters.LeftPart == null ||
                parameters.LeftPart is IsParentOfPathSubjectPart && parameters.FuturePart == null)
            {
                var id = ((IdentifierPathSubjectPart)parameters.Target).Identifier;
                result = new IReadOnlyEntry[] { _context.Logical.Nodes.Select(id, GraphPath.Empty) }.ToArray();
            }
            else
            {
                throw new ScriptProcessingException("The IdentifierPathSubjectPartProcessor should always be the first path part");
            }
            return result;
        }
    }
}
