namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    internal class QueryStructureProcessor : IQueryStructureProcessor
    {
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;
        private readonly IPathStructureBuilder _pathStructureBuilder;
        private readonly IPathDeterminer _pathDeterminer;

        public QueryStructureProcessor(
            IRelatedIdentityFinder relatedIdentityFinder,
            IPathStructureBuilder pathStructureBuilder,
            IPathDeterminer pathDeterminer)
        {
            _relatedIdentityFinder = relatedIdentityFinder;
            _pathStructureBuilder = pathStructureBuilder;
            _pathDeterminer = pathDeterminer;
        }

        public async Task Process(
            StructureFragment fragment,
            FragmentMetadata fragmentMetadata,
            SchemaExecutionScope executionScope)
        {
            var annotation = fragment.Annotation;

            if (fragmentMetadata.Parent != null)
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    var id = _relatedIdentityFinder.Find(structure);
                    await Build(executionScope, fragmentMetadata, annotation, id, fragment.Name, structure).ConfigureAwait(false);
                }
            }
            else
            {
                var id = Identifier.Empty;
                await Build(executionScope, fragmentMetadata, annotation, id, fragment.Name, null).ConfigureAwait(false);
            }
        }


        private async Task Build(
            SchemaExecutionScope executionScope,
            FragmentMetadata fragmentMetadata,
            NodeAnnotation annotation,
            Identifier id,
            string structureName,
            Structure parent)
        {
            var path = _pathDeterminer.Determine(fragmentMetadata, annotation, id);

            await _pathStructureBuilder.Build(executionScope, fragmentMetadata, annotation, structureName, parent, path).ConfigureAwait(false);
        }
    }
}
