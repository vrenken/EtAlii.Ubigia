namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public partial class GraphUnlinker : IGraphUnlinker
    {
        private readonly IComposeContext _context;
        private readonly IGraphPathTraverserFactory _graphPathTraverserFactory;
        private readonly IGraphChildAdder _graphChildAdder;
        private readonly IGraphLinkAdder _graphLinkAdder;

        public GraphUnlinker(
            IComposeContext context,
            IGraphPathTraverserFactory graphPathTraverserFactory,
            IGraphChildAdder graphChildAdder,
            IGraphLinkAdder graphLinkAdder)
        {
            _context = context;
            _graphPathTraverserFactory = graphPathTraverserFactory;
            _graphChildAdder = graphChildAdder;
            _graphLinkAdder = graphLinkAdder;
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            return await Task.Run<IReadOnlyEntry>((Func<IReadOnlyEntry>)(() =>
            {
                throw new NotImplementedException();
            }));
        }
    }
}
