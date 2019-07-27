namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal interface IStructureBuilder
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
