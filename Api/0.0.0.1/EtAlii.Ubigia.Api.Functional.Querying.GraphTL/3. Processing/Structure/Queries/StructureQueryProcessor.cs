namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class StructureQueryProcessor : IStructureQueryProcessor
    {
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;
        private readonly IStructureBuilder _structureBuilder;

        public StructureQueryProcessor(IRelatedIdentityFinder relatedIdentityFinder, IStructureBuilder structureBuilder)
        {
            _relatedIdentityFinder = relatedIdentityFinder;
            _structureBuilder = structureBuilder;
        }

        public async Task Process(
            QueryFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> fragmentOutput)
        {
            var structureQuery = (StructureQuery) fragment;

            var annotation = structureQuery.Annotation;

            if (fragmentMetadata.Parent != null)
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    var id = _relatedIdentityFinder.Find(structure);
                    await _structureBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, structureQuery.Name, structure);
                }
            }
            else
            {
                var id = Identifier.Empty; 
                await _structureBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, structureQuery.Name, null);
            }
        }

    }
}