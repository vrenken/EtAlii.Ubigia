namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class StructureMutationBuilder : IStructureMutationBuilder
    {
        private readonly IGraphSLScriptContext _scriptContext;
        private readonly IPathStructureBuilder _pathStructureBuilder;
        private readonly IPathDeterminer _pathDeterminer;
        private readonly IPathCorrecter _pathCorrecter;
        
        public StructureMutationBuilder(
            IGraphSLScriptContext scriptContext, 
            IPathStructureBuilder pathStructureBuilder, 
            IPathDeterminer pathDeterminer, 
            IPathCorrecter pathCorrecter)
        {
            _scriptContext = scriptContext;
            _pathStructureBuilder = pathStructureBuilder;
            _pathDeterminer = pathDeterminer;
            _pathCorrecter = pathCorrecter;
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

            if (annotation?.Operator != null)
            {
                var mutationScript = annotation.Subject == null 
                    ? new Script(new Sequence(new SequencePart[] {path, annotation.Operator})) 
                    : new Script(new Sequence(new SequencePart[] {path, annotation.Operator, annotation.Subject}));
                var scriptResult = await _scriptContext.Process(mutationScript, executionScope.ScriptScope);
                await scriptResult.Output;

                // For some operators we need to correct the path as well.
                path = _pathCorrecter.Correct(annotation, path);
            }

            await _pathStructureBuilder.Build(executionScope, fragmentMetadata, fragmentOutput, annotation, structureName, parent, path);

        }
    }
}