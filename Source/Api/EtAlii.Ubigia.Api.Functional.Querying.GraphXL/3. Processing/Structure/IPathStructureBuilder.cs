namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IPathStructureBuilder
    {
        Task Build(
            SchemaExecutionScope executionScope,
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> schemaOutput,
            NodeAnnotation annotation,
            string structureName,
            Structure parent,
            PathSubject path);
    }
}
