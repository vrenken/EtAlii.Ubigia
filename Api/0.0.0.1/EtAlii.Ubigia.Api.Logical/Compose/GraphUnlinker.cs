namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public class GraphUnlinker : IGraphUnlinker
    {
        private readonly IGraphChildAdder _graphChildAdder;
        private readonly IGraphLinkAdder _graphLinkAdder;

        public GraphUnlinker(
            IGraphChildAdder graphChildAdder,
            IGraphLinkAdder graphLinkAdder)
        {
            _graphChildAdder = graphChildAdder;
            _graphLinkAdder = graphLinkAdder;
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            return await Task.Run((Func<IReadOnlyEntry>)(() =>
            {
                throw new NotImplementedException();
            }));
        }
    }
}
