namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class StructureQueryProcessor : IStructureQueryProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public StructureQueryProcessor(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Process(QueryFragment fragment, QueryExecutionScope executionScope, FragmentContext fragmentContext, IObserver<Structure> output)
        {
            var structureQuery = (StructureQuery) fragment;
            
            var script = new Script(new Sequence(new SequencePart[] {structureQuery.Annotation.Path}));
            var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);

            switch (structureQuery.Annotation.Type)
            {
                case AnnotationType.Node:
                    if (await processResult.Output.LastOrDefaultAsync() is IInternalNode lastOutput)
                    {
                        AddStructure(lastOutput, output, structureQuery, fragmentContext.ParentChildren);
                    }
                    break;
                case AnnotationType.Nodes:
                    processResult.Output
                        .OfType<IInternalNode>()
                        .Subscribe(
                            onError: e => output.OnError(e),
                            onNext: o => AddStructure(o, output, structureQuery, fragmentContext.ParentChildren),
                            onCompleted: () => { });
                    break;
            }

        }

        private void AddStructure(
            IInternalNode scriptOutput, 
            IObserver<Structure> queryOutput, 
            StructureQuery structureQuery, 
            ObservableCollection<Structure> parentChildren)
        {

            var childChildren = new ObservableCollection<Structure>();
            var childValues = new ObservableCollection<Value>();

            var properties = (IPropertyDictionary)scriptOutput.GetProperties();
            foreach (var property in properties)
            {
                var value = new Value(property.Key, property.Value);
                childValues.Add(value);
            }

            var result = new Structure(structureQuery.Name, new ReadOnlyObservableCollection<Structure>(childChildren), new ReadOnlyObservableCollection<Value>(childValues)); 
            parentChildren.Add(result);
            queryOutput.OnNext(result);
        }

    }
}