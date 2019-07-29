namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal interface IPathStructureBuilder
    {
        Task Build(
            QueryExecutionScope executionScope,
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> fragmentOutput,
            Annotation annotation,
            string structureName,
            Structure parent,
            PathSubject path);
    }
}