namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class ValueMutationProcessor : IValueMutationProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public ValueMutationProcessor(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Process(MutationFragmentExecutionPlan plan, QueryExecutionScope executionScope, FragmentContext fragmentContext, IObserver<Structure> output)
        {
            var valueQuery = (ValueMutation) plan.Fragment;
            
            var script = new Script(new Sequence(new SequencePart[] {valueQuery.Annotation.Path}));
            var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);
            var lastOutput = await processResult.Output.LastOrDefaultAsync();

            var result = new Value(valueQuery.Name, lastOutput);
            fragmentContext.Values.Add(result);
            //output.OnNext(result);
        }
    }
}