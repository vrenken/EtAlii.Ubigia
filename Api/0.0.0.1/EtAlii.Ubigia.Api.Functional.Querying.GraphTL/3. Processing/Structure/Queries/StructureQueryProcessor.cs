namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
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

        public async Task Process(
            QueryFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> fragmentOutput)
        {
            var structureQuery = (StructureQuery) fragment;

            var annotation = structureQuery.Annotation;
            if (annotation != null)
            {
                var script = new Script(new Sequence(new SequencePart[] {annotation.Path}));
                var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);

                switch (annotation.Type)
                {
                    case AnnotationType.Node:
                        if (await processResult.Output.SingleOrDefaultAsync() is IInternalNode lastOutput)
                        {
                            AddStructure(lastOutput, fragmentOutput, structureQuery, fragmentMetadata);
                        }
                        break;
                    case AnnotationType.Nodes:
                        processResult.Output
                            .OfType<IInternalNode>()
                            .Subscribe(
                                onError: fragmentOutput.OnError,
                                onNext: o => AddStructure(o, fragmentOutput, structureQuery, fragmentMetadata),
                                onCompleted: () => { });
                        break;
                }
            }
        }

        private void AddStructure(
            IInternalNode node, 
            IObserver<Structure> fragmentOutput, 
            StructureQuery structureQuery, 
            FragmentMetadata fragmentMetadata)
        {

            var item = new Structure(structureQuery.Name, node.Type, null, node);
            fragmentMetadata.Items.Add(item);
            fragmentOutput.OnNext(item);
        }
    }
}