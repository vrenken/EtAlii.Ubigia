namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    internal class GraphLinker : IGraphLinker
    {
        private readonly IGraphChildAdder _graphChildAdder;
        private readonly IGraphLinkAdder _graphLinkAdder;
        private readonly IGraphUpdater _graphUpdater;
        private readonly IGraphPathTraverser _graphPathTraverser;

        public GraphLinker(
            IGraphChildAdder graphChildAdder,
            IGraphLinkAdder graphLinkAdder,
            IGraphUpdater graphUpdater, 
            IGraphPathTraverser graphPathTraverser)
        {
            _graphChildAdder = graphChildAdder;
            _graphLinkAdder = graphLinkAdder;
            _graphUpdater = graphUpdater;
            _graphPathTraverser = graphPathTraverser;
        }

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            // The GraphComposer cannot handle multiple updates yet.
            var locationEntry = await _graphPathTraverser.TraverseToSingle(location, scope);

            // The GraphComposer cannot handle multiple updates yet.
            var itemEntry = await _graphPathTraverser.TraverseToSingle(item, scope);

            // Let's check if a path already exists.
            var linkAddResult = await _graphLinkAdder.GetLink(itemName, locationEntry, scope);
            var locationLinkOriginalEntry = linkAddResult.Item1;
            var locationResult = linkAddResult.Item2;
            if (locationResult == null)
            {
                var locationUpdateEntry = await _graphUpdater.Update(locationEntry, scope);
                var locationLinkUpdateEntry = await _graphLinkAdder.AddLink(locationUpdateEntry, locationLinkOriginalEntry, EntryType.Add, scope);
                await _graphChildAdder.AddChild(locationLinkUpdateEntry.Id, itemName, scope);
            }

            throw new NotImplementedException();

            //return locationResult
        }
    }
}
