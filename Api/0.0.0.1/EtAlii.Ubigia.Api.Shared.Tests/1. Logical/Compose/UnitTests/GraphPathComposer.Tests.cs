namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class GraphPathComposer_UnitTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathComposer_Create()
        {
            // Arrange.
            var context = new ComposeContext(null);
            var traverserFactory = new GraphPathTraverserFactory();
            var graphChildAdder = new GraphChildAdder(context, traverserFactory);
            var graphLinkAdder = new GraphLinkAdder(context, graphChildAdder);
            var graphUpdater = new GraphUpdater(context);
            var graphAdder = new GraphAdder(context, traverserFactory, graphChildAdder, graphLinkAdder, graphUpdater);
            var graphRemover = new GraphRemover(context, traverserFactory, graphChildAdder, graphLinkAdder, graphUpdater);
            var graphLinker = new GraphLinker(context, traverserFactory, graphChildAdder, graphLinkAdder, graphUpdater);
            var graphUnlinker = new GraphUnlinker(context, traverserFactory, graphChildAdder, graphLinkAdder);
            var graphRenamer = new GraphRenamer(context, traverserFactory, graphUpdater);

            // Act.
            var composer = new GraphComposer(graphAdder, graphRemover, graphLinker, graphUnlinker, graphRenamer); 

            // Assert.
        }
    }
}
