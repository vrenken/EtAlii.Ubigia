namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class StructureMutationProcessor : IStructureMutationProcessor
    {
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;
        private readonly IStructureBuilder _structureBuilder;

        public StructureMutationProcessor(IRelatedIdentityFinder relatedIdentityFinder, IStructureBuilder structureBuilder)
        {
            _relatedIdentityFinder = relatedIdentityFinder;
            _structureBuilder = structureBuilder;
        }

        public async Task Process(
            MutationFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> fragmentOutput)
        {
            var structureMutation = (StructureMutation) fragment;

            var annotation = structureMutation.Annotation;

            if (fragmentMetadata.Parent != null)
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    var id = _relatedIdentityFinder.Find(structure);
                    await _structureBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, structureMutation.Name, structure);
                }
            }
            else
            {
                var id = Identifier.Empty; 
                await _structureBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, structureMutation.Name, null);
            }
        }
    }
}