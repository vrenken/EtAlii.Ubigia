namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class StructureQueryProcessor : IStructureQueryProcessor
    {
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;
        private readonly IStructureQueryBuilder _structureQueryBuilder;

        public StructureQueryProcessor(IRelatedIdentityFinder relatedIdentityFinder, IStructureQueryBuilder structureQueryBuilder)
        {
            _relatedIdentityFinder = relatedIdentityFinder;
            _structureQueryBuilder = structureQueryBuilder;
        }

        public async Task Process(
            StructureQuery fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> fragmentOutput)
        {
            var annotation = fragment.Annotation;

            if (fragmentMetadata.Parent != null)
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    var id = _relatedIdentityFinder.Find(structure);
                    await _structureQueryBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, fragment.Name, structure);
                }
            }
            else
            {
                var id = Identifier.Empty; 
                await _structureQueryBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, fragment.Name, null);
            }
        }
    }
}