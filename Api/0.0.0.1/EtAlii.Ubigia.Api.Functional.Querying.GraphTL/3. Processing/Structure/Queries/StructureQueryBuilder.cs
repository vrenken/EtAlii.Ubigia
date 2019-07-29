namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class StructureQueryBuilder : StructureBuilderBase, IStructureQueryBuilder
    {
        public StructureQueryBuilder(IGraphSLScriptContext scriptContext)
        : base(scriptContext)
        {
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
            var path = DeterminePath(fragmentMetadata, annotation, id);

            await BuildFromPath(executionScope, fragmentMetadata, fragmentOutput, annotation, structureName, parent, path);
        }
    }
}