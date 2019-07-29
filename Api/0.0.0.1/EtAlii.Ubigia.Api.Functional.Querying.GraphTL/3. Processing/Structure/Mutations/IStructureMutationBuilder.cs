namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal interface IStructureMutationBuilder
    {
        Task Build(
            QueryExecutionScope executionScope,
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> fragmentOutput,
            Annotation annotation, Identifier id,
            string structureName,
            Structure parent);
    }
}
