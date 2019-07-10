namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class ValueMutationProcessor : IValueMutationProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public ValueMutationProcessor(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Process(MutationFragmentExecutionPlan plan, QueryExecutionScope executionScope, FragmentMetadata fragmentMetadata, IObserver<Structure> output)
        {
            var valueQuery = (ValueMutation) plan.Fragment;
            
            var script = new Script(new Sequence(new SequencePart[] {valueQuery.Annotation.Path}));
            var processResult = await _scriptContext.Process(script, executionScope.ScriptScope); 
            var node = await processResult.Output.Cast<IInternalNode>().SingleOrDefaultAsync();

            //var result = new Value(valueQuery.Name, lastOutput);
            //fragmentMetadata.Values.Add(result);
            //output.OnNext(result);
        }
    }
}