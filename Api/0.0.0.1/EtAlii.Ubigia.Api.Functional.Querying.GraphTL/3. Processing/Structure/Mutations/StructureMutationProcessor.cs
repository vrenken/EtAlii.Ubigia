namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class StructureMutationProcessor : IStructureMutationProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public StructureMutationProcessor(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Process(MutationFragmentExecutionPlan plan, QueryExecutionScope executionScope, FragmentContext fragmentContext, IObserver<Structure> output)
        {
            var structureMutation = (StructureMutation) plan.Fragment;
            var script = new Script(new Sequence(new SequencePart[] {structureMutation.Annotation.Path}));
            var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);
            var lastOutput = await processResult.Output.LastOrDefaultAsync();

            var childChildren = new ObservableCollection<Structure>();
            var childValues = new ObservableCollection<Value>();
            var result = new Structure(structureMutation.Name, new ReadOnlyObservableCollection<Structure>(childChildren), new ReadOnlyObservableCollection<Value>(childValues));
            fragmentContext.Structures.Add(result);
            output.OnNext(result);
        }
    }
}