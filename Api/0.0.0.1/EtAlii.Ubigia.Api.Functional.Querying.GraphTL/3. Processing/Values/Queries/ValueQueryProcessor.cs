namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class ValueQueryProcessor : IValueQueryProcessor
    {
        private readonly IValueGetter _valueGetter;

        public ValueQueryProcessor(IValueGetter valueGetter)
        {
            _valueGetter = valueGetter;
        }

        public async Task Process(
            QueryFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata,  
            IObserver<Structure> fragmentOutput)
        {
            var valueQuery = (ValueQuery) fragment;

            foreach (var structure in fragmentMetadata.Parent.Items)
            {
                var value = await _valueGetter.Get(valueQuery.Name, valueQuery.Annotation, executionScope, structure);
                if(value != null)
                {
                    structure.EditableValues.Add(value);
                }
            }
        }
    }
}