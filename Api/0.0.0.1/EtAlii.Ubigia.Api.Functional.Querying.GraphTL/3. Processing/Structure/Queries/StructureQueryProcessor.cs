namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class StructureQueryProcessor : IStructureQueryProcessor
    {
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;
        private readonly IPathStructureBuilder _pathStructureBuilder;
        private readonly IPathDeterminer _pathDeterminer;

        public StructureQueryProcessor(
            IRelatedIdentityFinder relatedIdentityFinder, 
            IPathStructureBuilder pathStructureBuilder, 
            IPathDeterminer pathDeterminer)
        {
            _relatedIdentityFinder = relatedIdentityFinder;
            _pathStructureBuilder = pathStructureBuilder;
            _pathDeterminer = pathDeterminer;
        }

        public async Task Process(
            StructureQuery fragment, 
            QueryExecutionScope executionScope, 
            IObserver<Structure> fragmentOutput)
        {
            var annotation = fragment.Annotation;
            var metaData = fragment.Metadata;
            
            if (metaData.Parent != null)
            {
                foreach (var structure in metaData.Parent.Items)
                {
                    var id = _relatedIdentityFinder.Find(structure);
                    await Build(executionScope, metaData, fragmentOutput, annotation, id, fragment.Name, structure);
                }
            }
            else
            {
                var id = Identifier.Empty; 
                await Build(executionScope, metaData, fragmentOutput, annotation, id, fragment.Name, null);
            }
        }
        
        
        public async Task Build(
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> fragmentOutput, 
            Annotation annotation, 
            Identifier id, 
            string structureName,
            Structure parent)
        {
            var path = _pathDeterminer.Determine(fragmentMetadata, annotation, id);

            await _pathStructureBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, structureName, parent, path);
        }
    }
}