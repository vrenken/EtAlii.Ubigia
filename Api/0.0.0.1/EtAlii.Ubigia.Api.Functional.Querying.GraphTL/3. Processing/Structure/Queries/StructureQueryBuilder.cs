namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class StructureQueryBuilder : IStructureQueryBuilder
    {
        private readonly IPathStructureBuilder _pathStructureBuilder;
        private readonly IPathDeterminer _pathDeterminer;

        public StructureQueryBuilder(IPathStructureBuilder pathStructureBuilder, IPathDeterminer pathDeterminer)
        {
            _pathStructureBuilder = pathStructureBuilder;
            _pathDeterminer = pathDeterminer;
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