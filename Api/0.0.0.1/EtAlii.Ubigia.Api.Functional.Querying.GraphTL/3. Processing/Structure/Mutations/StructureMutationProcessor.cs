namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class StructureMutationProcessor : IStructureMutationProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public StructureMutationProcessor(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Process(MutationFragmentExecutionPlan plan, QueryExecutionScope executionScope, FragmentMetadata fragmentMetadata, IObserver<Structure> output)
        {
            var structureMutation = (StructureMutation) plan.Fragment;
            var script = new Script(new Sequence(new SequencePart[] {structureMutation.Annotation.Path}));
            var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);
            var node = await processResult.Output.Cast<IInternalNode>().SingleOrDefaultAsync();

            var result = new Structure(structureMutation.Name, node.Type, null);

            fragmentMetadata.Items.Add(result);
            output.OnNext(result);
        }
    }
}