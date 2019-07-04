namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class ValueQueryProcessor : IValueQueryProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public ValueQueryProcessor(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Process(QueryFragment fragment, QueryExecutionScope executionScope, FragmentContext fragmentContext, IObserver<Structure> output)
        {
            var valueQuery = (ValueQuery) fragment;
            
            var script = new Script(new Sequence(new SequencePart[] {valueQuery.Annotation.Path})); 
            var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);
            var lastOutput = await processResult.Output.LastOrDefaultAsync();

            var result = new Value(valueQuery.Name, lastOutput);
            fragmentContext.Values.Add(result);
            //output.OnNext(result);
        }
    }
}