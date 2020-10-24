namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    internal class GraphRenamer : IGraphRenamer
    {
        private readonly IGraphUpdater _graphUpdater;
        private readonly IGraphPathTraverser _graphPathTraverser;

        public GraphRenamer(
            IGraphUpdater graphUpdater, 
            IGraphPathTraverser graphPathTraverser)
        {
            _graphUpdater = graphUpdater;
            _graphPathTraverser = graphPathTraverser;
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            var result = await _graphPathTraverser.TraverseToSingle(item, scope);

            if (result.Type != newName)
            {
                result = (IReadOnlyEntry)await _graphUpdater.Update(result, newName, scope);
            }

            return result;
        }
    }
}
