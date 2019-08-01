namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class StructureMutationProcessor : IStructureMutationProcessor
    {
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;
        private readonly IStructureMutationBuilder _structureMutationBuilder;

        public StructureMutationProcessor(IRelatedIdentityFinder relatedIdentityFinder, IStructureMutationBuilder structureMutationBuilder)
        {
            _relatedIdentityFinder = relatedIdentityFinder;
            _structureMutationBuilder = structureMutationBuilder;
        }

        public async Task Process(
            StructureMutation fragment, 
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
                    await _structureMutationBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, fragment.Name, structure);
                }
            }
            else
            {
                var id = Identifier.Empty; 
                await _structureMutationBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, id, fragment.Name, null);
            }
        }
    }
}